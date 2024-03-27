using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    

    [SerializeField] private TMP_Text lockText;

    [SerializeField] private GameObject cameraPivot;
    [SerializeField] private float smoothCamRotate; 

    public Transform enemy; 
    public GameObject fireball; 
    public GameObject castPoint; 

    public bool lockedEnemy = false; 

    
    // Update is called once per frame
    void Update()
    {

        // Gathers Player Input
        GatherInput();

        // Sprinting Function
        Sprint(); 

        // Pressing Space locks onto enemy

        if (Input.GetKeyDown(KeyCode.Space) && enemy != null)
        {
            lockedEnemy =! lockedEnemy; 
        }

        if (lockedEnemy == true && enemy == null)
        {
            lockedEnemy = false;
            lockText.gameObject.SetActive(false); 
        }

        if (lockedEnemy == false)
        {
            Look();

            Quaternion normalRotation = Quaternion.Euler(30, 0, 0); 

            cameraPivot.transform.rotation = Quaternion.Slerp(cameraPivot.transform.rotation, normalRotation, smoothCamRotate * Time.deltaTime); 
        }
        else
        {
            EnemyLock();

            Quaternion lockedRotation = Quaternion.Euler(45, 0, 0); 
            cameraPivot.transform.rotation = Quaternion.Slerp(cameraPivot.transform.rotation, lockedRotation, smoothCamRotate * Time.deltaTime); 
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
        // Default Player Rotation 

        if (input == Vector3.zero) 
            return; 

        var rotation = Quaternion.LookRotation(input, Vector3.up); 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime); 
        
    }
    void Move()
    {
        // Default Player Movement

        Vector3 movementDirection = new Vector3(input.x, 0, input.z); 
        movementDirection.Normalize();
        rb.MovePosition(transform.position + movementDirection * movementSpeed * Time.deltaTime); 
 
    }

    void Sprint()
    {
        // Sprinting Mechanic 

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
        // Enemy Lock changes the player rotation to point at an enemy that is in range


        Vector3 lookToEnemy = enemy.position - transform.position; 
        Vector3 unitLookToEnemy = lookToEnemy.normalized;

        var rotation = Quaternion.LookRotation(unitLookToEnemy, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime); 

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LockZone"))
        {
            enemy = other.GetComponentInParent<Transform>(); 
        }

        lockText.gameObject.SetActive(true);

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LockZone"))
        {
            enemy = null; 
            lockedEnemy = false; 
        }

        lockText.gameObject.SetActive(false); 
    }
}
