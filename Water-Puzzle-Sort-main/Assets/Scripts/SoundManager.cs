using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("UI")]
    public Slider volSlider;
    float vol;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips - Assign in Inspector or auto-loaded from Resources")]
    public AudioClip bgmClip;
    public AudioClip pourWaterClip;
    public AudioClip buttonClickClip;
    public AudioClip levelClearClip;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Auto-create AudioSources if not assigned
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.playOnAwake = false;
            bgmSource.volume = 0.3f;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }

        // Auto-load clips from Resources/Audio if not assigned in Inspector
        if (bgmClip == null)
            bgmClip = Resources.Load<AudioClip>("Audio/kawaiiwork-lovely-garden-540008");

        if (pourWaterClip == null)
            pourWaterClip = Resources.Load<AudioClip>("Audio/floraphonic-pour-water-1-198288");

        if (buttonClickClip == null)
            buttonClickClip = Resources.Load<AudioClip>("Audio/the-sound-designer-electtic-button-on-sound-a-fl-mastyer-edited-and-final-520904");

        if (levelClearClip == null)
            levelClearClip = Resources.Load<AudioClip>("Audio/freesound_gamestudio-clear-combo-7-394494");
    }

    void Start()
    {
        if (volSlider != null)
        {
            volSlider.value = AudioListener.volume;
        }

        // Play BGM on start
        PlayBGM();
    }

    void Update()
    {

    }

    // ==================== BGM ====================
    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            if (!bgmSource.isPlaying)
            {
                bgmSource.Play();
            }
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // ==================== SFX ====================
    public void PlayPourWater()
    {
        PlaySFX(pourWaterClip);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickClip);
    }

    public void PlayLevelClear()
    {
        PlaySFX(levelClearClip);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // ==================== Volume Controls ====================
    public void ChangeVolume()
    {
        if (volSlider != null)
        {
            AudioListener.volume = volSlider.value;
            vol = volSlider.value;
        }
    }

    public void MuteVolume()
    {
        AudioListener.volume = 0;
    }

    public void OpenVolume()
    {
        AudioListener.volume = vol;
    }
}
