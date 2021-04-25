using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsBehaviour : MonoBehaviour
{
    [SerializeField] public float detectionRange;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float idleSpeed;
    [SerializeField] public float idleTurnTime;
    [SerializeField] public float idleDirectionX;
    [SerializeField] public float idleDirectionY;
    [SerializeField] public float injureSpeedReduction;

    private Vector2 GetIdleDirection() {
        return new Vector2(idleDirectionX, idleDirectionY);
    }

    private Player player;
    private Transform myTransform;
    private EnemyHealth health;
    private float idleForwardMultiplier = 1f;
    private float currentIdleTime;
    void Start()
    {
        player = FindObjectOfType<Player>();
        myTransform = GetComponent<Transform>();
        health = GetComponent<EnemyHealth>();
        currentIdleTime = idleTurnTime * Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(myTransform.position, player.transform.position);
        if (distance < detectionRange) {
            MoveTowardsPlayer();
        } else {
            IdleMove();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (myTransform.position - player.transform.position).normalized;
        float usedSpeed = moveSpeed;
        if (health.GetIsInjured()) {
            usedSpeed = moveSpeed * injureSpeedReduction * Time.deltaTime * 60;
        }
        myTransform.position -= direction * usedSpeed;
        transform.localScale = new Vector2(-Mathf.Sign(direction.x), 1);
    }

    private void IdleMove()
    {
        currentIdleTime -= Time.deltaTime;
        if (currentIdleTime <= 0) {
            idleForwardMultiplier *= -1;
            currentIdleTime = idleTurnTime;
        }
        Vector3 direction = GetIdleDirection().normalized * idleForwardMultiplier;
        myTransform.position -= direction * idleSpeed * Time.deltaTime * 60;
        transform.localScale = new Vector2(-Mathf.Sign(direction.x), 1);
    }
}
