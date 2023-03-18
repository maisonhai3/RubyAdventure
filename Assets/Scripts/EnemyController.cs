using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    public float speed;
    public bool vertical;

    private Rigidbody2D _rigidbody2D;
    
    // How long the enemy will wait before turning around.
    public float turnTime = 3.0f;
    private float _turnTimer;
    private int _direction = 1;

    private Animator _animator;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _turnTimer = turnTime;
    }

    private void Update()
    {
        _turnTimer -= Time.deltaTime;

        if (!(_turnTimer < 0)) return;
        _direction = -_direction;
        _turnTimer = turnTime;
    }

    private void FixedUpdate()
    {
        var position = _rigidbody2D.position;
        
        if (vertical)
        {
            position.y += Time.deltaTime * speed;
            _animator.SetFloat("Move X", 0);
            _animator.SetFloat("Move Y", _direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed;
            _animator.SetFloat("Move X", _direction);
            _animator.SetFloat("Move Y", 0);
        }

        _rigidbody2D.MovePosition(position);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}