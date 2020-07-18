using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDisassemble : MonoBehaviour
{
    [SerializeField] GameObject _particles;
    private GameObject _player;
    [SerializeField] private float _hitForce = 220f;
    [SerializeField] private float _timeToStop = 3f;

    private void Start()
    {
        _player = PlayerManager.instance.player;

        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        Vector3 direction = _player.transform.position - transform.position;
        rb.AddForce(-direction * _hitForce, ForceMode.Impulse);

        CharacterJoint[] joints = GetComponentsInChildren<CharacterJoint>();

        //int rand = Random.Range(0, joints.Length);
        //CharacterJoint joint = joints[rand];
        //Vector3 anchor = joint.anchor;

        //GameObject bloodVein = Instantiate(_particles);
        //bloodVein.transform.parent = joint.transform;
        //bloodVein.transform.localPosition = joint.anchor;
        //bloodVein.transform.localScale = Vector3.one;
        //bloodVein.transform.rotation = Quaternion.LookRotation(Vector3.up);
        //Destroy(joint.gameObject.GetComponent<CharacterJoint>());
        for(int i = 0; i < joints.Length; i++)
        {
            int rnd = Random.Range(0, 1 + 1);
            if (rnd == 1)
            {
                CharacterJoint joint = joints[i];
                Vector3 anchor = joint.anchor;

                GameObject bloodVein = Instantiate(_particles);
                bloodVein.transform.parent = joint.transform;
                bloodVein.transform.localPosition = joint.anchor;
                bloodVein.transform.localScale = Vector3.one;
                bloodVein.transform.rotation = Quaternion.LookRotation(Vector3.up);
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
