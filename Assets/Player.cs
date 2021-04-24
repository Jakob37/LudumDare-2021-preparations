using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float normalDescentSpeed = 2f;
    [SerializeField] float slowDescentSpeed = 1f;
    [SerializeField] float acceleration = 2f;

    private Animator myAnimator;

    // Cached component references
    private Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        Debug.Log("I can debug now!");
    }

    void Update()
    {
        Run();
        Descend();
        FlipSprite();
    }


    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHorizSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHorizSpeed);
    }

    private void Descend()
    {
        float targetVelocityY;

        if (Input.GetKey(KeyCode.Space))
        {
            targetVelocityY = -slowDescentSpeed;
        }
        else
        {
            targetVelocityY = -normalDescentSpeed;
        }

        if (myRigidBody.velocity.y > targetVelocityY)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y + targetVelocityY * Time.deltaTime * acceleration);
        }
        else if (myRigidBody.velocity.y < targetVelocityY)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y - targetVelocityY * Time.deltaTime * acceleration);
        }
        else
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, targetVelocityY);
        }
    }

    private void FlipSprite()
    {
        bool playerHorizSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHorizSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        var enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            Debug.Log(collision);
            Destroy(this.gameObject);
        }
    }
}
