using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameManager miGameManager;// = GameManager.getGameManager();

    // Start is called before the first frame update
    void Start()
    {
        miGameManager = GameManager.getGameManager();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Base"))
        {
            miGameManager.UpdateCounter(1);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}