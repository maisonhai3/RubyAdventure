using System;
using UnityEngine;

namespace Gameplay
{
    public class NonPlayerCharacter : MonoBehaviour
    {
        public float displayTime = 3f;
        public GameObject dialogBox;
        
        private float timerDisplay;

        private void Start()
        {
            dialogBox.SetActive(false);
            timerDisplay = -1.0f;
        }

        private void Update()
        {
            if (timerDisplay >= 0)
            {
                timerDisplay -= Time.deltaTime;
                if (timerDisplay < 0)
                {
                    dialogBox.SetActive(false);
                }
            }
        }
        
        public void DisplayDialog()
        {
            dialogBox.SetActive(true);
            timerDisplay = displayTime;
        }
    }
}