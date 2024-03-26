using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTextScript : MonoBehaviour
{

    [SerializeField] private Transform enemy; 
    // Start is called before the first frame update
    //void Start()
    //{
    //    enemy = transform.root; 
    //}

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.position;
    }
}
