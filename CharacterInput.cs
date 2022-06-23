using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterInput : MonoBehaviour
{

    public float GetDirectionsX()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool IsRun()
    {
        if (GetDirectionsX() != 0 && Input.GetKey(KeyCode.LeftShift) == true)
            return true;
        else
            return false;
    }

    public bool IsWalk()
    {
        if (GetDirectionsX() != 0 && IsRun() == false)
            return true;
        else
            return false;
    }
    public bool IsJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public bool IsDamage()
    {
        return Input.GetKeyDown(KeyCode.E);
    }


    public bool IsRespawn()
    {
        return Input.GetKey(KeyCode.R);
    }
}


