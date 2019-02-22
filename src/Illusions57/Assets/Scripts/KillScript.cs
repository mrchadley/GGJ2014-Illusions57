using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour
{

    public float yKill = -200.0f;
	
	// Update is called once per frame
	void Update ()
	{
        if (transform.position.y < yKill)
        {
			LevelController.NextLevel();
            
        }
	}
}
