using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorWall : MonoBehaviour
{
    [SerializeField] private LayerMask _sensorFor;

    private bool _isWall;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            _isWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            _isWall = false;
    }

    public bool IsWall()
    {
        return _isWall;
    }
}
