using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    // [SerializeField] public float detectionRange;
    // [SerializeField] public float moveSpeed;

    // private Player player;
    // private Transform myTransform;
    // private EnemyHealth health;

    void Start()
    {
        // player = FindObjectOfType<Player>();
        // myTransform = GetComponent<Transform>();
        // health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // float distance = Vector2.Distance(myTransform.position, player.transform.position);
        // if (distance < detectionRange) {
        //     MoveTowardsPlayer();
        // }
    }

    // private void MoveTowardsPlayer() {
    //     Vector3 direction = (myTransform.position - player.transform.position).normalized;
    //     float usedSpeed = moveSpeed;
    //     if (health.GetIsInjured()) {
    //         usedSpeed = moveSpeed * 0.5f;
    //     }
    //     myTransform.position -= direction * usedSpeed;
    //     transform.localScale = new Vector2(-Mathf.Sign(direction.x), 1);
    // }
}
