using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camera;
    public float relativeMove = .3f;

    void Update()
    {
        transform.position = new Vector2(camera.position.x,
            camera.position.y * relativeMove);
    }
}
