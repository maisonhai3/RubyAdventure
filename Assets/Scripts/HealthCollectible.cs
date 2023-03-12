using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

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