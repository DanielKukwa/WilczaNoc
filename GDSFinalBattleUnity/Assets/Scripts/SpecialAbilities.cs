using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpecialAbilities : MonoBehaviour
{

    CharacterStats myStats;
    PlayerManager playerManager;
    Transform playerPosition;
    Camera cam;
    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        playerManager = PlayerManager.instance;
        playerPosition = playerManager.player.GetComponent<Transform>();
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            myStats.Heal();


        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 100))
            {

                FaceMousePoint(hit.point);
                transform.position = hit.point;
                agent.SetDestination(transform.position);
                //agent.stoppingDistance = 0f;
                //agent.updateRotation = true;
            }

        }


    }

    private void FaceMousePoint(Vector3 hitPoint)
    {
        Vector3 direction = (hitPoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 500f);

    }


}
