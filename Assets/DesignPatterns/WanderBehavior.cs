using UnityEngine;

namespace DesignPatterns
{
    public interface IWanderBehavior
    {
        public Vector2 NextDirection();
    }
    
    public class FourDirectionWander : IWanderBehavior
    {
        public Vector2 NextDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
    
    public class FreeWander : IWanderBehavior
    {
        public Vector2 NextDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}