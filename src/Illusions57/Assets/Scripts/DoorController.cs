using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
	public string keyNameRequired;
	public Vector3 whereToMove;
	public float openTime;

	private bool alreadyOpened = false;

	private Vector3 startPos;
	private GameObject player;
	private InventorySystem inventory;

	private bool doorOpen = false;
	private bool moving = false;
	private bool readyToBeClosed = false;

	void Start()
	{
		startPos = gameObject.transform.position;

		player = GameObject.FindWithTag ("Player");

		if(player != null)
		{
			inventory = player.GetComponent<InventorySystem>();

			if(inventory == null)
			{
				Debug.Log("Warning: Could not get instance of inventory system");
			}
		}
		else
		{
			Debug.Log("Warning: Could not get instance of player.");
		}

		if (keyNameRequired == "")
			alreadyOpened = true;

	}

	void Update()
	{
		if(readyToBeClosed && !moving && doorOpen)
		{
			StartCoroutine(MoveDoor (whereToMove, startPos, openTime));
			Debug.Log("Door ready to be closed.");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if the player is the one that triggered collision.
		if(other.tag == "Player")
		{
			// Check if player has the key required to open door.
			if(inventory.CheckIfItem (keyNameRequired) || alreadyOpened == true)
			{
				alreadyOpened = true;

				if(!moving)
					StartCoroutine(MoveDoor (startPos, whereToMove, openTime));
			}
		}


	}

	void OnTriggerExit(Collider other)
	{

		if(other.tag == "Player")
		{
			if(!moving && doorOpen)
			{
				doorOpen = false;
				StartCoroutine(MoveDoor (whereToMove, startPos, openTime));
			}

			if(moving && !doorOpen)
				readyToBeClosed = true; // We do not need to set this unless the door is moving.
		}
	}

	IEnumerator MoveDoor(Vector3 from, Vector3 to, float time)
	{
		if(!moving)
		{
			moving = true;
			float t = 0.0f;
			
			while(t < 1.0)
			{
				t += Time.deltaTime / time;

				transform.position = Vector3.Lerp (from, to, t);
				yield return 0;
			}
			// If the door was open
			if(doorOpen)
			{
				Debug.Log("Set door to close.");
				doorOpen = false;
				readyToBeClosed = false;
			}
			else
				doorOpen = true;

			moving = false;
		}
	}
}
