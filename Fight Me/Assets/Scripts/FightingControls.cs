using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingControls : MonoBehaviour
{
    RangedAttack RangedAttack;

    private void Awake()
    {
        RangedAttack = GetComponent<RangedAttack>();
    }
    private void Update()
    {
        RangedAttack.Shoot();
    }
}
