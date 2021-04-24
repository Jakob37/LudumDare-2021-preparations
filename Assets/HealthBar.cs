using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Player player;
    private Transform myTransform;
    private float startScaleX;
    private float startScaleY;

    void Start()
    {
        player = FindObjectOfType<Player>();
        myTransform = GetComponent<Transform>();
        startScaleX = myTransform.localScale.x;
        startScaleY = myTransform.localScale.y;
    }

    void Update()
    {
        float scaleX = 0;
        if (player.getHealthFraction() > 0) {
            scaleX = player.getHealthFraction() * startScaleX;
        }
        myTransform.localScale = new Vector3(scaleX, startScaleY, 1);
    }
}
