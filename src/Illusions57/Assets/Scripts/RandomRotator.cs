using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
	public float speed;

	void FixedUpdate()
	{
		gameObject.transform.Rotate (new Vector3(0, 0, 1.0f) * speed);
	}
}
