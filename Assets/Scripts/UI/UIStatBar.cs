using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JM
{
    public class UIStatBar : MonoBehaviour
    {
        // VARIABLE TO SCALE BAR SIZE DEPENDING ON STAT (HIGER STAT = LONGER BAR)
        private Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public virtual void SetStat()
        {

        }

        public virtual void SetMaxStat()
        {

        }
    }
}