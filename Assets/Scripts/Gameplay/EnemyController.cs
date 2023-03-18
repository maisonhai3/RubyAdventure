using UnityEngine;

namespace Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        private bool broken = true;

        // How long the enemy will wait before turning around.
        public float speed;
        public bool vertical;
        public float changeTime = 3.0f;
        private float timer;
        private readonly float speedUpTimer = 4;
        private int direction = 3;

        private Animator _animator;
        private Rigidbody2D rigidbody2D;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            timer = changeTime;
        }

        private void Update()
        {
            if (!broken)
                return;

            // Change direction
            timer -= Time.deltaTime * speedUpTimer;

            if ((timer < 0))
            {
                direction = -direction;
                timer = changeTime;
            }
        }

        private void FixedUpdate()
        {
            if (!broken)
                return;

            var position = rigidbody2D.position;

            if (vertical)
            {
                position.y += Time.deltaTime * speed * direction;
                _animator.SetFloat("Move X", 0);
                _animator.SetFloat("Move Y", direction);
            }
            else
            {
                position.x = position.x + Time.deltaTime * speed * direction;
                _animator.SetFloat("Move X", direction);
                _animator.SetFloat("Move Y", 0);
            }

            rigidbody2D.MovePosition(position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<RubyController>();

            if (player != null)
            {
                player.ChangeHealth(-1);
            }
        }

        public void Fix()
        {
            broken = false;
            rigidbody2D.simulated = false;

            _animator.SetTrigger("Fixed");
        }
    }
}