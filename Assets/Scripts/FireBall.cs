using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBall : Spells
{

    public override void Activate(GameObject parent)
    {
        Transform playerPosition = parent.GetComponent<Transform>();
        MovementScript movementScript = parent.GetComponent<MovementScript>();
        Transform enemy = movementScript.enemy; 
        
        

        Instantiate(movementScript.fireball, movementScript.castPoint.transform.position, movementScript.castPoint.transform.rotation); 
        
    }
}
