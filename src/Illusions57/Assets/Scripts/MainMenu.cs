using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public Vector2 offset;
	public Vector2 size;

	public Vector2 creditsPos;
	public Vector2 creditSize;

	public int maxLevel;

	private enum menuItems
	{
		ROOT_MENU,
		CREDITS_MENU
	};

	private int menuIndex = (int)menuItems.ROOT_MENU;

	void Start()
	{
		LevelController.maxLevel = maxLevel;
	}

	void OnGUI ()
	{
		if(menuIndex == (int)menuItems.ROOT_MENU)
		{
			if (GUI.Button (new Rect (offset.x, offset.y + size.y, size.x, size.y), "PLAY"))
				LevelController.ChangeLevel(1);

			if (GUI.Button (new Rect (offset.x, (offset.y + size.y) * 2, size.x, size.y), "CREDITS"))
				menuIndex = (int)menuItems.CREDITS_MENU;

			if (GUI.Button (new Rect (offset.x, (offset.y + size.y) * 3, size.x, size.y), "QUIT GAME"))
				Application.Quit ();
		}
		else if(menuIndex == (int)menuItems.CREDITS_MENU)
		{
			if(GUI.Button(new Rect(10, 10, 100, 50), "BACK"))
				menuIndex = (int)menuItems.ROOT_MENU;

			GUI.enabled = false;
			GUI.TextArea(new Rect(creditsPos.x, creditsPos.y, creditSize.x, creditSize.y), "A game by:\n\nJustin Rhude\n\nMarc Tremblay\n\nSantiago Santoro", 200);
			GUI.enabled = true;
		}
	}
}
