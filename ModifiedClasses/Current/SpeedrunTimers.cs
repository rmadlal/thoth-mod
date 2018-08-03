using System;
using System.Diagnostics;
using UnityEngine;

public class SpeedrunTimers {

    public SpeedrunTimers()
    {
        this.realTimeTimer = new Stopwatch();
        this.loadlessTimer = new Stopwatch();
        this.inGameTime = new Stopwatch();
        this.prevRoomTime = TimeSpan.Zero;
        this.timerGUIStyle = new GUIStyle();
        this.timerGUIStyle.fontStyle = FontStyle.Bold;
        this.timerGUIStyle.fontSize = 24;
        this.timerGUIStyle.normal.textColor = Color.white;
    }

    public static SpeedrunTimers Instantiate()
    {
        return Globals.speedrunTimers = new SpeedrunTimers();
    }

    public static void Disable()
    {
        Globals.speedrunTimers = null;
    }

    public void OnGUI()
    {
        this.timerGUIStyle.alignment = TextAnchor.UpperLeft;
        GUI.Label(new Rect(10f, 10f, 150f, 100f), SpeedrunTimers.FormatTime(this.loadlessTimer.Elapsed), this.timerGUIStyle);
        GUI.Label(new Rect(10f, 35f, 150f, 100f), SpeedrunTimers.FormatTime(this.realTimeTimer.Elapsed), this.timerGUIStyle);
        if (SpeedrunTimers.showRealTimeAndILTime)
        {
            this.timerGUIStyle.alignment = TextAnchor.UpperRight;
            GUI.Label(new Rect((float)Screen.width - 160f, 10f, 150f, 100f), SpeedrunTimers.FormatTime(this.inGameTime.Elapsed), this.timerGUIStyle);
            GUI.Label(new Rect((float)Screen.width - 160f, 35f, 150f, 100f), SpeedrunTimers.FormatTime(this.prevRoomTime), this.timerGUIStyle);
        }
    }

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

    public GUIStyle timerGUIStyle;

    public Stopwatch realTimeTimer;

    public Stopwatch loadlessTimer;

    public Stopwatch inGameTime;

    public TimeSpan prevRoomTime;

    public static bool showRealTimeAndILTime;

    public static SpeedrunTimers instance;
}
