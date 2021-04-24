using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public Projectile projectile;
    public float verticalOffset;
    public float fireRate;
    private Projectile harpoon;
    // Start is called before the first frame update
    private float nextReload;

    void Start()
    {
        nextReload = Time.time;
        //LoadProjectile(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0.0f, verticalOffset, 0.0f);
        
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 movementDirection = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized;
        
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        Vector3 gunScale = transform.localScale;
        if (Mathf.Abs(angle) > 90) {
            gunScale.y = -1.0f;
        } else {
            gunScale.y = 1.0f;
        }
        transform.localScale = gunScale;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Time.time > nextReload && harpoon is null) {
            LoadProjectile();
        }

    }

    public void Shoot() {

            Debug.Log("Harpoon is fired");
            harpoon.Trigger();
            // LoadProjectile();
            harpoon = null;
            nextReload = Time.time + fireRate;
    }

    public void LoadProjectile() {
        harpoon = Instantiate<Projectile>(projectile, transform.position, Quaternion.identity);
        harpoon.setGun(this);
        if (harpoon is null) {
            Debug.Log("Harpoon is null");
        } else {

            Debug.Log("Harpoon is: " + harpoon.ToString());
        }
    }
}
