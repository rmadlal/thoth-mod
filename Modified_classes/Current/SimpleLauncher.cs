using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLauncher : MonoBehaviour
{
    // Modified
	private void Start()
	{
		Application.runInBackground = true;
		bool flag = false;
		if (!File.Exists("settings.txt"))
		{
			flag = true;
		}
		else if (File.ReadAllLines("settings.txt")[0].ToCharArray()[3] != '2')
		{
			File.Delete("settings.txt");
			flag = true;
		}
		if (flag)
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
				"Window size: width,height,fullscreen",
				"1600,900," + bool.FalseString,
				"//Debug",
				bool.FalseString
			};
			File.WriteAllLines("settings.txt", contents);
		}
		string[] array = File.ReadAllLines("settings.txt");
		bool flag2 = array[4] == "1";
		bool flag3 = array[2] == "1";
		OverlapPlane.useExpensiveLava = !(array[6] == "1");
		if (!OverlapPlane.useExpensiveLava)
		{
			SceneManager.LoadSceneAsync("LoadTilingTextureBank", LoadSceneMode.Additive);
		}
		int qualityLevel;
		if (flag2)
		{
			if (flag3)
			{
				qualityLevel = 3;
			}
			else
			{
				qualityLevel = 2;
			}
		}
		else if (flag3)
		{
			qualityLevel = 1;
		}
		else
		{
			qualityLevel = 0;
		}
		QualitySettings.SetQualityLevel(qualityLevel);
		string[] expr_13B = array[8].Split(new char[]
		{
			','
		});
		int width = int.Parse(expr_13B[0]);
		int height = int.Parse(expr_13B[1]);
		bool fullscreen = bool.Parse(expr_13B[2]);
		Screen.SetResolution(width, height, fullscreen);
		Globals.showRoomDebugInfo = bool.Parse(array[10]);
		Cursor.visible = false;
	}
}
