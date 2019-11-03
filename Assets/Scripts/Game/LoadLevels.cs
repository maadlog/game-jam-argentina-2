using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    string managingScene;
   public void LoadLevel(string scene)
    {
        managingScene = scene;
        LevelFader.GetInstance(this.gameObject).FadeIn();
    }

    public void FinishedFadeIn() {
        SceneManager.LoadScene(managingScene);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
