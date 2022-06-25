using UnityEngine.UI;
using UnityEngine;
using System.Collections;

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

    [SerializeField] protected float _health;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected int _damage;
    [SerializeField] private Image _hpBarImage;


    private void Awake()
    {
        // Берёт информацию из евент менеджера
        Events.Air += OnAir; // подписка на канал EventManager.HealthChanged, вызов OnHealthChanged при оповещении
    }

    protected void StartAbstarktUnit()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        _hpBarImage.fillAmount = _health / _maxHealth;
    }

    protected void OnDestroy()
    {
        Events.Air -= OnAir;
    }


    protected bool GetIsGround()
    {
        return _onGround;
    }

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
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            Instantiate(_particalJump, transform);
        }

    }

    protected void Reclining()
    {
        {
            _rigidbody.velocity = new Vector2(_runSpeed * -_currentDirection, (float)_jumpForce/2);
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

    public virtual void GetDamage(int value)
    {
        Reclining();
        Instantiate(_particalBlod, transform);
        _health = ((int)_health) - value;
        _hpBarImage.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {
            Reclining();
            _rigidbody.GetComponent<Collider2D>().isTrigger = true;

            StartCoroutine(Death());
        }
            
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
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
