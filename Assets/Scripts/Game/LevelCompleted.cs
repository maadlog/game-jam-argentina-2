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
    public Text cantidaddekills2;
    public Text totalScore;
    public Text totalScore2;

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
    }
  public  void ShowKills(int kills,int kills2)
    {
        cantidaddekills.text = kills.ToString();
        cantidaddekills2.text = kills2.ToString();

        if (kills2 != 0)
        {
            cantidaddekills2.text = kills2.ToString();
        }
        else { cantidaddekills2.text = " "; }
    }

   public void ShowScore(int score, int score2)
    {
        totalScore.text = score.ToString();
        if (score2 != 0)
        {
            totalScore2.text = score2.ToString();
        }
        else { totalScore2.text = " "; }
       
    }
}
