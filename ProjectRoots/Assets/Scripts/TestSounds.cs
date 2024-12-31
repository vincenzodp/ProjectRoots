using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSounds : MonoBehaviour
{
    private List<AudioSource> playingSounds = new List<AudioSource>();

    public Slider sliderSoundEffects;
    public Slider sliderSoundMusic;

    private float volumeUpdateTimer = 0f; 



    private void Awake()
    {
        SoundManager.Initialize();
    }

    private void Update()
    {
        if(volumeUpdateTimer < 0f)
        {
            volumeUpdateTimer = .1f;
            SoundEffectVolumeUpdate();
            SoundMusicVolumeUpdate();
        }
        else
        {
            volumeUpdateTimer -= Time.deltaTime;
        }
    }

    public void SoundEffectVolumeUpdate()
    {
        SoundManager.effectsVolume = sliderSoundEffects.value;
    }

    public void SoundMusicVolumeUpdate()
    {
        SoundManager.musicVolume = sliderSoundMusic.value;
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
        SoundManager.PlaySoundEffect(SoundManager.Sound.EnemyHit);
    }

    public void PlayTurretShootSound()
    {
        SoundManager.PlaySoundEffect(SoundManager.Sound.TurretShoot);
    }

    public void PlayMainThemeSound()
    {
        AudioSource audioSource = (SoundManager.PlaySoundStoppable(SoundManager.Sound.MainTheme));
        if(audioSource != null)
        {
            playingSounds.Add(audioSource);
        }
        
    }

    public void PlayEnemyAttackSound()
    {
        SoundManager.PlaySoundEffect(SoundManager.Sound.EnemyAttack);
    }
}
