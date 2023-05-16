using UnityEngine;

namespace Gameplay
{
    public class HealthCollectible : MonoBehaviour
    {
        public AudioClip collectedClip;
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"Object {col.gameObject.name} entered the trigger");
            
            var rubyController = col.GetComponent<RubyController>();
            
            if (rubyController == null) return;
            if (rubyController.health > rubyController.maxHealth) return;
            
            rubyController.ChangeHealth(1);
            rubyController.PlaySound(collectedClip);
            
            Destroy(gameObject);
        }
    }
}