using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] EventTrigger _eventTrigger;
    [SerializeField] Transform _destination;
    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger.OnEventTrigger += MovePlayerToPoint;
    }

    // Update is called once per frame
    private void MovePlayerToPoint()
    {
        PlayerManager.instance.player.transform.position = _destination.position;
        NavMeshAgent agent = PlayerManager.instance.player.GetComponent<NavMeshAgent>();
        agent.Warp(_destination.position);
    }
}
