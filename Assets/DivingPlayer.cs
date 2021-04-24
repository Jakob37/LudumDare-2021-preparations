using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DivingPlayer : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float normalDescentSpeed = 2f;
    [SerializeField] float slowDescentSpeed = 1f;

    // Cached component references
    private Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
        Descend();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
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
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y + targetVelocityY / 500);
        }
        else if (myRigidBody.velocity.y < targetVelocityY)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y - targetVelocityY / 500);
        }
        else
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, targetVelocityY);
        }
    }
}
