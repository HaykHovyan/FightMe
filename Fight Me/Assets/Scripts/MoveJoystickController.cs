using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystickController : MonoBehaviour
{
    private Transform player;


    //For joystick
    public FixedJoystick moveJoystick;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
