using System;
using System.IO;
using UnityEngine;

public class ExitFade : MonoBehaviour
{
	// New
	private void OnApplicationQuit()
	{
		string[] array = File.ReadAllLines("settings.txt");
        array[8] = string.Join(",", new string[]
        {
            Screen.width.ToString(),
            Screen.height.ToString(),
            Screen.fullScreen.ToString()
        });
        File.WriteAllLines("settings.txt", array);
	}
}
