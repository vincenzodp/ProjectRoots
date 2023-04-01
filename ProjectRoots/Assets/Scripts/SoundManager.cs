using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public static float effectsVolume = 1f;
    public static float musicVolume = 1f;

    public enum Sound
    {
        MainTheme,
        TurretShoot,
        EnemyHit,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.MainTheme] = 0f;
    }

    public static void PlaySoundEffect(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = effectsVolume;
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static AudioSource PlaySoundStoppable(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = musicVolume;
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);

        return audioSource;
    }

    public static void PlaySoundLooping(Sound sound)
    {
        GameObject musicGameObject = new GameObject("MainTheme");
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = musicVolume;
        audioSource.loop = true;
        audioSource.Play();

        // Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static void AdjustMusicVolume(float newValue) 
    {
        GameObject.Find("MainTheme").GetComponent<AudioSource>().volume = newValue;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
         foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
         {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
         }
         Debug.LogError("Sound " + sound + " not found in GameAssets prefab");
         return null;
    }
}
