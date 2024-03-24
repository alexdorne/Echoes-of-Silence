using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    private Vector3 input;

    [SerializeField] private Rigidbody rb; 
    [SerializeField] private float movementSpeed; 
    [SerializeField] private float rotationSpeed; 
    [SerializeField] private float sprintSpeed; 
    [SerializeField] private float walkSpeed;

    [SerializeField] private Transform enemy; 

    public bool lockedEnemy = false; 

    
    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Sprint(); 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockedEnemy =! lockedEnemy; 
        }

        if (lockedEnemy == false)
        {
            Look(); 
        }
        else
        {
            EnemyLock();
        }

    }

    private void FixedUpdate()
    {
        Move();  
    }
    void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
    }

    void Look()
    {
        if (input == Vector3.zero) 
            return; 

        var rotation = Quaternion.LookRotation(input, Vector3.up); 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime); 
        
    }
    void Move()
    {

        Vector3 movementDirection = new Vector3(input.x, 0, input.z); 
        movementDirection.Normalize();
        rb.MovePosition(transform.position + movementDirection * movementSpeed * Time.deltaTime); 
 
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
        }
        else
        {
            movementSpeed = walkSpeed; 
        }
    }

    void EnemyLock()
    {
        Vector3 lookToEnemy = enemy.position - transform.position; 
        Vector3 unitLookToEnemy = lookToEnemy.normalized;

        var rotation = Quaternion.LookRotation(unitLookToEnemy, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime); 
    }
}
