using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float verticalSpeed = 2f;
    [SerializeField] float acceleration = 2f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxPressure = 10f;
    [SerializeField] Gun gun;

    private Animator myAnimator;

    // Cached component referencek
    private Rigidbody2D myRigidBody;
    private float currentHealth;
    private float currentPressure;
    private float pressureDamage = 10;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;
        currentPressure = maxPressure;
    }

    void Update()
    {
        Run();
        Descend();
        FlipSprite();
        UpdatePressure();

        if (currentHealth < 0) {
            Destroy(this.gameObject);
        }
        Shoot();
    }

    public float getHealthFraction() {
        return currentHealth / maxHealth;
    }

    public float getPressureFraction() {
        return currentPressure / maxPressure;
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity; 

        if (controlThrow > 0)
        {
            playerVelocity = new Vector2(myRigidBody.velocity.x + controlThrow * runSpeed * Time.deltaTime * acceleration, myRigidBody.velocity.y);
        }
        else if (controlThrow < 0)
        {
            playerVelocity = new Vector2(myRigidBody.velocity.x + controlThrow * runSpeed * Time.deltaTime * acceleration, myRigidBody.velocity.y);
        }
        else
        {
            playerVelocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y);
        }

        SetVelocity(playerVelocity);
        
        bool playerHorizSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHorizSpeed);
    }

    private void Descend()
    {
        float yVelocity;
        
        if (CrossPlatformInputManager.GetAxis("Vertical") <= 0)
        {
            yVelocity = myRigidBody.velocity.y - verticalSpeed * Time.deltaTime * acceleration;
        }
        else
        {
            yVelocity = myRigidBody.velocity.y + verticalSpeed * Time.deltaTime * acceleration;
        }
        
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, yVelocity);
    }

    public void UpdatePressure() {

        var descendSpeed = myRigidBody.velocity.y;

        var restoredPressure = Time.deltaTime;
        var descendIncrease = - descendSpeed * Time.deltaTime;
        var diffPressure = restoredPressure - descendIncrease;
        currentPressure += diffPressure;

        if (currentPressure > maxPressure) {
            currentPressure = maxPressure;
        } else if (currentPressure < 0) {
            currentHealth += currentPressure * pressureDamage;
            currentPressure = 0;
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
        var enemyDamage = collision.gameObject.GetComponent<EnemyDamage>();
        if (enemyDamage != null)
        {
            currentHealth -= enemyDamage.damage;
            if (enemyDamage.destroyOnContact) {
                Destroy(enemyDamage.gameObject);
            }
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Recoil();
            gun.Shoot();
        }
    }

    private void Recoil()
    {
        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var movementDirection = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        var reverse = -movementDirection;
        var velocity = new Vector2(myRigidBody.velocity.x + reverse.x, myRigidBody.velocity.y + reverse.y);
        SetVelocity(velocity);
    }

    private void SetVelocity(Vector2 velocity)
    {
        if (velocity.x > runSpeed)
        {
            velocity.x = runSpeed;
        }
        else if (velocity.x < -runSpeed)
        {
            velocity.x = -runSpeed;
        }

        if (velocity.y > verticalSpeed)
        {
            velocity.y = verticalSpeed;
        }
        else if (velocity.y < -verticalSpeed)
        {
            velocity.y = -verticalSpeed;
        }

        myRigidBody.velocity = velocity;
    }
}
