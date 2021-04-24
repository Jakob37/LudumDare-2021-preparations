using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    private Rigidbody2D myRigidBody;
    private EnemyHealth health;
    private Animator myAnimator;
    private float startDelay = 3f;
    private float currentSpeed = 0f;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (startDelay > 0) {
            startDelay -= Time.deltaTime;
            myAnimator.SetBool("isMoving", false);
        } else {
            currentSpeed = moveSpeed;
            myAnimator.SetBool("isMoving", true);
        }

        if (IsFacingRight()) {
            myRigidBody.velocity = new Vector2(currentSpeed, 0);   
        } else {
            myRigidBody.velocity = new Vector2(-currentSpeed, 0);   
        }
    }

    private bool IsFacingRight() 
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);

    }
}
