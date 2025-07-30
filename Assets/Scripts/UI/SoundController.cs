using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{   //разделить логику отражения и основную

    [SerializeField] private float _defaultSoundEffectsVolume = 0.5f;
    [SerializeField] private float _defaultBackgroundMusicVolume = 0.5f;

    [SerializeField] private List<AudioClip> _sounds;

    [SerializeField] private Slider _backgroundMusicSlider;
    [SerializeField] private Slider _soundEffectsSlider;

    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private AudioSource _soundEffectsSource;

    private void Awake()
    {
        SetSourcesDefaultValue();
        _backgroundMusicSlider.onValueChanged.AddListener(ChangeBGVolume);
        _soundEffectsSlider.onValueChanged.AddListener(ChangeSEVolume);
    }

    public void PlaySound(string soundName)
    {
        _soundEffectsSource.Stop();
        AudioClip clip = FindSound(soundName);
        _soundEffectsSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        _soundEffectsSource.Stop();
    }

    private AudioClip FindSound(string soundName)
    {
        foreach (AudioClip clip in _sounds)
        {
            if (clip.name == soundName) return clip;
        }

        return null;
    }

    private void SetSourcesDefaultValue()
    {
        ChangeVolume(_backgroundMusicSource, _defaultBackgroundMusicVolume);
        ChangeVolume(_soundEffectsSource, _defaultSoundEffectsVolume);
    }

    private void ChangeSEVolume(float volume)
    {
        ChangeVolume(_soundEffectsSource, volume);
        PlayTestEffect();
    }

    private void ChangeBGVolume(float volume)
    {
        ChangeVolume(_backgroundMusicSource, volume);
    }

    private void ChangeVolume(AudioSource source, float volume)
    {
        source.volume = volume;
    }

    private void PlayTestEffect()
    {
        if (!_soundEffectsSource.isPlaying)
            PlaySound(SoundName.Diamond.ToString());
    }
}
