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

    
    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look(); 
        Sprint(); 
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



        var rotation = Quaternion.LookRotation(input.ToIso(), Vector3.up); 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime); 
        
    }
    void Move()
    {
        rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * movementSpeed * Time.deltaTime); 
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
}
