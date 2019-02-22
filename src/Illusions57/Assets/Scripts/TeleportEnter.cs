using UnityEngine;
using System.Collections;

public class TeleportEnter : MonoBehaviour
{

    public GameObject enter;
    public GameObject exit;
    public bool enterBool = false; // Flag to set if we can enter through that 'Portal' (sometimes we don't want to go back through an exit)
	public float timeUntilCanEnterAgain;

	public float angleMin;
	public float angleMax;

	private float x;
	private float y;
	private float z;

	public bool canEnter = true; // DO NOT SET IN EDITOR!
	private bool timerRunning = false;

	private GameObject player;

	void Start()
	{
		player = GameObject.FindWithTag ("Player") as GameObject;
	}

	void Update()
	{
		//Debug.Log (player.transform.position);
	}

	void OnTriggerEnter (Collider other) 
    {
		if (!enterBool)
			return; // We don't need to do any processsing if we can't enter.

		// Only allow the player to teleport
		if(other.gameObject.tag == "Player")
		{
			if(angleMin > 0.0f && angleMax > 0.0f)
			{
				Debug.Log("Rot: " + other.gameObject.transform.eulerAngles.y);
				if(other.gameObject.transform.eulerAngles.y < angleMin || other.gameObject.transform.eulerAngles.y > angleMax)
				{
					Debug.Log("Angle rotation is not true");
					return;
				}
			}

			if (canEnter == true)
			{
				Debug.Log("Entered trigger!");

				canEnter = false; 
				//exit.GetComponent<teleportEnter>().enterBool = true;

				if(gameObject.tag == "Entrance")
				{
					Debug.Log("Entered trigger entrance!");

					exit.GetComponent<TeleportEnter>().canEnter = false;

				    //y = exit.transform.position.y - (enter.transform.position.y - player.transform.position.y); This is not guaranteed to work.
					y = exit.transform.position.y; // We can use this. Portal has to be placed around 1.5f from the ground.
				}
				else if(gameObject.tag == "Exit")
				{
					Debug.Log("Entered trigger exit!");
					enter.GetComponent<TeleportEnter>().canEnter = false;

					y = enter.transform.position.y;
				}

				x = player.transform.position.x;
				z = player.transform.position.z;

				player.transform.position = new Vector3(x, y, z);

			}
		}
	}

    void OnTriggerExit(Collider other)
    {
        if (canEnter == false && timerRunning == false)
			StartCoroutine (CanEnterAgain(timeUntilCanEnterAgain));
        
    }

	private IEnumerator CanEnterAgain(float time)
	{
		timerRunning = true;
		yield return new WaitForSeconds (time);
		canEnter = true;
		timerRunning = false;
	}
}
