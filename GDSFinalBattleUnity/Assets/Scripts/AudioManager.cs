using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _audioRedLightHits;
    [SerializeField] private AudioSource _audioImpact;
    [SerializeField] private AudioSource _audioWolfAttack;
    [SerializeField] private AudioSource _audioWolfDie;
    [SerializeField] private AudioSource _audioWolfHits;

    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioClip[] _redLightHitsSFX;
    [SerializeField] private AudioClip[] _wolfAttacksSFX;
    [SerializeField] private AudioClip[] _wolfHitSFX;
    [SerializeField] private AudioClip[] _impactSFX;

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
        _audio.PlayOneShot(_audioClips[0], _audio.volume);
    }

    public void PlayWolfDie()
    {
        _audioWolfDie.PlayOneShot(_audioWolfDie.clip, _audioWolfDie.volume);
    }

    public void PlayWolfHit()
    {
        int random = Random.Range(0, _wolfHitSFX.Length);
        _audioWolfDie.PlayOneShot(_wolfHitSFX[random], _audioWolfDie.volume);
    }
    public void PlayWolfAttack()
    {
        int random = Random.Range(0, _wolfAttacksSFX.Length);
        _audioWolfAttack.PlayOneShot(_wolfAttacksSFX[random], _audioWolfAttack.volume);
    }
    public void PlayRedLightHit()
    {
        int random = Random.Range(0, _redLightHitsSFX.Length);
        _audioRedLightHits.PlayOneShot(_redLightHitsSFX[random], _audioRedLightHits.volume);
    }

    public void PlayRedHeavyHit()
    {
       // int random = Random.Range(0, _redLightHitsSFX.Length);
       // _audioWolfDie.PlayOneShot(_redLightHitsSFX[random]);
    }

    public void PlayImpactSounds()
    {
        int random = Random.Range(0, _impactSFX.Length);
        _audioImpact.PlayOneShot(_impactSFX[random], _audioImpact.volume);
    }
}
