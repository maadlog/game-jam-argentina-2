using UnityEngine;

public class LevelFader: MonoBehaviour {
    
    public static LevelFader GetInstance(GameObject callbackReceptor) {
        LevelFader fader = GameObject.FindGameObjectWithTag("LevelFader").GetComponent<LevelFader>();
        fader.callbackReceptor = callbackReceptor;
        return fader;
    }
    Animator animator;

    private void Start() {
        animator = this.GetComponent<Animator>();
    }

    public GameObject callbackReceptor;
    public void FinishedFadeIn() {
        callbackReceptor?.BroadcastMessage("FinishedFadeIn");
    }
    public void FinishedFadeOut() {
        callbackReceptor?.BroadcastMessage("FinishedFadeOut");
    }

    public void FadeIn() {
        this.GetComponent<Animator>().Play("FadeIn");
    }

    public void FadeOut() {
        this.GetComponent<Animator>().Play("FadeOut");
    }

    public void Invisible() {
        this.GetComponent<Animator>().Play("Invisible");
    }
}