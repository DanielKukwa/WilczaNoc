using UnityEngine;

public class BloodDecay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randZ = Random.Range(0, 359);
        transform.rotation = Quaternion.Euler(0, 0, randZ);
    }
}
