using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float startMenuTime;

    private void Start()
    {
        if (startMenuTime <= 0) {
            Debug.Log("level auto loaded disabeled");
        }
        else { 
            Invoke("LoadNextLevel", startMenuTime);
        }
    }

    public void LoadLevel(string name) {
        Debug.Log("Level Load Requested for: " + name);
        SceneManager.LoadScene(name);
    }
    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void QuitRequest() {
        Debug.Log("Quit Request");
        Application.Quit();
    }
}
