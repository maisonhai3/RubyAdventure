using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Animator _animator;
    private Vector2 lookDirection = new(1, 0);
    
    public float speed = 3.0f;

    public int maxHealth = 5;
    public int Health => _currentHealth;
    private int _currentHealth;

    public float timeInvincible = 2.0f;
    private bool _isInvincible;
    private float _invincibleTimer;

    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private float _vertical;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _currentHealth = maxHealth;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new(_horizontal, _vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        _animator.SetFloat("Look X", lookDirection.x);
        _animator.SetFloat("Look Y", lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        if (!_isInvincible) return;
        _invincibleTimer -= Time.deltaTime;
        if (_invincibleTimer <= 0)
            _isInvincible = false;
    }

    private void FixedUpdate()
    {
        var position = _rigidbody2D.position;
        position.x = position.x + speed * _horizontal * Time.deltaTime;
        position.y = position.y + speed * _vertical * Time.deltaTime;

        _rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            _animator.SetTrigger("Hit");
            
            if (_isInvincible)
                return;

            _isInvincible = true; // Ruby will be hit in the next physics update.
            _invincibleTimer = timeInvincible; // Reset the timer.
        }

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log($"Health: {_currentHealth} / {maxHealth}");
    }
}