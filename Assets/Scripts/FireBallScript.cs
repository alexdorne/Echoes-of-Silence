using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBallScript : MonoBehaviour
{

    MovementScript movementScript; 
    Transform target;

    [SerializeField] private float fireBallSpeed = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponentInParent<MovementScript>();
        target = movementScript.enemy; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, fireBallSpeed * Time.deltaTime);
    }
}
