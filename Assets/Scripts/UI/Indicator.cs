using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public enum State
    {
        Good, Warning, Bad
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private State currentState;
    public void Trigger(State state)
    {
        if (state == currentState)
        {
            return;
        }

        currentState = state;

        switch (state)
        {
            case State.Good:
                this.GetComponent<Animator>().Play("Good");
                    break;
            case State.Warning:
                this.GetComponent<Animator>().Play("Warning");
                    break;
            case State.Bad:
                this.GetComponent<Animator>().Play("Bad");
                break;
        }
    }

    //  Bad  Warning  Good
    //R 225    225    25
    //G  25    225    225
    //B  25     25    25

    //225 * 2 = 450

    const float topRef = 0.88f;
    const float bottomRef = 0.01f;

    bool capped = false;
    Color cappedColor = new Color(topRef, bottomRef, bottomRef);
    internal void SetRate(float proportion)
    {
        if (proportion >= 1)
        {
            capped = true;
        }
        if (proportion <= 0)
        {
            capped = false;
        }

        this.GetComponent<Image>().fillAmount = proportion;
        if (capped)
        {
            this.GetComponent<Image>().color = cappedColor;
        }
        else
        {
            this.GetComponent<Image>().color = new Color(
                Mathf.Lerp(bottomRef, topRef*2, proportion)
             , Mathf.Lerp(topRef*2, bottomRef, proportion)
             , bottomRef
                );
        }
        



    }

}
