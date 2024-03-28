using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBallScript : MonoBehaviour
{

    public MovementScript movementScript; 
    Transform target;
    private bool lockedTarget = false; 
    private bool targetFound = false; 
    public Transform enemy; 
    EnemyScript enemyScript;

    public int fireballDamage; 

    [SerializeField] private float fireBallSpeed = 5f; 

    void Start()
    {
        movementScript = FindAnyObjectByType<MovementScript>();
        target = movementScript.enemy; 

        if (target != null && movementScript.lockedEnemy == true)
        {
            lockedTarget = true;
        }
        else
        {
            lockedTarget = false; 
        }
    }


    void FixedUpdate()
    {
        if (lockedTarget == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, fireBallSpeed * Time.deltaTime);
        }
        
        else if(lockedTarget == false && targetFound == false)
        {
            transform.position += transform.forward * fireBallSpeed * Time.deltaTime; 
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.position, fireBallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PullZone"))
        {
            targetFound = true; 
            enemy = other.GetComponentInParent<Transform>(); 
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyScript = collision.gameObject.GetComponent<EnemyScript>();

            if (enemyScript != null)
            {
                enemyScript.health -= fireballDamage; 
            }

            Destroy(gameObject);
        }
    }
}
