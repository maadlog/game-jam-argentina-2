using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeBar : MonoBehaviour
{

    public static BossLifeBar ins;
    public TakeDamage healthboss;
    public Image bossLifeBar;
    public float life;
    public float totallife;
    public float total;

    private void Awake()
    {
        ins = this;
       

        if (Manager.GetInstance().Players == 1)
        {
            totallife = 285;
            healthboss.startHealth = 285;
        }
        else
        {
            totallife = 530;
            healthboss.startHealth = 530;
        }
    
       // total = totallife + Mathf.Round((totallife / 2)) + Mathf.Round((totallife / 2)) + Mathf.Round((totallife / 2)) + Mathf.Round((totallife / 2))/*+ (totallife / 6)*/;
        total = totallife + (totallife / 2) + (totallife / 2) +(totallife / 2) + (totallife / 2)/*+ (totallife / 6)*/;
        life = total + 25;
    }
    
   public void RecibirDaño(float daño)
    {
        life -= daño;
        ActualizarBarra();
    }

    void Update()
    {

    }
    void ActualizarBarra()
    {
        if (GameManager.getGameManager().isInGame == true)
        {
            bossLifeBar.fillAmount = porc();
        }
        else
        {
            bossLifeBar.fillAmount = 0;
        }
    }

    float porc()
    {
        var res = (life * 100) / total;
        
        return res / 100;
    }
}
