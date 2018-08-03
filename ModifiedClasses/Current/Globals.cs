using System;
using System.Collections.Generic;
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
        this.guiStyle = new GUIStyle();
        this.guiStyle.fontStyle = FontStyle.Bold;
        this.guiStyle.fontSize = 32;
        this.guiStyle.normal.textColor = Color.white;
        this.guiStyle.alignment = TextAnchor.UpperCenter;
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
            GUI.Label(new Rect((float)(Screen.width / 2) - 100f, 10f, 200f, 200f), "CHEATS ENABLED", this.guiStyle);
        }
        if (Globals.speedrunTimers != null)
        {
            Globals.speedrunTimers.OnGUI();
        }
        if (Globals.showRoomDebugInfo)
        {
            GUI.Label(new Rect(10f, 60f, 200f, 200f), string.Format("currentRoom: {0}, currentGlobalRoomID: {1}, currentLevelID: {2}, currentSceneName: {3}, room state: {4}, timer diff: {5}", new object[]
            {
                (Globals.currentRoom != null) ? string.Concat(Globals.currentRoom) : "",
                string.Concat(Globals.currentGlobalRoomID),
                string.Concat(Globals.currentLevelID),
                (Globals.currentLevelName != null) ? SceneManager.GetActiveScene().name : "",
                (Globals.currentRoom != null) ? string.Concat(Globals.currentRoom.myState) : "",
                (Globals.speedrunTimers.realTimeTimer != null) ? Globals.speedrunTimers.realTimeTimer.Elapsed.Subtract(Globals.speedrunTimers.loadlessTimer.Elapsed).ToString() : ""
            }));
        }
    }

    // New
    public GUIStyle guiStyle;

    // New
    public static SpeedrunTimers speedrunTimers;

    // New
    public static bool showRoomDebugInfo;
}
