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
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxPressure = 10f;

    private Animator myAnimator;

    // Cached component references
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
        Debug.Log("OnTriggerEnter2D");
        var enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            Debug.Log(collision);
            // Destroy(this.gameObject);
            currentHealth -= 10;
            Debug.Log("Current heath " + currentHealth);
        }
    }
}
