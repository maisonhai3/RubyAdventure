using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    public float speed;
    public bool vertical;

    private Rigidbody2D _rigidbody2D;
    
    // How long the enemy will wait before turning around.
    public float turnTime = 3.0f;
    private float _turnTimer;
    private int direction = 1;

    private Animator _animator;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _turnTimer = turnTime;
    }

    private void FixedUpdate()
    {
        var position = _rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed;
            _animator.SetFloat("Move X", 0);
            _animator.SetFloat("Move Y", direction);
        }
        else
        {
            _animator.SetFloat("Move Y", direction);
            _animator.SetFloat("Move X", 0);
            position.x = position.x + Time.deltaTime * speed;
        }

        _rigidbody2D.MovePosition(position);
    }
}