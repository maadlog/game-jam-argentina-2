using UnityEngine;

public class LevelFader: MonoBehaviour {
    public GameObject callbackReceptor;
    public void FinishedFadeIn() {
        callbackReceptor?.BroadcastMessage("FinishedFadeIn", SendMessageOptions.DontRequireReceiver);
    }
    public void FinishedFadeOut() {
        callbackReceptor?.BroadcastMessage("FinishedFadeOut", SendMessageOptions.DontRequireReceiver);
    }
}