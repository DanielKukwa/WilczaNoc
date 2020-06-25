using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private EventTrigger _eventTrigger;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

        if (_eventTrigger)
        {
            _eventTrigger.OnEventTrigger += OnTriggerActive;
        }
    }

    private void OnTriggerActive()
    {
        Instantiate(_enemyToSpawn, transform.position, Quaternion.identity);
    }
}
