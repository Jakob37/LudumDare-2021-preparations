using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void LoadGameOver() {
        StartCoroutine(LoadScene("GameOver"));
    }

    public void LoadGame() {
        StartCoroutine(LoadScene("main"));
    }

    public void LoadYouWon() {
        StartCoroutine(LoadScene("YouWon"));
    }

    public void LoadYouLost() {
        StartCoroutine(LoadScene("YouLost"));
    }

    IEnumerator LoadScene(string scene) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
