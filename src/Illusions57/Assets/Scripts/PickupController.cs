using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour
{
	public Vector3 pickupOffset;
	public float inputTimerTime = 0.5f;
	public float maxDistance;

	public float throwSpeed = 10000.0f;

	private bool allowedToPickup;
	private bool pickedUpObject;
	private GameObject pickupObject;

	private bool inputAllowed = true;

	private Quaternion lastRotation;

	void FixedUpdate()
	{
		if(pickupObject != null)
		{
			if(pickedUpObject == true)
			{
				pickupObject.rigidbody.isKinematic = true;
				pickupObject.rigidbody.useGravity = false;

				pickupObject.rigidbody.MovePosition (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height/2, pickupOffset.z)));
				pickupObject.rigidbody.MoveRotation(transform.rotation);

				//lastRotation = transform.rotation;

				//pickupObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height/2, pickupOffset.z));
				//pickupObject.transform.rotation = transform.rotation;
			}
			
		}
	}

	void Update()
	{

		if(Input.GetKey(KeyCode.E) && inputAllowed)
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f)), out hit))
			{
				if(hit.transform.tag == "PickupObject" && !pickedUpObject)
				{
					if(hit.distance <= maxDistance)
					{
						pickupObject = hit.transform.gameObject;
						allowedToPickup = true;
					}
					else
					{
						allowedToPickup = false;
					}
				}
			}
			else
			{
				allowedToPickup = false;
			}

			if(allowedToPickup && !pickedUpObject)
			{
				// Pickup Object
				Debug.Log("Picked up Object!");
				pickedUpObject = true;
			} 
			else if(pickedUpObject)
			{
				// Drop object
				Debug.Log("Dropped Object!");
				pickedUpObject = false;
                
				if(pickupObject != null)
				{
					pickupObject.rigidbody.isKinematic = false;
					pickupObject.rigidbody.useGravity = true;

					/*Debug.Log ("" + (transform.rotation.x - lastRotation.x) + ", " + (transform.rotation.y - lastRotation.y) + ", " + (transform.rotation.z - lastRotation.z));

					if(transform.rotation != lastRotation)
					{
						float rotationY = lastRotation.y - transform.rotation.y;

						if(rotationY > 0.0f)
							pickupObject.rigidbody.AddForce(Vector3.left * (-rotationY * throwSpeed));
						else if(rotationY < 0.0f)
							pickupObject.rigidbody.AddForce(Vector3.right * (rotationY * throwSpeed));
					}*/

	                pickupObject = null;
				}

			}

			inputAllowed = false;
			StartCoroutine(TimeInput(inputTimerTime));
		}
	

	}

	IEnumerator TimeInput(float time)
	{
		yield return new WaitForSeconds (time);
		inputAllowed = true;
	}
}
