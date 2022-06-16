using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorWall : MonoBehaviour
{
    [SerializeField] private LayerMask _sensorFor;

    public bool _isWall = false;

    private void Start()
    {


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isWall = false;
    }

}
