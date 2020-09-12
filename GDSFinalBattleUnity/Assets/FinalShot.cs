using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalShot : MonoBehaviour
{
    [SerializeField] EventTrigger _eventTrigger;

    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private AudioSource _gunAudio;

    [SerializeField] private float _timeToFade = 4f;
    [SerializeField] private GameObject _final;

    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger.OnEventTrigger += OnTriggerActive;
    }

    // Update is called once per frame
    void OnTriggerActive()
    {
        StartCoroutine(FinalShotScene());
    }

    private IEnumerator FinalShotScene()
    {
        _gunAnimator.SetTrigger("Shot");
        _gunAudio.Play();
        PlayerController playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        playerController.GetComponentInChildren<Animator>().SetTrigger("died");
        DashBar dashbar = playerController.GetComponentInChildren<DashBar>();
        Destroy(dashbar.gameObject);
        Healthbar healthbar = playerController.GetComponentInChildren<Healthbar>();
        Destroy(healthbar.gameObject);
        yield return new WaitForSeconds(_timeToFade);
        _final.gameObject.SetActive(true);
    }
}
