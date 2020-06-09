using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilities : MonoBehaviour
{

    CharacterStats myStats;
   

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            myStats.Heal();
            

        }
        
    }

}
