using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private float _fade = 1f;
    [SerializeField] private List<AudioClipList> _clipList = null;

    private List<AudioSource> _audios = null;
    private Sequence _sequence = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake() => _audios ??= new List<AudioSource>(GetComponentsInChildren<AudioSource>());

    private void Start() { }

    private AudioSource GetAudioSource(string name)
    {
        AudioSource audioSource = null;
        bool stop = false;
        _audios.ForEach(aus =>
        {
            if (stop) return;

            if (aus.name.Equals(name))
            {
                audioSource = aus;
                stop = true;
            }
        });

        return audioSource;
    }

    private AudioClip GetAudioClip(string name)
    {
        AudioClip audioClip = null;
        bool stop = false;
        _clipList.ForEach(aus =>
        {
            if (stop) return;

            if (aus.NameClip.ToLower().Trim().Equals(name.ToLower().Trim()))
            {
                audioClip = aus.Clip;
                stop = true;
            }
        });

        return audioClip ?? _clipList[0].Clip;
    }

    public void SwitchMusic(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        
        AudioSource source = GetAudioSource("Music");

        if (source.isPlaying && source.clip == GetAudioClip(name))
            return;
        
        _sequence = DOTween.Sequence();
        _sequence.Append(source.DOFade(0, _fade)).OnComplete(() =>
        {
            source.Stop();
            source.clip = GetAudioClip(name);
            source.Play();
        });
        _sequence.Append(source.DOFade(_singleton.Data.Settings.Music, _fade));
        _sequence.Play();
    }

    public void SwitchSound(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        AudioSource source = GetAudioSource("Sound");

        if (source.isPlaying && source.clip == GetAudioClip(name))
            return;

        source.Pause();
        source.clip = GetAudioClip(name);
        source.Play();
    }

    public void OneShot(string name) => GetAudioSource("Sound").PlayOneShot(GetAudioClip(name));

    public void PlayWalk(string name)
    {
        AudioSource source = GetAudioSource("Walk");

        if (source.isPlaying && source.clip == GetAudioClip(name))
            return;

        source.Stop();
        source.clip = GetAudioClip(name);
        source.Play();
    }
    public void StopWalk()
    {
        AudioSource source = GetAudioSource("Walk");
        if (source.isPlaying)
            source.Stop();
    }
}

[System.Serializable]
public class AudioClipList
{
    [SerializeField] private string _nameClip = null;
    [SerializeField] private AudioClip _clip = null;

    public string NameClip => _nameClip;
    public AudioClip Clip => _clip;
}
