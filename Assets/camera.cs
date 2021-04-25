using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Player player;
    private float clampXLeft = 8f;
    private float clampXRight = 22f;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private float clampX(float currPos) {
        if (currPos < clampXLeft) {
            return clampXLeft;
        } else if (currPos > clampXRight) {
            return clampXRight;
        } else {
            return currPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
        var clampedX = clampX(newCamPos.x);
        transform.position = new Vector3(clampedX, newCamPos.y, transform.position.z);
    }
}
