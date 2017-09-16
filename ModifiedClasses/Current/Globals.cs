using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    // Modified
	private void Awake()
	{
		Globals.instance = this;
		if (Globals.currentLevelID == -1)
		{
			Globals.currentLevelID = 0;
		}
		Globals.timerGUIStyle = new GUIStyle();
		Globals.timerGUIStyle.fontStyle = FontStyle.Bold;
		Globals.timerGUIStyle.fontSize = 24;
		Globals.timerGUIStyle.normal.textColor = Color.white;
	}

	// Modified
	private void Update()
	{
		if ((Globals.currentLevelID == 16 || Globals.currentLevelID == 18 || Globals.currentLevelID == 19 || (Globals.currentLevelID == 17 && ProceduralRoom.roomCounter == ProceduralRoom.roomCounterTarget)) && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("NewMenu");
            GeneralSoundBank.GetInstance().StopEndTheme();
            Globals.currentLevelID = -1;
        }
	}

    // New
	private void OnGUI()
	{
		if (NewMenu.cheatsEnabled)
        {
            Globals.timerGUIStyle.fontSize = 32;
            Globals.timerGUIStyle.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect((float)(Screen.width / 2) - 100f, 10f, 200f, 200f), "CHEATS ENABLED", Globals.timerGUIStyle);
        }
        Globals.timerGUIStyle.fontSize = 24;
        if (Globals.showRoomDebugInfo)
        {
            GUI.Label(new Rect(10f, 60f, 200f, 200f), string.Format("currentRoom: {0}, currentGlobalRoomID: {1}, currentLevelID: {2}, currentSceneName: {3}, room state: {4}, timer diff: {5}", new object[]
            {
                (Globals.currentRoom != null) ? string.Concat(Globals.currentRoom) : "",
                string.Concat(Globals.currentGlobalRoomID),
                string.Concat(Globals.currentLevelID),
                (Globals.currentLevelName != null) ? SceneManager.GetActiveScene().name : "",
                (Globals.currentRoom != null) ? string.Concat(Globals.currentRoom.GetRoomState()) : "",
                (Globals.realTimeTimer != null) ? Globals.realTimeTimer.Elapsed.Subtract(Globals.loadlessTimer.Elapsed).ToString() : ""
            }));
        }
        if (Globals.loadlessTimer != null)
        {
            Globals.timerGUIStyle.alignment = TextAnchor.UpperLeft;
            GUI.Label(new Rect(10f, 10f, 150f, 100f), Globals.FormatTime(Globals.loadlessTimer.Elapsed), Globals.timerGUIStyle);
            GUI.Label(new Rect(10f, 35f, 150f, 100f), Globals.FormatTime(Globals.realTimeTimer.Elapsed), Globals.timerGUIStyle);
            if (Globals.showRealTimeAndILTime)
            {
                Globals.timerGUIStyle.alignment = TextAnchor.UpperRight;
                GUI.Label(new Rect((float)Screen.width - 160f, 10f, 150f, 100f), Globals.FormatTime(Globals.inGameTime.Elapsed), Globals.timerGUIStyle);
                GUI.Label(new Rect((float)Screen.width - 160f, 35f, 150f, 100f), Globals.FormatTime(Globals.prevRoomTime), Globals.timerGUIStyle);
            }
        }
	}

    // New
    public static string FormatTime(TimeSpan ts)
    {
        int millis = (ts.Milliseconds >= 100) ? (ts.Milliseconds / 10) : ts.Milliseconds;
        if (ts.Hours > 0)
        {
            return string.Format("{0}:{1:00}:{2:00}.{3:00}",
                ts.Hours,
                ts.Minutes,
                ts.Seconds,
                millis);
        }
        if (ts.Minutes > 0)
        {
            return string.Format("{0}:{1:00}.{2:00}", ts.Minutes, ts.Seconds, millis);
        }
        return string.Format("{0}.{1:00}", ts.Seconds, millis);
    }

    // New
	public static Stopwatch inGameTime;

    // New
	public static TimeSpan prevRoomTime;

    // New
	public static GUIStyle timerGUIStyle;

    // New
	public static bool showRealTimeAndILTime;

    // New
	public static Stopwatch realTimeTimer;

    // New
	public static bool showRoomDebugInfo;

    // New
	public static Stopwatch loadlessTimer;

    // New
	public static bool cheatRepeatLevel;

    // New
	public static bool checkpointCheat;

    // New
	public static bool lavaAlwaysOn;
}
