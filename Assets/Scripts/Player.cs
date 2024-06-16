using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject playerCharacter;

    public GameObject sword;

    public GameObject bow;
    public GameObject arrowPF; //PF = prefab
    private GameObject arrow;
    public GameObject arrowTrajectoryParent;
    public Transform arrowTrajectory;
    public Transform rightHand;

    //Where the arrow should spawn
    public Transform arrowPosition;

    //Where the arrow should be when taken into hand
    public Transform arrowPositionInHand;
    
    //From where the attack range should be calculated (for regular attack)
    public Transform attackPoint;
        
    //Reference to the Animator component
    public static Animator animator;

    //To detect weapon collision with the enemy
    public LayerMask enemyLayer;
    
    //To turn the player according to the trajectory of the arrow
    Vector3 aimDirectionForPlayer;
    
    public float moveSpeed;

    //Range of regular attack
    public float attackRange;

    //So that the player can't move or change direction while attacking
    public bool isAttacking;

    //Force applied to arrow upon shooting
    public float arrowForce;

    //How long to wait to destroy the arrow upon hitting the ground
    public float arrowDestroyTime;

    //So the player can't aim without getting his bow first
    bool isReadyToShoot;

    //So the player can't shoot without aiming first
    bool isAiming;

    //So that the player can't use the move joystick while shooting
    public bool isShooting;

    //Variable to offset player rotation while shooting
    public float rangedOffset;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move()
    {
        if (!isShooting && !isAttacking)
        {
            animator.SetBool("isMoving", true);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ChangeDirection(float _horizontal, float _vertical)
    {
        if (!isShooting && !isAttacking)
        {
            transform.LookAt(transform.position + new Vector3(_horizontal, 0, _vertical));
            transform.eulerAngles = new Vector3(transform.rotation.x,
                                                transform.eulerAngles.y + 45,
                                                transform.rotation.z);
        }
    }
    public void RegularAttack()
    {
        if (!isShooting)
        {
            StartCoroutine(WaitForAttack());
            animator.SetTrigger("Attack");
            isAttacking = true;

            Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider enemy in hitEnemy)
            {
                Debug.LogError("Hit");
            }
        }
    }

    //To determine attack range
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere(attackPoint.position, attackRange);
    //}

    //Wait for the attack animation to end, to set isAttacking to false;
    IEnumerator WaitForAttack()
    {
        float attackAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackAnimationLength);
        isAttacking = false;
    }

    public void GetRangedWeapon()
    {
        if (!isAttacking && !isShooting)
        {
            isReadyToShoot = true;
            arrowTrajectoryParent.SetActive(true);
        }
    }

    public void Aim(float _horizontal, float _vertical)
    {
        if (!isAttacking && isReadyToShoot)
        {
            isAiming = true;
            Vector3 aimDirection = arrowTrajectoryParent.transform.position + new Vector3(_horizontal, 0, _vertical);
            aimDirectionForPlayer = transform.position + new Vector3(_horizontal, 0, _vertical);
            arrowTrajectoryParent.transform.LookAt(aimDirection);
            arrowTrajectoryParent.transform.eulerAngles = new Vector3(arrowTrajectoryParent.transform.eulerAngles.x,
                                                                      arrowTrajectoryParent.transform.eulerAngles.y + 45,
                                                                      arrowTrajectoryParent.transform.eulerAngles.z);
        }
    }

    public void TakeArrow()
    {
        arrow.transform.parent = rightHand;
        arrow.transform.position = arrowPositionInHand.position;
        arrow.transform.rotation = arrowPositionInHand.rotation;
    }

    public void DrawBow()
    {
        arrowTrajectoryParent.SetActive(false);
        if (!isAttacking && isAiming)
        {
            isShooting = true;
            transform.LookAt(aimDirectionForPlayer);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                transform.eulerAngles.y + 45,
                                                transform.eulerAngles.z);
            animator.SetTrigger("RangedAttack");

            arrow = Instantiate(arrowPF, arrowPosition.position, arrowPosition.rotation, arrowPosition.parent);

            AnimatorClipInfo[] currentAnimationInfo = animator.GetCurrentAnimatorClipInfo(0);

            if (currentAnimationInfo[0].weight * (currentAnimationInfo[0].clip.length * currentAnimationInfo[0].clip.frameRate) == 9)
            {
                arrow.transform.parent = rightHand;
            }

            //To offset the weird rotation during the shooting animation
            playerCharacter.transform.eulerAngles = new Vector3(playerCharacter.transform.eulerAngles.x,
                                                                playerCharacter.transform.eulerAngles.y + rangedOffset,
                                                                playerCharacter.transform.eulerAngles.z);
        }
    }

    public void ReleaseArrow()
    {
        arrow.transform.parent = null;
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowForce, ForceMode.Impulse);
        arrow.GetComponent<Rigidbody>().useGravity = true;
        isAiming = false;
        isShooting = false;
        Cooldown.isCooldown = true;
        Cooldown.cooldownTimer = Cooldown.cooldownTime;
    }

    public void ResetAfterShooting()
    {
        playerCharacter.transform.localPosition = Vector3.zero;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        float timer = 0f;
        Quaternion startRot = playerCharacter.transform.localRotation;
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            playerCharacter.transform.localRotation = Quaternion.Lerp(startRot, Quaternion.identity, timer);
            yield return new WaitForEndOfFrame();
        }
        playerCharacter.transform.localRotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
            Destroy(arrow, arrowDestroyTime);

        if(collision.collider.tag == "Enemy")
        {
            Debug.Log("Bullseye!");
            Destroy(arrow);
        }
    }
}
