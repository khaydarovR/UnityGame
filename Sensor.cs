using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour
{
    [SerializeField] private LayerMask _sensorFor;

    [SerializeField] protected bool IsGrounded = false;




    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var i in collision.contacts)
        {
            if (i.normal.y > 0.8 && (_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
                IsGrounded = true;
            Debug.Log(i.normal.y);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            IsGrounded = false;
    }
}
