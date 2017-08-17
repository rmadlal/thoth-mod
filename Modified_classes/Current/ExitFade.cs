using System;
using System.IO;
using UnityEngine;

public class ExitFade : MonoBehaviour
{
	// Modified
	private void OnApplicationQuit()
	{
		string[] contents = new string[]
		{
			"Ver2   - do not modify this line",
			"//VSync, 0 means off, 1 means on. Turning it off can minimize input latency but might introduce some screen tearing",
			"1",
			"//Anti-aliasing. 0 means off, 1 means on. AA is expensive so it is off by default",
			"0",
			"//Cheap lava optimization, 0 means off, 1 means on. If you experience low fps, turn it on",
			"0",
			"//Window size: width,height,fullscreen",
			string.Join(",", new string[]
			{
				Screen.width.ToString(),
				Screen.height.ToString(),
				Screen.fullScreen.ToString()
			}),
			"//Debug",
			Globals.showRoomDebugInfo.ToString()
		};
		File.WriteAllLines("settings.txt", contents);
	}
}
