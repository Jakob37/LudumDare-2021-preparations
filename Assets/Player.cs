using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpStrength = 250f;
    [SerializeField] Vector2 recoilStrength = new Vector2(25f, 50f);
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxPressure = 10f;
    [SerializeField] Gun gun;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float pressureRecovery = 2f;

    private Animator myAnimator;
    public LevelLoader levelLoader;

    // Cached component referencek
    private Rigidbody2D myRigidBody;
    private SpriteRenderer mySpriteRenderer;
    private float currentHealth;
    private float currentPressure;
    private float pressureDamage = 10;
    private float damageIndicatorTime;
    private float changeToGameOverTime;
    private bool isDead = false;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        currentPressure = maxPressure;

        damageIndicatorTime = Time.time;
        changeToGameOverTime = Time.time;
    }

    void Update()
    {
        if (!isDead) {
            Run();
            Shoot();
        }

        Descend();
        FlipSprite();
        UpdatePressure();
        ResetDamageIndicator();

        if (currentHealth < 0 && !isDead) {
            isDead = true;
            Destroy(gun);
            myAnimator.SetBool("isDead", isDead);

            changeToGameOverTime = Time.time + 3;
        }

        if (isDead && Time.time > changeToGameOverTime) {
            levelLoader.LoadGameOver();
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
        float moveForce = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * 60;
        myRigidBody.AddForce(transform.right * moveForce * movementSpeed);
    }

    private void Descend()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myRigidBody.IsTouchingLayers(groundLayerMask))
        {
            myRigidBody.AddForce(transform.up * jumpStrength);
        }
    }

    public void UpdatePressure() {
        var descendSpeed = myRigidBody.velocity.y;

        var restoredPressure = Time.deltaTime * pressureRecovery;
        var descendIncrease = Math.Max(0, -descendSpeed) * Time.deltaTime;
        var diffPressure = restoredPressure - descendIncrease;

        currentPressure += diffPressure;

        if (currentPressure > maxPressure)
        {
            currentPressure = maxPressure;
        }
        else if (currentPressure < 0)
        {
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

            mySpriteRenderer.color = Color.red;
            damageIndicatorTime = Time.time + 0.1f;
            
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
            bool shotFired = gun.Shoot();
            if (shotFired) {
                Recoil(movementDirection);
            }
        }
    }

    private void Recoil(Vector2 projectileDirection)
    {
        myRigidBody.AddForce(-projectileDirection * recoilStrength);
    }

    private void ResetDamageIndicator() {
        if (Time.time > damageIndicatorTime) {
            mySpriteRenderer.color = Color.white;
        }
    }
}
