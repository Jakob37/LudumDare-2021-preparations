using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameOver() {
        StartCoroutine(LoadScene("GameOver"));
    }

    public void LoadGame() {
        StartCoroutine(LoadScene("main"));
    }

    IEnumerator LoadScene(string scene) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
