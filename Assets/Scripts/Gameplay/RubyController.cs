using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class RubyController : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public AudioClip getHitClip;
        
        private AudioSource audioSource;
        private Animator animator;
        private Rigidbody2D rigidbody2D;
        private BoxCollider2D boxCollider2D;

        private Vector2 lookDirection = new(1, 0);

        public float speed = 3.0f;
        public int maxHealth = 5;
        private int currentHealth;
        public int Health => currentHealth;

        public float timeInvincible = 2.0f;
        private bool isInvincible;
        private float invincibleTimer;

        private float horizontal;
        private float vertical;

        public GameObject worldLimiter;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            boxCollider2D = GetComponent<BoxCollider2D>();

            currentHealth = maxHealth;
            
            GetXYBoundariesFromWorldLimiter();
        }

        private void GetXYBoundariesFromWorldLimiter()
        {
            // Get the world limiter's 4 vertices.
            var vertices = new Vector3[4];
            worldLimiter.GetComponent<MeshFilter>().mesh.vertices.CopyTo(vertices, 0);
        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 move = new(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }

            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
            
            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer <= 0)
                    isInvincible = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
                Launch();
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                var hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
                if (hit.collider != null)
                {
                    var npc = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (npc != null)
                    {
                        npc.DisplayDialog();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            var position = rigidbody2D.position;
            position.x += speed * horizontal * Time.deltaTime;
            position.y += speed * vertical * Time.deltaTime;

            MovingInBounds(position);
        }

        private void MovingInBounds(Vector2 position)
        {
            rigidbody2D.MovePosition(position);
        }

        public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                animator.SetTrigger("Hit");
                audioSource.PlayOneShot(getHitClip);

                if (isInvincible)
                    return;

                isInvincible = true; // Ruby will be hit in the next physics update.
                invincibleTimer = timeInvincible; // Reset the timer.
            }

            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UIHealthBar.Instance.SetValue(currentHealth / (float) maxHealth);
        }

        private void Launch()
        {
            var projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
            
            var projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);
            
            animator.SetTrigger("Launch");
        }
        
        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }    
}
