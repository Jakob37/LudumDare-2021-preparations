using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            levelLoader.LoadYouWon();
        }
    }
}
