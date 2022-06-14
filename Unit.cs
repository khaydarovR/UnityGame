using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _sencorGround;
    [SerializeField] private int _walkSpead;
    [SerializeField] private int _runSpeed;
    [SerializeField] private int _jumpForce;
    private bool _onGround;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    
    private void Awake()
    {
        // Берёт информацию из евент менеджера
        Events.Air += OnAir; // подписка на канал EventManager.HealthChanged, вызов OnHealthChanged при оповещении
    }
    private void OnDestroy()
    {
        Events.Air -= OnAir;
        //Debug.Log("Отписка - на сенсор");
    }

    protected void Walk(float directions)
    {
        _rigidbody.velocity = new Vector2(directions * _walkSpead, _rigidbody.velocity.y);
    }

    protected void Run (float directions)
    {
        _rigidbody.velocity = new Vector2(directions * _runSpeed, _rigidbody.velocity.y);
    }

    protected void Jump()
    {
        if (_onGround == true)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

        }
    }

    protected void OnAir(bool onAir)
    {
        _onGround = !onAir;
    }

    protected void RespawnToPoint(Vector2 point)
    {
        transform.position = point;
    }
    protected void Respawn()
    {
        transform.position = new Vector2(0,2);
    }

}
