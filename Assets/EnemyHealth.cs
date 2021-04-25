using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] bool destroyProjectile;
    private SpriteRenderer mySpriteRenderer;

    private int currentHealth;
    private float damageIndicatorTime;
    private bool isInjured = false;
    public bool GetIsInjured() {
        return isInjured;
    }
    void Start()
    {
        currentHealth = maxHealth;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        damageIndicatorTime = Time.time;
    }

    void Update() {
        ResetDamageIndicator();
    }

    public void Damage(int damage=1) {
        mySpriteRenderer.color = Color.red;
        damageIndicatorTime = Time.time + 0.1f;

        currentHealth -= damage;
        isInjured = true;
        if (currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            Damage();
            if (destroyProjectile && collision.gameObject.GetComponent<Projectile>().isTriggered()) {
                Destroy(collision.gameObject);
            }
        }
    }
    private void ResetDamageIndicator() {
        if (Time.time > damageIndicatorTime) {
            mySpriteRenderer.color = Color.white;
        }
    }
    
}
