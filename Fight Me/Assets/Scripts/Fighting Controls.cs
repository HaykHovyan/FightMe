using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingControls : MonoBehaviour
{
    public Transform arrow;
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Debug.Log("Hit");
    }
}
