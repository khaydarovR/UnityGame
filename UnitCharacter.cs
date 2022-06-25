
using UnityEngine;


public class UnitCharacter : Unit, IDiscoverable
{


    [SerializeField] private CharacterInput _controlInput;
    [SerializeField] private SensorGround _sensorGround;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _particlAtac;


    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private Transform _transform;
    //[SerializeField] private bool _blockInput;
    [SerializeField] private bool _isMoveCkimb;

    [SerializeField] private SensorClimb _sensorClimb;
    [SerializeField] private Transform _climbPoint;
    [SerializeField] private SensorWall _sensorWall;
    [SerializeField] private LayerMask _LayerForEnemy;



    float progres = 0;
    Vector2 startPos;
    Vector2 toPoint;

    private void Start()
    {
        base.StartAbstarktUnit();
        _animator = GetComponent<Animator>();
        _controlInput = GetComponent<CharacterInput>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();


    }
    // описание вещей относящего только игроку
    private void Update()
    {
        float directionX = _controlInput.GetDirectionsX();


        base.Walk(directionX);
        base.ChangeDirection(directionX);

        _particlAtac.transform.localScale = new Vector2(_currentDirection, 1);


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
            base.RespawnToPoint(new Vector2(16, 7));


        if (_controlInput.IsDamage())
        { 
            Instantiate(_particlAtac,_transform.position, Quaternion.identity);
            SetDamage();
        }
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

    public void SetDamage()
    {
        
        {
            RaycastHit2D _enemyInfo = Physics2D.Raycast(transform.position, base._currentDirection * transform.right, 0.8f, _LayerForEnemy);
            Debug.DrawRay(transform.position, base._currentDirection * 0.8f * transform.right, Color.red);

            if (_enemyInfo.collider == null)
            {
                return;
            }
            else if (_enemyInfo.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.GetDamage(base._damage);

            }
        }
        
    }
    public override void GetDamage(int value)
    {
        base.GetDamage(value);

        if (base._health <= 0)
        {
            _sensorGround.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }

}
