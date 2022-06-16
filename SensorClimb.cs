using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorClimb : MonoBehaviour
{
    [SerializeField] private LayerMask _sensorFor;

    public bool _isClimb = false;

    private void Start()
    {

     
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isClimb = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isClimb = true;
    }

}
