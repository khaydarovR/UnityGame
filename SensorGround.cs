using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGround : Sensor
{
    //[SerializeField] private BoxCollider2D boxCollider2D;

    private void Update()
    {

        if (base.IsGrounded == true)
            Events.OnAir(false);
        else
            Events.OnAir(true);
        Debug.Log("�� �����:" + IsGrounded);

    }


}
