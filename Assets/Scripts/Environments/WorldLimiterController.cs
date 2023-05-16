using System;
using UnityEngine;

namespace Environments
{
    public class WorldLimiterController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            // Prevent other objects from leaving the world
            // if (other.gameObject.CompareTag("Player"))
            // {
            //     other.gameObject.transform.position = new Vector3(0, 0, 0);
            // }
        }
    }
}