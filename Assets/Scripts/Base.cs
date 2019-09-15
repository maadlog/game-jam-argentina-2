using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Base : MonoBehaviour
{
    // Start is called ;before the first frame update
    public float health;
    public Image uihealth;
    void Start()
    {
        uihealth.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+ 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        uihealth.fillAmount= health/ 100;
    }
    public void GetHit(int damage)
    {
        health-= damage;
    }

}
