using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : Unit
{


    [SerializeField] private CharacterInput _controlInput;


    // описание вещей относящего только игроку
    private void Update()
    {
        float directionX = _controlInput.GetDirectionsX();

        base.Walk(directionX);

        if (_controlInput.IsRun())
            base.Run(directionX);

        if (_controlInput.IsJump())
            base.Jump();

        if (_controlInput.IsRespawn())
            base.Respawn();

        
        //hp bar
        //использование вещей укрытий карты
    }


}
