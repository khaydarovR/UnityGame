using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    //[SerializeField] private BoxCollider2D _sencorGround;

    [SerializeField] private int _walkSpead = 2;
    [SerializeField] private int _runSpeed = 5;
    [SerializeField] private int _jumpForce = 6;
    [SerializeField] private bool kindOfJamp = false;
    private bool _flag = false;
    protected bool _onGround;
    private bool _directionRight = true;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    
    private void Awake()
    {
        // Берёт информацию из евент менеджера
        Events.Air += OnAir; // подписка на канал EventManager.HealthChanged, вызов OnHealthChanged при оповещении
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //_sencorGround = GetComponent<BoxCollider2D>();

    }

    private void OnDestroy()
    {
        Events.Air -= OnAir;
    }

    //////////////////////
                         //////// BLOK 
    //////////////////////
    protected void Walk(float directions)
    {
        _rigidbody.velocity = new Vector2(directions * _walkSpead, _rigidbody.velocity.y);

    }

    protected void ChangeDirection(float directions)
    {
        if (directions < 0 && _directionRight == true)
        {
            _rigidbody.transform.localScale *= new Vector2(-1, 1);
            _directionRight = false;
        }


        if (directions > 0 && _directionRight == false)
        {
            _rigidbody.transform.localScale *= new Vector2(-1, 1);
            _directionRight = true;
        }
    }

    protected void Run (float directions)
    {
        _rigidbody.velocity = new Vector2(directions * _runSpeed, _rigidbody.velocity.y);
    }

    protected void Jump()
    {      
        if (_onGround == true)
        {
            var _2 = _rigidbody.velocity.x;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

        }

    }

    protected void OnAir(bool onAir)
    {
        _onGround = !onAir;
    }
    protected void AirVelocity(bool isRun)
    {
        if (_onGround == false)
        {
            var _1 = 0;
            if (_directionRight == true)
                _1 = 1;
            else
                _1 = -1;

            if (_flag == false)
                kindOfJamp = isRun;
            _flag = true;


            if (kindOfJamp == true)
                _rigidbody.velocity = new Vector2(_1 * _runSpeed, _rigidbody.velocity.y);
            else
                _rigidbody.velocity = new Vector2(_1 * _walkSpead, _rigidbody.velocity.y);
        }
        else
            _flag = false;

    }

    protected void StopWall()
    {
        //_rigidbody.transform.position = new Vector3(_rigidbody.transform.position.x, _rigidbody.transform.position.y, _rigidbody.transform.position.z);
        Debug.Log("do climb");
        _rigidbody.velocity = new Vector2(0,0);
    }

    protected void RespawnToPoint(Vector2 point)
    {
        transform.position = point;
    }

}
