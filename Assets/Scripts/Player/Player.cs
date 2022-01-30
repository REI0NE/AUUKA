using System;
using UnityEngine;

public class Player : MonoCache, IPlayer
{
    [SerializeField] private PlayerState _state = PlayerState.Idle;
    [SerializeField] private Stats _stats = null;
    [SerializeField] private Transform _firePoint = null;

    public event Action<Vector3> Transition = delegate { };

    private Rigidbody _rigidBody = null;
    private Animator _animator = null;
    private Cursor _cursor = null;
    private Audio _audio = null;
    private bool _isForest = false;
    private bool _attackMode = false;

    private string[] _sounds = new string[2];

    private void Awake()
    {
        _rigidBody ??= GetComponent<Rigidbody>();
        _animator ??= GetComponent<Animator>();
        _cursor ??= FindObjectOfType<Cursor>();
        _audio ??= FindObjectOfType<Audio>();
    }

    private void Start()
    {
        _sounds[0] = "WalkHouse";
        _sounds[1] = "WalkForest";
    }

    private void OnEnable()
    {
        AddUpdateTick();
        _singleton.Data.Player = this;
    }
    private void OnDisable()
    {
        RemoveUpdateTick();
        _singleton.Data.Player = null;
    }
    private void OnDestroy() => OnDisable();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
            _isForest = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
            _isForest = false;
    }

    private void Movement(string param = null)
    {
        Vector2 velocity = Vector2.zero;
        velocity.Set(Input.GetAxis("Horizontal") * _stats.GetStat(param ?? "Walk"), _rigidBody.velocity.y);
        _rigidBody.velocity = velocity;
    }

    private void MovementForest()
    {
        if (_isForest)
        {
            if (Input.GetKeyDown(KeyCode.W))
                Transition?.Invoke(Vector3.up);

            if (Input.GetKeyDown(KeyCode.S))
                Transition?.Invoke(Vector3.down);
        }
    }

    private void Jump(string param = null)
    {
        Vector2 powar = Vector2.zero;
        powar.Set(_rigidBody.velocity.x, _stats.GetStat(param ?? "Jump"));
        _rigidBody.velocity = powar;
    }

    private void Flip()
    {
        Vector2 scale = Vector2.zero;
        scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        _firePoint.Rotate(0f, 0f, 180f);
    }

    private void IsFlip()
    {
        if (_cursor.Poss().position.x < transform.position.x & transform.localScale.x > 0)
            Flip();

        if (_cursor.Poss().position.x > transform.position.x & transform.localScale.x < 0)
            Flip();
    }

    private void SwitchState()
    {
        switch (_state)
        {
            case PlayerState.Idle:
                if (Input.GetAxis("Horizontal") != 0f)
                {
                    _state = PlayerState.Walk;
                    break;
                }
                if (_attackMode)
                {
                    _state = PlayerState.Attack;
                    break;
                }
                MovementForest();
                //_audio.SwitchMusic("Default");
                break;
            case PlayerState.Jump:
                if (Input.GetAxis("Vertical") == 0f)
                {
                    _state = PlayerState.Idle;
                    break;
                }
                Jump();
                Movement();
                break;
            case PlayerState.Walk:
                MovementForest();
                if (Input.GetAxis("Horizontal") == 0f)
                {
                    _state = PlayerState.Idle;
                    break;
                }
                Movement();
                break;
            case PlayerState.Attack:
                if (!_attackMode)
                {
                    _state = PlayerState.Idle;
                    break;
                }
                //_audio.SwitchMusic("Battle");
                break;
            case PlayerState.Find:
                break;
            case PlayerState.Die:
                break;
        }
    }

    public override void UpdateTick()
    {
        if (_singleton.Data.Settings.IsPause)
            return;

        IsFlip();
        SwitchState();
        if (_animator.GetInteger("Poss") != (int)_state)
            _animator.SetInteger("Poss",(int)_state);


        if (_state == PlayerState.Walk) _audio.PlayWalk(_sounds[Convert.ToInt32(_isForest)]);
        else _audio.StopWalk();
    }


    public string Name() => name;
    public Stats GetStats() => _stats;
    public Transform Poss() => transform;

    public PlayerState GetState() => _state;

    public void SwitchMod() => _attackMode = ! _attackMode;
}

public enum PlayerState
{
    Idle,
    Walk,
    Attack,
    Jump,
    Find,
    Die,
}
