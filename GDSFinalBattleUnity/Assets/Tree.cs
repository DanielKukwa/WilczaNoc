using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private CharacterStats _treestats;
    // Start is called before the first frame update
    void Start()
    {
        _treestats = GetComponent<CharacterStats>();
        _treestats.OnCharacterDie += OnTreeDie;
    }

    public void OnTreeDie()
    {
        Destroy(gameObject);
    }
}
