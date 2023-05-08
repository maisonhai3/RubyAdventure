using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DesignPatterns
{
    public class BrokenRobot : NPC 
    {

        public override void Awake()
        {
            // Do what the base class does
            base.Awake();
            
            wanderBehavior = new FreeWander();
        }
        private void Start()
        {
            wanderBehavior = new FourDirectionWander();
        }

        private void FixedUpdate()
        {
            // Move the robot the the position that wanderBehavior.NextDirection() returns
            // rigidBody.MovePosition(rigidBody.position + wanderBehavior.NextDirection());
            
            // Add a random force to the robot
            rigidBody.AddForce(wanderBehavior.NextDirection());
        }
    }
}