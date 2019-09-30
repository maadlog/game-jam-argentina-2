using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public enum Axis // your custom enumeration
	{
		Horizontal,
		Vertical
	};

	public bool isActivated = false;
	public float selectedAnimationSpeed = 0.5f;
	public Axis movementAxis = Axis.Vertical;
	public Button[] options = default;

	int indexOption = 0;
	float animationTimer = 0f;
	float initialPercentage = 1f;
	float scaleFactor = 1.1f;

	float timerAxis = 0;
	float axisColdDownTime = 0.1f;

	// Update is called once per frame
	void Update()
	{
		if (isActivated)
		{
			SelectedAnimation();
			CheckChangeSelectedOption();
			CheckEnterOption();
		}

	}

	void SelectedAnimation()
	{
		if (animationTimer < selectedAnimationSpeed)
		{
			float slope = (scaleFactor - initialPercentage) / selectedAnimationSpeed;
			float increaseFactor = animationTimer * slope + initialPercentage;
			options[indexOption].transform.localScale = new Vector3(increaseFactor, increaseFactor, 1);
			animationTimer += Time.unscaledDeltaTime;
		}
		else if (animationTimer > selectedAnimationSpeed && animationTimer < selectedAnimationSpeed * 2)
		{
			float slope = (initialPercentage - scaleFactor) / (selectedAnimationSpeed * 2 - selectedAnimationSpeed);
			float increaseFactor = animationTimer * slope + (initialPercentage - slope * selectedAnimationSpeed * 2);
			options[indexOption].transform.localScale = new Vector3(increaseFactor, increaseFactor, 1);
			animationTimer += Time.unscaledDeltaTime;
		}
		else
		{
			animationTimer = 0f;
		}
	}

	void CheckChangeSelectedOption()
	{
		if (timerAxis < 0f)
		{
			if (Input.GetAxisRaw(movementAxis.ToString()) == 1)
			{
				options[indexOption].transform.localScale = new Vector3(1, 1, 1);
				indexOption++;
			}
			else if (Input.GetAxisRaw(movementAxis.ToString()) == -1)
			{
				options[indexOption].transform.localScale = new Vector3(1, 1, 1);
				indexOption--;
			}

			if (indexOption >= options.Length)
			{
				indexOption = 0;
			}
			if (indexOption < 0)
			{
				indexOption = options.Length - 1;
			}
			timerAxis = axisColdDownTime;
		}
		else
		{
			timerAxis -= Time.unscaledDeltaTime;
		}
	}

	void CheckEnterOption()
	{
		if (Input.GetButtonDown("Submit"))
		{
			options[indexOption].onClick.Invoke();
		}
	}
}
