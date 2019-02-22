using UnityEngine;
using System.Collections;

public class winScript : MonoBehaviour
{

	// Use this for initialization
    void OnTriggerEnter()
    {
		LevelController.NextLevel ();
    }
}
