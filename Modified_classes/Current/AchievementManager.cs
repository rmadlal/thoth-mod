using System;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	// Modified
	private void UnlockAchievement(string nameID)
	{
		if (!NewMenu.cheatsEnabled && SteamManager.s_instance != null && SteamManager.Initialized)
		{
			SteamManager.UnlockAchievement(nameID);
		}
		Debug.Log("Unlock A: " + nameID);
	}
}
