using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour
{
    [SerializeField] private int _countCollider = 0;
    [SerializeField] private LayerMask _sensorFor;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {

            _countCollider++;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((_sensorFor & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {

            _countCollider--;
        }
    }

    public int GetCountCollider()
    {
        Debug.Log("количество пересещений с колайдерами" + _countCollider);
        return _countCollider;
    }
}
