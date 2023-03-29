using UnityEngine;

namespace DesignPatterns
{
    public class GreenFog : NPC
    {
        private void Awake()
        {
            wanderBehavior = new FreeWander();
            rigidBody = gameObject.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // Move the robot the the position that wanderBehavior.NextDirection() returns
            rigidBody.MovePosition(rigidBody.position + wanderBehavior.NextDirection());
        } 
    }
}