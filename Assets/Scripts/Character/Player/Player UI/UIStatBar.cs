using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JM
{
    public class UIStatBar : MonoBehaviour
    {
        private Slider slider;
        private RectTransform rectTransform;

        // VARIABLE TO SCALE BAR SIZE DEPENDING ON STAT (HIGER STAT = LONGER BAR)
        [Header("Bar Settings")]
        [SerializeField] protected bool scaleBar = true;
        [SerializeField] protected float scaleMultiplier = 1f;


        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void SetStat(float newValue)
        {
            slider.value = newValue;
        }

        public virtual void SetMaxStat(float maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;

            if (scaleBar)
            {
                rectTransform.sizeDelta = new Vector2(maxValue * scaleMultiplier, rectTransform.sizeDelta.y);
                // RESET HUD POSITION AFTER SCALING
                PlayerUIManager.instance.playerUIHudManager.RefreshHUD();
            }
        }
    }
}