using System;
using System.Text;
using Steamworks;
using UnityEngine;

[DisallowMultipleComponent]
internal class SteamManager : MonoBehaviour
{
	// Modified
	public static void UnlockAchievement(string idName)
	{
		Debug.Log("Unlock Achievement: " + idName);
		bool flag = false;
		SteamUserStats.GetAchievement(idName, out flag);
		if (!NewMenu.cheatsEnabled && !flag)
		{
			SteamUserStats.SetAchievement(idName);
			SteamManager.storeStats = true;
		}
	}
}
