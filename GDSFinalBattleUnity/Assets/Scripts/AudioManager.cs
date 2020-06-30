using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _audioWolfDie;
    [SerializeField] private AudioSource _audioWolfHits;

    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioClip[] _wolfHitSFX;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayWolfPack()
    {
        _audio.PlayOneShot(_audioClips[0]);
    }

    public void PlayWolfDie()
    {
        _audioWolfDie.PlayOneShot(_audioClips[1]);
    }

    public void PlayWolfHit()
    {
        int random = Random.Range(0, _wolfHitSFX.Length);
        _audioWolfDie.PlayOneShot(_wolfHitSFX[random]);
    }
}
