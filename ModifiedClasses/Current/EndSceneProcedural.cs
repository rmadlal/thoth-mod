using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneProcedural : MonoBehaviour
{
    // Modified
    private void Start()
    {
        this.fromLevelIndex = 16;
        this.toLevelIndex = 17;
        Debug.Log("End Scene Loaded and Started");
        this.myState = EndSceneProcedural.MyState.slide;
        Globals.speedrunTimers.loadlessTimer.Start();
        // from here unchanged
    }
}
