using UnityEngine;

namespace Gameplay
{
    public class EnemyController : MonoBehaviour
    {
        public ParticleSystem smokeEffect;
        
        private Animator animator;
        private Rigidbody2D rigidbody2D;
        
        public float speed;
        public bool vertical;
        public float changeTime = 3.0f;
        
        private bool broken = true;

        // How long the enemy will wait before turning around.
        private float timer;
        private readonly float speedUpTimer = 4;
        private int direction = 3;
        private AudioSource audioSource;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

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

            var xRandomNumber = Random.Range(-3f, 3f);
            var yRandomNumber = Random.Range(-3f, 3f);
            position.y += Time.deltaTime * speed * direction * yRandomNumber;
            position.x += Time.deltaTime * speed * direction * xRandomNumber;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", direction);
            

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

            animator.SetTrigger("Fixed");
            smokeEffect.Stop();
        }
        
        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}