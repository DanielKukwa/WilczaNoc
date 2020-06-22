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

    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField] private float dashDistance = 5f;
    //[SerializeField] private float dashForce = 5;
    [SerializeField] private float dashDuration;
    [SerializeField] private AnimationCurve jumpCurve;
    private float elapsedTime = 0;

    Rigidbody rb;

 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Blink();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            

            if (Physics.Raycast(ray, out hit, 100))
            {
                               
                startPosition = transform.position;
                targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                elapsedTime = 0;
                StartCoroutine(Dash());
                FaceMousePoint(hit.point);
               
            }
        }


    }

    public IEnumerator Dash()
    {
        while (elapsedTime <= 1)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dashDuration;
            float curveValue = jumpCurve.Evaluate(t);
            transform.position = Vector3.LerpUnclamped(startPosition, targetPosition, curveValue);
            Debug.Log("DASH!");
            agent.SetDestination(transform.position);
            yield return null;
          
        }
    }

    private void Blink()
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

    private void FaceMousePoint(Vector3 hitPoint)
    {
        Vector3 direction = (hitPoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 500f);

    }


}
