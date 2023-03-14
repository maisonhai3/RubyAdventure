using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Object {col.gameObject.name} entered the trigger");
        
        var rubyController = col.GetComponent<RubyController>();
        
        if (rubyController == null) return;
        if (rubyController.Health > rubyController.maxHealth) return;
        
        rubyController.ChangeHealth(1);
        Destroy(gameObject);
    }
}