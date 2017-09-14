using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSwipeManager : MonoBehaviour
{
	// Modified
	private void GameOverLoadLevel()
	{
		this.deathFinished = true;
		if (Globals.lavaAlwaysOn)
		{
			Room.anotherChance = true;
		}
		if (Globals.checkpointCheat)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			return;
		}
		if (HardcoreArena.hardcoreModeActive)
		{
			if (Globals.currentLevelID < 8)
			{
				if (!Room.anotherChance)
				{
					if (Globals.currentRoom.roomID == 1 && Globals.currentLevelID == 0)
					{
						Room.anotherChance = false;
					}
					else
					{
						Room.anotherChance = true;
						DeathSwipeManager.playLavaEnableSound = true;
					}
					GC.Collect();
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					return;
				}
				if (!NewMenu.cheatsEnabled || !Globals.lavaAlwaysOn)
				{
					Room.anotherChance = false;
				}
				Globals.currentLevelID = 0;
				Globals.currentGlobalRoomID = 0;
				Globals.currentLevelName = "Basic";
				RoomMusic.GetInstance().StartRoomSound();
				GC.Collect();
				SceneManager.LoadScene("Basic-1-HARDCORE");
				return;
			}
			else
			{
				if (!Room.anotherChance)
				{
					if (Globals.currentRoom.roomID == 1 && Globals.currentLevelID == 8)
					{
						Room.anotherChance = false;
					}
					else
					{
						Room.anotherChance = true;
						DeathSwipeManager.playLavaEnableSound = true;
					}
					GC.Collect();
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					return;
				}
				if (!NewMenu.cheatsEnabled || !Globals.lavaAlwaysOn)
				{
					Room.anotherChance = false;
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
		}
		else
		{
			Globals.musicTimeSinceLevelLoad = 0f;
			if (Globals.currentRoom.roomID > 0 && Room.anotherChance)
			{
				Globals.currentGlobalRoomID = Globals.currentLevelID * 4;
			}
			GC.Collect();
			if (Globals.currentRoom.roomID < 0)
			{
				SceneManager.LoadScene("Procedural-1");
				return;
			}
			if (Globals.currentLevelName == string.Empty)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				return;
			}
			if (!Room.anotherChance)
			{
				if (Globals.currentRoom.roomID == 1)
				{
					Room.anotherChance = false;
				}
				else
				{
					Room.anotherChance = true;
					DeathSwipeManager.playLavaEnableSound = true;
				}
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				return;
			}
			if (!NewMenu.cheatsEnabled || !Globals.lavaAlwaysOn)
			{
				Room.anotherChance = false;
			}
			SceneManager.LoadScene(Globals.currentLevelName + "-1");
			return;
		}
	}
}
