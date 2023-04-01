using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{


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

    public static void PlaySound(Sound sound)
    {
        if(!CanPlaySound(sound)) { return; }

        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.PlayOneShot(audioSource.clip);
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static AudioSource PlaySoundStoppable(Sound sound)
    {
        if (!CanPlaySound(sound)) { return null; }

        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);

        return audioSource;
    }

    public static void PlaySoundLooping(Sound sound)
    {
        if (!CanPlaySound(sound)) { return; }

        GameObject musicGameObject = new GameObject("Main Music");
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = .1f;
        audioSource.loop = true;
        audioSource.Play();

        // Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static void AdjustMusicVolume(float newValue)
    {
        GameObject.Find("Main Music").GetComponent<AudioSource>().volume = newValue;
    }

    // in here the sounds that cannot be played repeatedly can be set. by default every sound can be played constantly.
    private static bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default: 
                return true;
            case Sound.MainTheme:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float mainThemeTimerMax = 1f; // main theme lasts 71 seconds.
                    if (lastTimePlayed + mainThemeTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                
        }
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
