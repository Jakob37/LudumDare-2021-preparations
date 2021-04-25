using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] bool destroyProjectile;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage=1) {
        currentHealth -= damage;
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
}
