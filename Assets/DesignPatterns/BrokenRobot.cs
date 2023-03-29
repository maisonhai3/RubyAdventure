using System;
using UnityEngine;

namespace DesignPatterns
{
    public class BrokenRobot : NPC 
    {

        public override void Awake()
        {
            // Do what the base class does
            base.Awake();
            
            wanderBehavior = new FourDirectionWander();
        }
        private void Start()
        {
            wanderBehavior = new FourDirectionWander();
        }

        private void FixedUpdate()
        {
            // Move the robot the the position that wanderBehavior.NextDirection() returns
            rigidBody.MovePosition(rigidBody.position + wanderBehavior.NextDirection());
        }
    }
}