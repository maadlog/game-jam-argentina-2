using UnityEngine;
using UnityEngine.UI;

public class HeatIndicatorArrow : MonoBehaviour
{
    Image image;
    void Start() {
        image = GetComponent<Image>();
    }
    internal void SetRate(float proportion)
    {
        var eulerAngle = proportion * -Mathf.PI;

        image.transform.rotation = Quaternion.Euler(0, 0, eulerAngle * Mathf.Rad2Deg);
    }
}