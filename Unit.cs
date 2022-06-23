using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamagable
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected GameObject _particalBlod;
    [SerializeField] protected GameObject _particalJump;
    [SerializeField] protected int _walkSpead = 2;
    [SerializeField] private int _runSpeed = 5;
    [SerializeField] private int _jumpForce = 6;
    private bool kindOfJamp = false;
    private bool _flag = false;
    private bool _onGround;
    private bool _directionRight = true;
    protected int _currentDirection; // -1 <-----> 1

    [SerializeField] protected int _health;
    [SerializeField] protected int _damage;
    
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


    protected bool GetIsGround()
    {
        return _onGround;
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

        if (_directionRight == true)
            _currentDirection = 1;
        else
            _currentDirection = -1;
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
            Instantiate(_particalJump, transform);
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
            if (_flag == false)
                kindOfJamp = isRun;
            _flag = true;


            if (kindOfJamp == true)
                _rigidbody.velocity = new Vector2(_currentDirection * _runSpeed, _rigidbody.velocity.y);
            else
                _rigidbody.velocity = new Vector2(_currentDirection * _walkSpead, _rigidbody.velocity.y);
        }
        else
            _flag = false;
    }

    protected void RespawnToPoint(Vector2 point)
    {
        transform.position = point;
    }

    public void GetDamage(int value)
    {
        Jump();
        Instantiate(_particalBlod, transform);
        _health -= value;
        if (_health <= 0)
        {
            // через секунду
            _rigidbody.AddForce(new Vector2(-1 * _jumpForce, 0.5f), ForceMode2D.Impulse);
            gameObject.SetActive(false);
        }
            
    }


    void OnBecameVisible()
    {
        enabled = true;
        //this.gameObject.SetActive(true);
    }

    void OnBecameInvisible()
    {
        enabled = false;
        //this.gameObject.SetActive(false);
    }
}
