using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : Unit
{


    [SerializeField] private CharacterInput _controlInput;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controlInput = GetComponent<CharacterInput>();
    }
    // описание вещей относящего только игроку
    private void Update()
    {
        float directionX = _controlInput.GetDirectionsX();


        base.Walk(directionX);

        // анимация при падение
        if (base._onGround == true)
            _animator.SetBool("isJump", false);
        else
            _animator.SetBool("isJump", true);

        // анимация при ходьбе
        if (base._onGround == true)
        {
            _animator.SetFloat("moveX", System.Math.Abs(directionX));
        }

        // анимация при беге
        if (_controlInput.IsRun())
        {
            base.Run(directionX);
            if (base._onGround == true)
                _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }

        // анимация при прыжке
        if (_controlInput.IsJump() && _controlInput.IsRun())
        {
            base.Jump();
            _animator.SetBool("isJump", true);
        }
     


        if (_controlInput.IsRespawn())
            base.RespawnToPoint(new Vector2(0,2));


    }


}
