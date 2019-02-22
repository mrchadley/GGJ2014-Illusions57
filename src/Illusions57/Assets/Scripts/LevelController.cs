using UnityEngine;
using System.Collections;

public static class LevelController
{
	public static int currentLevel = 1;
	public static int maxLevel = 4;

	public static void NextLevel()
	{
		Debug.Log ("NextLevel: " + (currentLevel + 1));

		if(currentLevel > maxLevel)
		{
			MainMenu();
		}
		else
			ChangeLevel (currentLevel + 1);
	}

	public static void ChangeLevel(int levelNumber)
	{
		currentLevel = levelNumber;

		Application.LoadLevel ("Level" + levelNumber);
	}

	public static void MainMenu()
	{
		Application.LoadLevel ("Main Menu");
	}
}
