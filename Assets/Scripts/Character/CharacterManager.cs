using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
	public class CharacterManager : NetworkBehaviour
	{
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;

        [HideInInspector] public CharacterNetworkManager characterNetworkManager;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }

        protected virtual void Update()
        {
            // IF CHARACTER IS BEING CONTROLLED FROM OUR SIDE, THEN ASSIGN ITS NETWORK POSITION TO THE POSITION OF OUR TRANSFORM
            if (IsOwner)
            {
                characterNetworkManager.networkPosition.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            // IF CHARACTER IS BEING CONTROLLED FROM ELSEWHERE, THEN ASSIGN ITS POSITION LOCALLY BY POSITION OF ITS NETWORK TRANSFORM
            else
            {
                // POSITION
                transform.position = Vector3.SmoothDamp(transform.position, characterNetworkManager.networkPosition.Value, 
                    ref characterNetworkManager.networkVelocity, 
                    characterNetworkManager.networkPositionSmoothTime);

                // ROTATION
                transform.rotation = Quaternion.Slerp(transform.rotation, characterNetworkManager.networkRotation.Value, characterNetworkManager.networkRotationSmoothTime);
            }
        }

        protected virtual void LateUpdate()
        {

        }
    }
}
