using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 30;

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log($"Horizontal: {horizontal}");
        
        float vertical = Input.GetAxis("Vertical");
        Debug.Log($"Vertical: {vertical}");
        
        Vector2 rubyPosition = transform.position;
        rubyPosition.x = rubyPosition.x + 5f * horizontal * Time.deltaTime;
        rubyPosition.y = rubyPosition.y + 5f * vertical * Time.deltaTime;
        
        transform.position = rubyPosition;
    }
    
    private IEnumerator WaitAndPrint()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Debug.Log("WaitAndPrint " + Time.time);
    }
    
    private IEnumerable<int> GetNumbers()
    {
        yield return 1;
        yield return 2;
        yield return 3;
    }
}