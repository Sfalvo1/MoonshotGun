using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Sounds
    {
        ShotgunShot,
        LaserShot,
        PlasmaShot,
        ShipExplosion,
    }

    private AudioSource audioSource;
    private Dictionary<Sounds, AudioClip> soundAudioClipDictionary;
    private float soundEffectVolume = .1f;
    private float musicVolume = .5f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sounds, AudioClip>();

        foreach (Sounds sound in System.Enum.GetValues(typeof(Sounds)))
        {
         soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sounds sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], soundEffectVolume);
    }

    //public void PlaySound(Sounds sound, float volume)
    //{
    //    audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    //    soundEffectVolume = volume;
    //}

}
