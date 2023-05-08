using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class UIHealthBar : MonoBehaviour
    {
        public static UIHealthBar Instance { get; private set; }
        
        public Image mask;
        private float originalSize;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            originalSize = mask.rectTransform.rect.width;
        }
        
        public void SetValue(float value)
        {
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
        }
    }
}