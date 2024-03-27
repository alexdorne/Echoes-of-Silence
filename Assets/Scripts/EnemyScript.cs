using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health; 
    public int maxHealth; 


    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }


}
