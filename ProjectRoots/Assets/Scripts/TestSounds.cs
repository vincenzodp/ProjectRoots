using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSounds : MonoBehaviour
{
    private List<AudioSource> playingSounds = new List<AudioSource>();
    


    private void Awake()
    {
        SoundManager.Initialize();
    }

    public void StopPlayingSound()
    {
        foreach(AudioSource source in playingSounds)
        {
            // this only works if clip is played using play(), not playoneshot()
            source.Stop();
        }
        playingSounds.Clear(); 
        
        
    }

    public void PlayEnemyHitSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.EnemyHit);
    }

    public void PlayTurretShootSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.TurretShoot);
    }

    public void PlayMainThemeSound()
    {
        AudioSource audioSource = (SoundManager.PlaySoundStoppable(SoundManager.Sound.MainTheme));
        if(audioSource != null)
        {
            playingSounds.Add(audioSource);
        }
        
    }
}
