using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    public float GetDirectionsX()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool IsRun()
    {
        if (GetDirectionsX() != 0 && Input.GetKey(KeyCode.LeftShift))
            return true;
        else
            return false;

        
    }

    public bool IsJump()
    {
        return Input.GetKey(KeyCode.Space);
    }
    public bool IsRespawn()
    {
        return Input.GetKey(KeyCode.R);
    }

}


