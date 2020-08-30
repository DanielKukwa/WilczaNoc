﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextures : MonoBehaviour
{
    public static PlayerTextures Instance;

    [SerializeField] private Texture[] _textures;
    [SerializeField] private Material _material;
    [SerializeField] private float _baseCoatColor = 0.01176471f;
    [SerializeField] private float _lastCoatColor = 0.02352941f;

    private int _currentTex = 0;
    // Start is called before the first frame update

    private void Awake()
    {       
            Instance = this;     
    }

    void Start()
    {
        _currentTex = 0;
        _material.SetTexture("_BaseColorMap", _textures[_currentTex]);
        _material.SetTexture("_EmissiveColorMap", _textures[_currentTex]);
        _material.SetColor("_EmissiveColor", new Color(_baseCoatColor, _baseCoatColor, _baseCoatColor, 1));
    }

    public void ChangeHoodTexture()
    {
        _currentTex++;
        if (_currentTex >= _textures.Length) _currentTex = _textures.Length - 1;
        if (_currentTex == 4)
        {
            _material.SetColor("_EmissiveColor", new Color(_lastCoatColor, _lastCoatColor, _lastCoatColor, 1));
        }
        else
        {
            _material.SetColor("_EmissiveColor", new Color(_baseCoatColor, _baseCoatColor, _baseCoatColor, 1));
        }
        _material.SetTexture("_BaseColorMap", _textures[_currentTex]);
        _material.SetTexture("_EmissiveColorMap", _textures[_currentTex]);
    }
}
