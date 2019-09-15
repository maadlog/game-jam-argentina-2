using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOff : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	float alpha = 0;

	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (alpha < 256)
		{
            alpha += Time.deltaTime;
			spriteRenderer.color = new Color(0, 0, 0, alpha);
		}
	}
}
