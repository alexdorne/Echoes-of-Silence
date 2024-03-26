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

        Instantiate(movementScript.fireball, playerPosition); 
        
    }

    public override void BeginCooldown(GameObject parent)
    {
        
    }
}
