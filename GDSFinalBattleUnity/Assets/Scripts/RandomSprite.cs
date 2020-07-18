using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        int rand = Random.Range(0, _sprites.Length);
        _renderer.sprite = _sprites[rand];
    }
}
