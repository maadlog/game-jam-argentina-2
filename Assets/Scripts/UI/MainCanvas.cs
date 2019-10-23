using UnityEngine;

public class MainCanvas: MonoBehaviour
{
    private static MainCanvas mainCanvas = null;
	void Awake()
	{
		if (mainCanvas == null)
		{
			mainCanvas = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (mainCanvas != this)
			{
				Destroy(gameObject);
			}
		}
	}
	public static MainCanvas GetInstance()
	{
		return mainCanvas;
	}
    public GameObject HeatIndicatorPrefab;

    public HeatIndicator CreateIndicator(HeatIndicator.Position position) 
    {
        var newObject = Canvas.Instantiate(HeatIndicatorPrefab,Vector3.zero, Quaternion.identity, transform);
        RectTransform rectTransform = newObject.GetComponent<RectTransform>();

        if (position == HeatIndicator.Position.Left) 
        {
            rectTransform.anchorMax = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.pivot = Vector2.zero;
        }
        else
        {
            rectTransform.anchorMax = new Vector2(1,0);
            rectTransform.anchorMin = new Vector2(1,0);
            rectTransform.pivot = new Vector2(1,0);
        }
        rectTransform.anchoredPosition = Vector2.zero;

        return newObject.GetComponent<HeatIndicator>();
    }
}
