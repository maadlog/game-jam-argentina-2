using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public GameObject transicionPanel;
    public Text refugiadostotales;
    public Text cantidaddekills;
    public Text totalScore;


    void Start()
    {
        transicionPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ShowRefugees(int refugees)
    {
        refugiadostotales.text = refugees.ToString() ;

    }
public void WinLevel()
    {
        transicionPanel.SetActive(true);
        //llama a pausa

    }
  public  void ShowKills(int kills)
    {
        cantidaddekills.text = kills.ToString();
    }

   public void ShowScore(int score)
    {
        totalScore.text = score.ToString();
    }
}
