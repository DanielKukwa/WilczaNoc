using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDisassemble : MonoBehaviour
{
    [SerializeField] private float _timeToStop = 3f;
    private void Start()
    {
        CharacterJoint[] joints = GetComponentsInChildren<CharacterJoint>();
        for(int i = 0; i < joints.Length; i++)
        {
            int rnd = Random.Range(0, 1 + 1);
            if (rnd == 1)
            {
                Destroy(joints[i].gameObject.GetComponent<CharacterJoint>());
                
            }
           // Rigidbody rb = joints[i].gameObject.GetComponent<Rigidbody>();
            //StartCoroutine(DisableGravity(rb, _timeToStop));
        }
        Debug.Log(joints.Length);
    }

    private IEnumerator DisableGravity(Rigidbody rb, float time)
    {
        yield return new WaitForSeconds(time);
        rb.isKinematic = true;
    }
    
}
