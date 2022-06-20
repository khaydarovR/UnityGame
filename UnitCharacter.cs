using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : Unit, IDiscoverable
{


    [SerializeField] private CharacterInput _controlInput;
    [SerializeField] private SensorGround _sensorGround;
    [SerializeField] private SpriteRenderer _sprite;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    //[SerializeField] private bool _blockInput;
    [SerializeField] private bool _isMoveCkimb;

    [SerializeField] private SensorClimb _sensorClimb;
    [SerializeField] private Transform _climbPoint;
    [SerializeField] private SensorWall _sensorWall;

    float progres = 0;
    Vector2 startPos;
    Vector2 toPoint;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controlInput = GetComponent<CharacterInput>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();

    }
    // описание вещей относящего только игроку
    private void Update()
    {
        float directionX = _controlInput.GetDirectionsX();


        base.Walk(directionX);
        base.ChangeDirection(directionX);

        

        // анимация при падение
        if (base.GetIsGround() == true)
        {
            _animator.SetBool("isJump", false);
        }
        else
        {
            _animator.SetBool("isJump", true);
        }

        // анимация при ходьбе
        if (base.GetIsGround() == true)
        {
            _animator.SetFloat("moveX", System.Math.Abs(directionX));
        }

        // анимация при беге
        if (_controlInput.IsRun())
        {
            base.Run(directionX);
            if (base.GetIsGround() == true)
                _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }

        if (_controlInput.IsRespawn())
            base.RespawnToPoint(new Vector2(6, 2));

    }

    public void FixedUpdate()
    {
        // анимация при прыжке
        if (_controlInput.IsJump() && (_controlInput.IsWalk() || _controlInput.IsRun()))
        {
            base.Jump();
            _animator.SetBool("isJump", true);
        }
        if(_isMoveCkimb == false)
            AirVelocity(_controlInput.IsRun());

        if (_controlInput.IsJump() || _isMoveCkimb == true)/////////////////////////////////
            WallClimb();

        //Debug.Log(_isMoveCkimb);
    }

    
    private void WallClimb()
    {
        
        var canClimb = _sensorClimb.IsClimb();
        var canWall = _sensorWall.IsWall();
        
        if ((canClimb && canWall) && (_isMoveCkimb == false || progres == 0)) ///////////////
        {
            _isMoveCkimb = true;
            startPos = transform.position;
            toPoint = _climbPoint.position;

           
        }
        else if (_isMoveCkimb == true)
        {
            base._rigidbody.velocity = new Vector2(0, 0);
            _boxCollider.enabled = false;
            _sensorGround.enabled = false;
            _sensorClimb.enabled = false;
            _sensorWall.enabled = false;
            _animator.SetBool("isWallClimb", true);
            transform.position = Vector2.Lerp(startPos, toPoint, progres);
            progres += 0.02f;
            if (progres > 0.9f)
            {
                
                progres = 0;
                _isMoveCkimb = false;
                _boxCollider.enabled = true;
                _sensorGround.enabled = true;
                _sensorClimb.enabled = true;
                _sensorWall.enabled = true;
                _animator.SetBool("isWallClimb", false);
                base._rigidbody.velocity = new Vector2(0, 0);
            }
        }
    }

    public void Detected(bool isDetected)
    {
        if (isDetected == true)
            _sprite.color = Color.red;
        else
            _sprite.color = Color.white;
    }
}
