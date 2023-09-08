using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    public float volumeSounds;
    private void Awake()
    {
        Instance = this;
        volumeSounds = PlayerPrefs.GetFloat("SoundsVolume");
    }
    

    private void PlaySound(AudioClip[] audioClipArray,Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volumeMultiplier * volumeSounds);
    }

    public void PlayJumpSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.jump,position, volume);
    }

}
