using UnityEngine;

namespace DesignPatterns
{
    public class NPC: MonoBehaviour
    {
        public IWanderBehavior wanderBehavior;
        
        // ====== ENEMY MOVEMENT ========
        public Rigidbody2D rigidBody;
        
        public float speed;
        public float TimeToChange = 2;
        public bool horizontal;
        private float remainingTimeToChange;
        private Vector2 direction = Vector2.right;

        // ===== STATE ========
        private bool repaired;

        // ===== ANIMATION ========
        private Animator animator;
        public GameObject smokeParticleEffect;
        public ParticleSystem fixedParticleEffect;

        // ================= SOUNDS =======================
        public AudioClip hitSound;
        public AudioClip fixedSound;
        private AudioSource audioSource;

        public virtual void Awake()
        {
            remainingTimeToChange = TimeToChange;

            direction = horizontal ? Vector2.right : Vector2.down;

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (repaired)
                return;

            remainingTimeToChange -= Time.deltaTime;

            if (remainingTimeToChange <= 0)
            {
                remainingTimeToChange += TimeToChange;
                direction *= -1;

                GenerateNewSpeed();
            }

            animator.SetFloat("ForwardX", direction.x);
            animator.SetFloat("ForwardY", direction.y);
        }

        private void GenerateNewSpeed()
        {
            TimeToChange = Random.Range(1, 5);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (repaired)
                return;

            var controller = other.collider.GetComponent<RubyController>();

            if (controller != null)
                controller.ChangeHealth(-1);
        }

        public void Fix()
        {
            animator.SetTrigger("Fixed");
            repaired = true;

            smokeParticleEffect.SetActive(false);

            Instantiate(fixedParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            //we don't want that enemy to react to the player or bullet anymore, remove its rigidbody from the simulation
            rigidBody.simulated = false;

            audioSource.Stop();
            audioSource.PlayOneShot(hitSound);
            audioSource.PlayOneShot(fixedSound);
        }
    }
}