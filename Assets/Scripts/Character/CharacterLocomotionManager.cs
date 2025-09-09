using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{   
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Ground Check & Jumping")]
        [SerializeField] float gravityForce = -5.55f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 1f;
        [SerializeField] protected Vector3 yVelocity; // FORCE AT WHICH CHARACTER WILL BE PULLED UP OR DOWN
        [SerializeField] protected float groundedYVelocity = -20; // FORCE AT WHICH CHARACTER IS STICKING TO THE GROUND
        [SerializeField] protected float fallStartYVelocity = -5; // FORCE AT WHICH CHARACTER STARTS TO FALL
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {
            HandleGroundCheck();

            if (character.isGrounded)
            {
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                if (!character.isJumping && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                character.animator.SetFloat("InAirTimer", inAirTimer);

                yVelocity.y += gravityForce * Time.deltaTime;
            }

            // FORCE APPLIED TO CHARACTER
            character.characterController.Move(yVelocity * Time.deltaTime);
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        }

        // DRAWS GROUND CHECK SPHERE RADIUS IN EDITOR
        protected void HandleGroundCheck()
        {
            character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        }

    }
}
