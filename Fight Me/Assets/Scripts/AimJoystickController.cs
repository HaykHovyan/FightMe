using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AimJoystickController : MonoBehaviour
{
    private Transform player;
    public GameObject rangedWeaponPF;
    private GameObject rangedWeapon;
    public GameObject rangedTrajectory;

    //which way the ranged weapon should face
    public Transform rangedDirection;



    //For joystick
    public GameObject handle;
    public FixedJoystick aimJoystick;
    private float aimHorizontal;
    private float aimVertical;
    private Vector3 aimDirection;

    //Max time of ranged weapon moving in the air
    private float time;

    public float projectileSpeed;
    private bool isPressed;

    private void Start()
    {
        time = 1 / projectileSpeed;
        player = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PointerIsOnUI())
            {
                isPressed = true;
                aimJoystick.GetComponent<Image>().enabled = true;
                rangedTrajectory.SetActive(true);
                rangedWeapon = Instantiate(rangedWeaponPF, rangedDirection.position, rangedDirection.rotation, player);
            }
        }

        if (Input.GetMouseButton(0))
        {
            aimHorizontal = aimJoystick.Horizontal;
            aimVertical = aimJoystick.Vertical;
            aimDirection = player.position + new Vector3(aimHorizontal, 0, aimVertical);
            player.LookAt(aimDirection);
        }


        if (Input.GetMouseButtonUp(0) && isPressed)
        {
            Debug.Log("Shoot");
            aimJoystick.GetComponent<Image>().enabled = false;
            rangedTrajectory.SetActive(false);
            rangedWeapon.transform.DOMove(rangedWeapon.transform.position + rangedWeapon.transform.forward * 10, time);
            Destroy(rangedWeapon, time);
            isPressed = false;
        }
    }

    private bool PointerIsOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
