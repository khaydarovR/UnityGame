using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWoman : Unit
{
    [SerializeField] private Transform _ourTransform;
    [SerializeField] private LayerMask _LayerForEye;
    [SerializeField] private LayerMask _LayerForWall;
    [SerializeField] private int _eyeDistance = 6;
    [SerializeField] private int _currentState;
    [SerializeField] private Animator _animator;

    private bool _onWall = false;
    private readonly float _eyeHeight = 1.2f;
    private Vector2 _rayOrigin;
    
    

    private void Start()
    {
        _currentState = (int)WomanState.isSecurity;
        _ourTransform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        base._currentDirection = 1;
    }

    private void FixedUpdate()
    {
        SensorCharacter();
        
    }
    private void Update()
    {

        _animator.SetInteger("WomanState", _currentState);


        switch (_currentState)
        {
            //case (int)WomanState.isIdle:
            //    break;
            case (int)WomanState.isSecurity:
                base.Walk(base._currentDirection);
                SensorWall();
                break;
            case (int)WomanState.isPursuit:        
                base.Run(base._currentDirection);   
                break;
        }  

    }
                           
    private void OnCollisionExit2D(Collision2D collision)
    {
        // реализтвать падение с уступа
        return;
    }

    enum WomanState : int
    { 
        isIdle = 0,//анимация ходдьбы
        isSecurity = 1,
        isPursuit = 2,
        

        //все состояния woman
    }

    private void SensorWall()
    {
        //Рейкаст для поворота от стены (ближняя)
        RaycastHit2D _wallInfo = Physics2D.Raycast(_rayOrigin, base._currentDirection * _ourTransform.right, 1, _LayerForWall);
        Debug.DrawRay(_rayOrigin, base._currentDirection  * 1 *_ourTransform.right, Color.red);
        if (_wallInfo.collider == null) //если рейкаст не врезался
        {
            _onWall = false;
        }
        else
        {
            _onWall = true;
        }

        if (_onWall == true)
        {
            base._currentDirection = -base._currentDirection; //если стена направление изменяем с -1 на -(-1)
            ChangeDirection(base._currentDirection);          //меняем напрвление на текущее
        }
    }

    private void SensorCharacter()
    {
        _rayOrigin = new Vector2(_ourTransform.position.x, _ourTransform.position.y + _eyeHeight);

        //Рейкаст для обноружения противников (дальная)
        RaycastHit2D _eyeInfo = Physics2D.Raycast(_rayOrigin, base._currentDirection * _ourTransform.right, _eyeDistance,_LayerForEye);
        Debug.DrawRay(_rayOrigin, base._currentDirection * _eyeDistance *_ourTransform.right, Color.green);
        if (_eyeInfo.collider.TryGetComponent(out IDiscoverable discoverable))//если нашлось обект с компонентом
        {
            discoverable.Detected(true);
            _currentState = (int)WomanState.isPursuit;
        }
        else// если пусто
        {
            _currentState = (int)WomanState.isSecurity;
        }
    }
}
