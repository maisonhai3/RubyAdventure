using UnityEngine;

namespace Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public AudioClip launchClip;
        public AudioClip hitClip;
        
        private Rigidbody2D rigidbody2D;
        private AudioSource audioSource;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (transform.position.magnitude > 1000.0f)
                Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Fix();
            }
            
            enemy.PlaySound(hitClip);
            
            Destroy(gameObject);
        }

        public void Launch(Vector2 direction, float force)
        {
            rigidbody2D.AddForce(direction * force);
            audioSource.PlayOneShot(launchClip);
        }
    }
}