using UnityEngine;
using System.Collections;

public class InventorySystem : MonoBehaviour
{
	public GUITexture keyTexture;

	private ArrayList items = new ArrayList();


	void Start()
	{
		keyTexture.enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		Item otherItem = other.gameObject.GetComponent<Item> ();

		if(otherItem != null)
		{
			AddItem(otherItem);

			Destroy (other.gameObject);
		}
	}

	public void AddItem(Item item)
	{
		items.Add (item);

		if(item.name == "key_gold")
		{
			keyTexture.enabled = true;
		}
	}

	public void RemoveItem(string itemName)
	{
		foreach(Item item in items)
		{
			if(item.name == itemName)
			{
				items.Remove(item);

				if(itemName == "key_gold")
				{
					keyTexture.enabled = true;
				}
			}
		}


	}

	public bool CheckIfItem(string itemName)
	{
		foreach(Item item in items)
		{
			if(item.name == itemName)
			{
				return true;
			}
		}
		return false;
	}
}
