using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    [SerializeField] public float detectionRange;
    [SerializeField] public float moveSpeed;

    private Player player;
    private Transform myTransform;

    void Start()
    {
        player = FindObjectOfType<Player>();
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(myTransform.position, player.transform.position);
        if (distance < detectionRange) {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer() {
        Vector3 direction = (myTransform.position - player.transform.position).normalized;
        myTransform.position -= direction * moveSpeed;
    }
}
