using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBall : Spells
{
    [SerializeField] private int numberOfFireballs = 5;
    [SerializeField] private float skillCheckDuration = 1f; 

    public override void Activate(GameObject parent)
    {
        CoroutineManager.instance.StartCoroutine(CastFireballs(parent));   

        
        
    }

    IEnumerator CastFireballs(GameObject parent)
    {
        Transform playerPosition = parent.GetComponent<Transform>();
        MovementScript movementScript = parent.GetComponent<MovementScript>();
        Transform enemy = movementScript.enemy; 



        movementScript.skillCheck.SetActive(true); 
        for (int i = 0; i < numberOfFireballs; i++)
        { 
            GameObject noteBlock = Instantiate(movementScript.noteBlock, movementScript.noteSpawner.transform.position, movementScript.noteSpawner.transform.rotation, movementScript.skillCheck.transform); 
            NoteBlock noteBlockScript = noteBlock.GetComponent<NoteBlock>();
           

            
            yield return new WaitForSeconds(skillCheckDuration);

        }

        yield return new WaitForSeconds(skillCheckDuration * numberOfFireballs);
        movementScript.skillCheck.SetActive(false); 

    }
}
