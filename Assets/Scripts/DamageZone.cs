using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.GetComponent<RubyController>();
        if (controller != null) controller.ChangeHealth(-1);
    }
}