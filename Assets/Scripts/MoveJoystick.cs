using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MoveJoystick : Joystick
{
    //Reference to Player class
    public Player player;

    //Initiate movement until the joystick is released
    private bool moveJoystickIsPressed;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        moveJoystickIsPressed = true;
        StartCoroutine(PlayerMove());
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        moveJoystickIsPressed = false;
        Player.animator.SetBool("isMoving", false);
    }

    IEnumerator PlayerMove()
    {
        while (moveJoystickIsPressed == true)
        {
            player.Move();
            player.ChangeDirection(Horizontal, Vertical);
            yield return null;
        }
    }
}