using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackEvent : MonoBehaviour
{
    public Player player;
    Animator bowAnimator;


    private void Awake()
    {
        bowAnimator = player.bow.GetComponent<Animator>();
    }
    void ShootArrow()
    {
        bowAnimator.SetTrigger("ShootArrow");
    }

    void TakeArrow()
    {
        player.TakeArrow();
    }

    void ReleaseArrow()
    {
        player.ReleaseArrow();    
    }

    void ResetAfterShooting()
    {
        player.ResetAfterShooting();
    }
}
