using UnityEngine;

namespace DesignPatterns
{
    public class NPC: MonoBehaviour
    {
        public IWanderBehavior wanderBehavior;
        internal Rigidbody2D rb;
    }
}