using UnityEngine;
using System.Collections;

public class CheckpointSystem : MonoBehaviour
{
	public float timeLeft;
	public GUIText timeText;

	void OnTriggerEnter(Collider other)
	{
		Checkpoint checkpoint = other.gameObject.GetComponent<Checkpoint> ();

		if(checkpoint != null)
		{
			timeLeft += checkpoint.time;

			Destroy(other.gameObject);
		}
	}

	void Update()
	{
		timeLeft -= Time.deltaTime;

		if(timeLeft < 0.0f)
		{
			timeLeft = 0.0f;
		}

		timeText.text = "Time: " + timeLeft;
	}
}
