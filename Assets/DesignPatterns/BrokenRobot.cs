using System;
using UnityEngine;

namespace DesignPatterns
{
    public class BrokenRobot : NPC 
    {
        
        private void Awake()
        {
            wanderBehavior = new FourDirectionWander();
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // Move the robot the the position that wanderBehavior.NextDirection() returns
            rb.MovePosition(rb.position + wanderBehavior.NextDirection());
        }
    }
}