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
        GameObject spawnedEnemy = Instantiate(_enemyToSpawn, transform.position, Quaternion.identity);
        if(_eventTrigger.gameObject.tag == "WolfHowl")
        {
            DirIcon dirIcon = DirectionIconPool.Instance.Get();
            GameObject canvas = GameObject.FindGameObjectWithTag("DirUI");
            if (!canvas) Debug.Log("brakCanvsu");
            dirIcon.transform.SetParent(canvas.transform);
            dirIcon.gameObject.SetActive(true);
            dirIcon.SetTarget(spawnedEnemy);
        }
        
    }
}
