/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
            if (player.isJump)
            {
                float volume = 1f;
                SoundManager.Instance.PlayJumpSound(player.transform.position,volume);
            }
    }
    
}*/