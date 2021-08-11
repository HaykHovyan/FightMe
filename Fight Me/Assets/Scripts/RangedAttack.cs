using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RangedAttack : MonoBehaviour
{
    public Transform player;
    public GameObject rangedWeaponPF;
    private GameObject rangedWeapon;
    public GameObject rangedTrajectory;

    //which way the ranged weapon should face
    public Transform rangedDirection;



    //For joystick
    public FixedJoystick aimJoystick;
    private float aimHorizontal;
    private float aimVertical;
    private Vector3 aimDirection;

    //Max time of ranged weapon moving in the air
    private float time;

    public float projectileSpeed;

    private void Start()
    {
        time = 1 / projectileSpeed;
    }

    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rangedTrajectory.SetActive(true);
            rangedWeapon = Instantiate(rangedWeaponPF, rangedDirection.position, rangedDirection.rotation, player);
        }

        if (Input.GetMouseButton(0))
        {
            aimJoystick.GetComponent<Image>().enabled = true;
            aimHorizontal = aimJoystick.Horizontal;
            aimVertical = aimJoystick.Vertical;
            aimDirection = player.position + new Vector3(aimHorizontal, 0, aimVertical);
            player.LookAt(aimDirection);
        }

        if (Input.GetMouseButtonUp(0))
        {
            aimJoystick.GetComponent<Image>().enabled = false;
            rangedTrajectory.SetActive(false);
            rangedWeapon.transform.DOMove(rangedWeapon.transform.position + rangedWeapon.transform.forward * 10, time);
            Destroy(rangedWeapon, time);
        }
    }
}
