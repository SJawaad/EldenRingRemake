using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        float vertical;
        float horizontal;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            character.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true)
        {
            character.animator.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            // CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTION
            // FOR EXAMPLE IF YOU GET DAMAGED AND PERFORMING A DAMAGE ANIMATION, PREVENT FROM DOING ANY OTHER ACTION
            character.isPerformingAction = isPerformingAction;
        }
    }
}