using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorClimb : MonoBehaviour
{
    [SerializeField] private LayerMask _sensorFor;

    private bool _isClimb;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            _isClimb = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            _isClimb = true;
    }

    public bool IsClimb()
    {
        return _isClimb;
    }
}
