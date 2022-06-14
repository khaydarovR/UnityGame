using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGround : Sensor
{


    private void Update()
    {
        if (base.GetCountCollider() <= 0)
        {
            Events.OnAir(true);
            //Debug.Log("Отправка - Сенсор НЕ в земле");
        }
        else
        {
            Events.OnAir(false);
            //Debug.Log("Отправка - Сенсор НА  в земле");
        }


    }


}
