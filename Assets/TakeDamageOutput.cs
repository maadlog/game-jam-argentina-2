using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamageOutput : MonoBehaviour
{
    TakeDamage damageProvider;
    // Start is called before the first frame update
    void Start()
    {
        damageProvider = GetComponentInParent<TakeDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().fillAmount = damageProvider.health / damageProvider.startHealth;
    }
}
