using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Gun gun;
    [SerializeField] float speed = 0.03f;
    [SerializeField] float offset = 0.0f;
    [SerializeField] float xOffset = 0.0f;
    [SerializeField] float yOffset = 0.0f;

    private Vector2 target;
    private Vector2 movementDirection;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) {
            transform.position = new Vector2(transform.position.x + movementDirection.x, transform.position.y + movementDirection.y); 
        } else {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (gun != null) { 
                transform.position = gun.transform.position;
                transform.rotation = gun.transform.rotation;
                Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized;
                transform.position = transform.position + new Vector3(direction.x * offset, direction.y * offset, 0.0f);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Trigger() {
        triggered = true;

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movementDirection = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * speed * Time.deltaTime;
        
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void setGun(Gun gun) {
        this.gun = gun;
    }

    public bool isTriggered() {
        return triggered;
    }
}
