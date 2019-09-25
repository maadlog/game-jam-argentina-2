using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotationer
{
	Quaternion GetRotationUpdate(float deltaTime, Transform baseTransform);
}

public class DampRotationer : IRotationer
{
	public GameObject objective;
	public SpriteRenderer parentSprite;

	public DampRotationer(GameObject objective, SpriteRenderer parentSprite)
	{
		this.objective = objective;
		this.parentSprite = parentSprite;
	}

	// float quickRotationSpeed = 80;
	float mediumRotationSpeed = 30;
	float slowRotationSpeed = 15;
	float speed;

	public Quaternion GetRotationUpdate(float deltaTime, Transform baseTransform)
	{
		Vector3 dir = objective.transform.position - baseTransform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		float firstRotzAngle;
		if (zAngle > 0)
		{
			firstRotzAngle = zAngle;
		}
		else
		{
			firstRotzAngle = 360 + zAngle;
		}
		var remainingRot = Math.Abs(baseTransform.rotation.eulerAngles.z) - Math.Abs(firstRotzAngle);

		if (remainingRot > 45)
		{
			speed = mediumRotationSpeed;
		}
		else if (remainingRot > 15)
		{
			speed = slowRotationSpeed;
		}
		else
		{
			speed = mediumRotationSpeed;
		}

		Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);
		return Quaternion.RotateTowards(baseTransform.rotation, desiredRotation, (speed * deltaTime));

	}
}

public class InstantaneousRotationer : IRotationer
{
	public GameObject objective;

	public InstantaneousRotationer(GameObject objective)
	{
		this.objective = objective;

	}

	public Quaternion GetRotationUpdate(float deltaTime, Transform baseTransform)
	{

		Vector3 dir = objective.transform.position - baseTransform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);

		return Quaternion.RotateTowards(baseTransform.rotation, desiredRotation, 360);

	}
}


public class FaceTaggedObject : MonoBehaviour
{

	private GameObject objective;

	public IRotationer rotationer { get; private set; }

	public string tagToFace;
	public bool instantaneous;

	// Start is called before the first frame update
	void Awake()
	{
		FindObjective();

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (objective == null)
		{
			FindObjective();
		}

		if (objective == null)
			return;

		transform.rotation = rotationer.GetRotationUpdate(Time.deltaTime, transform);

	}

	private void FindObjective()
	{
		if (String.IsNullOrEmpty(tagToFace))
		{
			objective = null;
		}
		else
		{
			//Find the desired target
			GameObject go = GameObject.FindWithTag(tagToFace);

			if (go != null)
			{
				objective = go;
				if (instantaneous)
				{
					rotationer = new InstantaneousRotationer(go);
				}
				else
				{
					rotationer = new DampRotationer(go, this.GetComponent<SpriteRenderer>());
				}

			}
		}

	}
}