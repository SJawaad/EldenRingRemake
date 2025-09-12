using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            CalculateTotalHealthBasedOnVigorLevel(player.playerNetworkManager.vigor.Value);
            CalculateTotalStaminaBasedOnEnduranceLevel(player.playerNetworkManager.endurance.Value);
        }
    }
}