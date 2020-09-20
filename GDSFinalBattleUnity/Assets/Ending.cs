using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private Animator _blackScreen;
    private AudioSource _audio;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void Start()
    {
        _blackScreen.SetTrigger("FadeOut");
        StartCoroutine(DestroyPlayer());
    }

    IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(1f);
        PlayerManager.instance.player.GetComponent<PlayerController>().enabled = false;
        PlayerManager.instance.player.GetComponent<SpecialAbilities>().enabled = false;
        yield return new WaitForSeconds(12f);
        SceneLoader loader = FindObjectOfType<SceneLoader>();
        loader.LoadStartMenu();
    }
}
