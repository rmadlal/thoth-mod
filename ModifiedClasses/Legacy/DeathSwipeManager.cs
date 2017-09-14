using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSwipeManager : MonoBehaviour
{
	// Modified
	private void OnGameOver()
	{
		if (Globals.currentRoom.roomID >= 0 && !HardcoreArena.hardcoreModeActive)
		{
			this.skipTransitionActive = true;
			return;
		}
		if (HardcoreArena.hardcoreModeActive && DeathSwipeManager.checkpointCheat)
		{
			this.skipTransitionActive = true;
			Globals.warpedIntoRoom = true;
			return;
		}
		if (Globals.currentRoom.roomID < 0)
		{
			this.skipTransitionActive = true;
			return;
		}
		if (!Globals.currentRoom.xWrapped && !Globals.currentRoom.yWrapped)
		{
			this.shiftBackConst = 30f;
		}
		Vector3 position = Globals.player2.transform.position;
		if (this.p1LastPlayerAlive)
		{
			position = Globals.player.transform.position;
		}
		if (Globals.currentRoom.xWrapped || Globals.currentRoom.yWrapped)
		{
			DeathSwipeManager.deathInWrap = true;
		}
		this.hardcoreDeathSphere.transform.position = position + Vector3.back * this.shiftBackConst;
		this.hardcoreDeathSphere.localScale = Vector3.one * 3f;
		this.hardcoreDeathSphereRenderer.material.SetVector("_Pos", position);
		this.hardcoreDeathSphereRenderer.material.SetFloat("_Radius", 1.25f);
		this.hardcoreDeathSphere.gameObject.SetActive(true);
	}

    // Modified
	private void GameOverLoadLevel()
	{
		this.deathFinished = true;
		if (HardcoreArena.hardcoreModeActive)
		{
			if (DeathSwipeManager.checkpointCheat)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				return;
			}
			if (Globals.currentLevelID < 8)
			{
				Globals.currentLevelID = 0;
				Globals.currentGlobalRoomID = 0;
				Globals.currentLevelName = "Basic";
				RoomMusic.GetInstance().StartRoomSound();
				GC.Collect();
				SceneManager.LoadScene("Basic-1-HARDCORE");
				return;
			}
			Globals.currentLevelID = 8;
			Globals.currentGlobalRoomID = 32;
			Globals.currentLevelName = "Scalers";
			RoomMusic.GetInstance().StartRoomSound();
			this.deathFinished = true;
			GC.Collect();
			SceneManager.LoadScene("Scalers-1-HARDCORE");
			return;
		}
		else
		{
			Globals.musicTimeSinceLevelLoad = 0f;
			if (!DeathSwipeManager.checkpointCheat && Globals.currentRoom.roomID > 0)
			{
				Globals.currentGlobalRoomID = Globals.currentLevelID * 4;
			}
			GC.Collect();
			if (Globals.currentRoom.roomID < 0)
			{
				SceneManager.LoadScene("Procedural-1");
				return;
			}
			if (Globals.currentLevelName == string.Empty || DeathSwipeManager.checkpointCheat)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				return;
			}
			SceneManager.LoadScene(Globals.currentLevelName + "-1");
			return;
		}
	}

    //New
	public static bool checkpointCheat;
}
