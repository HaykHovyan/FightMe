using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public Player player;
    public GameObject aimJoystick;
    public GameObject attackButton;
    public GameObject superAttackButton;
    public GameObject blockButton;

    public void ChangeState()
    {
        if (!player.isShooting && !player.isAttacking)
        {
            if (aimJoystick.gameObject.activeSelf)
            {
                aimJoystick.SetActive(false);
                attackButton.SetActive(true);
                superAttackButton.SetActive(true);
                blockButton.SetActive(true);
            }

            else
            {
                aimJoystick.SetActive(true);
                attackButton.SetActive(false);
                superAttackButton.SetActive(false);
                blockButton.SetActive(false);
            }

            if (player.sword.activeSelf == true)
            {
                player.sword.SetActive(false);
                player.bow.SetActive(true);
            }

            else
            {
                player.sword.SetActive(true);
                player.bow.SetActive(false);
            }
        }
    }
}
