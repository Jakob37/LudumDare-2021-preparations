using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpStrength = 250f;
    [SerializeField] float hoverStrength = 0.5f;
    [SerializeField] Vector2 recoilStrength = new Vector2(25f, 50f);
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxPressure = 10f;
    [SerializeField] Gun gun;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float movementSpeed = 5f;

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
        float moveForce = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * 60;
        myRigidBody.AddForce(transform.right * moveForce * movementSpeed);
    }

    private void Descend()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myRigidBody.IsTouchingLayers(groundLayerMask))
        {
            myRigidBody.AddForce(transform.up * jumpStrength);
        }

        // TODO: Change me to other button!
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            myRigidBody.AddForce(transform.up * hoverStrength);
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
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var movementDirection = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            gun.Shoot();
            Recoil(movementDirection);
        }
    }

    private void Recoil(Vector2 projectileDirection)
    {
        myRigidBody.AddForce(-projectileDirection * recoilStrength);
    }
}
