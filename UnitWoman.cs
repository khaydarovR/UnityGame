using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWoman : Unit
{
    [SerializeField] private Transform _ourTransform;
    [SerializeField] private LayerMask _LayerForEye;
    [SerializeField] private LayerMask _LayerForWall;
    [SerializeField] private GameObject _particlAtac;
    [SerializeField] private int _eyeDistance = 6;
    [SerializeField] private int _currentState;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _walkTime = 300;
    [SerializeField] private int _reloadTime = 50;

    private bool _onWall = false;
    private readonly float _eyeHeight = 0.1f;
    private Vector2 _rayOrigin;
    private int _curentEnergyWalk;
    private int _curentReloadTime = 0;
    
    

    private void Start()
    {
        _currentState = (int)WomanState.isSecurity;
        _ourTransform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        base._currentDirection = 1;
        _curentEnergyWalk = 0;
    }

    private void FixedUpdate()
    {
        SensorCharacter();
        
    }
    private void Update()
    {

        _animator.SetInteger("WomanState", _currentState);
        _particlAtac.transform.localScale = new Vector2(_currentDirection, 1);

        switch (_currentState)
        {
            case (int)WomanState.isIdle:
                _curentEnergyWalk++;
                if (_curentEnergyWalk > _walkTime)
                {
                    _currentState = (int)WomanState.isSecurity;
                    base._currentDirection = -base._currentDirection;
                    ChangeDirection(base._currentDirection);
                }
                break;
            case (int)WomanState.isSecurity:
                base.Walk(base._currentDirection);
                SensorWall();
                _curentEnergyWalk--;
                if (_curentEnergyWalk < 0)
                    _currentState = (int)WomanState.isIdle;
                break;
            case (int)WomanState.isPursuit:        
                base.Run(base._currentDirection);   
                break;
            case (int)WomanState.isAtak:

                break;

        }  

    }
                           
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((_LayerForWall & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            base.Jump();
            Instantiate(base._particalJump, transform);
        }
            
        return;
    }

    enum WomanState : int
    { 
        isIdle = 0,
        isSecurity = 1,//анимаци€ ходдьбы
        isPursuit = 2,
        isAtak = 3,
        

        //все состо€ни€ woman
    }

    private void SensorWall()
    {
        //–ейкаст дл€ поворота от стены (ближн€€)
        RaycastHit2D _wallInfo = Physics2D.Raycast(_rayOrigin, base._currentDirection * _ourTransform.right, 1.4f, _LayerForWall);
        Debug.DrawRay(_rayOrigin, base._currentDirection  * 1.4f * _ourTransform.right, Color.red);
        if (_wallInfo.collider == null) //если рейкаст не врезалс€
        {
            _onWall = false;
        }
        else
        {
            _onWall = true;
        }

        if (_onWall == true)
        {
            base._currentDirection = -base._currentDirection; //если стена направление измен€ем с -1 на -(-1)
            ChangeDirection(base._currentDirection);          //мен€ем напрвление на текущее
        }
    }

    private void SensorCharacter()
    {
        _rayOrigin = new Vector2(_ourTransform.position.x, _ourTransform.position.y + _eyeHeight);

        //–ейкаст дл€ обноружени€ противников (дальна€)
        RaycastHit2D _eyeInfo = Physics2D.Raycast(_rayOrigin, base._currentDirection * _ourTransform.right, _eyeDistance,_LayerForEye);
        Debug.DrawRay(_rayOrigin, base._currentDirection * _eyeDistance *_ourTransform.right, Color.green);
        if (_eyeInfo.collider.TryGetComponent(out IDiscoverable discoverable))//если нашлось обект с компонентом
        {       
            if (_eyeInfo.collider.TryGetComponent(out IDamagable damagable) && _eyeInfo.distance < 1)
            {
                _currentState = (int)WomanState.isAtak;
                if (_curentReloadTime == 0)
                {
                    damagable.GetDamage(base._damage);
                    Instantiate(_particlAtac, transform.position, Quaternion.identity);

                }
                _curentReloadTime++;
                if (_curentReloadTime > _reloadTime)
                    _curentReloadTime = 0;
            }
            else
            {
                _currentState = (int)WomanState.isPursuit;
                discoverable.Detected(true);
            }
        }
        else// если пусто
        {
            if (_curentEnergyWalk > 0)
                _currentState = (int)WomanState.isSecurity;
            else if (_curentEnergyWalk < 0)
                _currentState = (int)WomanState.isIdle;
        }
    }
}
