using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBlock : MonoBehaviour
{

    [SerializeField] private float noteSpeed; 
    public bool goodNote = false; 
    public bool successHit = false; 

    public bool playableNote = false; 
    MovementScript movementScript;

    private void Start()
    {
        movementScript = FindAnyObjectByType<MovementScript>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playableNote == true)
        {
            GameObject fireBall = Instantiate(movementScript.fireball, movementScript.castPoint.transform.position, movementScript.castPoint.transform.rotation); 
            FireBallScript fireBallScript = fireBall.GetComponent<FireBallScript>();    

            if (goodNote)
            {
                fireBallScript.fireballDamage = 2; 
                Debug.Log("Awesome!"); 
            }

            else
            {
                fireBallScript.fireballDamage = 1; 
                Debug.Log("Bad..."); 
            }
            Destroy(gameObject);
            

        }


               
    }
    void FixedUpdate()
    {
        transform.position -= transform.up * noteSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayableNote"))
        {
            playableNote = true; 
        }
        if (collision.gameObject.CompareTag("End"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkillBar"))
        {
            goodNote = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkillBar"))
        {
            goodNote = false;
        }
    }
}
