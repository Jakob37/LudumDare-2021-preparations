using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureBar : MonoBehaviour
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
        myTransform.localScale = new Vector3(player.getPressureFraction() * startScaleX, startScaleY, 1);
    }
}
