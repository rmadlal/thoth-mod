using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
	// Modified
	private void OnDestroy()
	{
		if (BossMusic.GetInstance() && !DeathSwipeManager.playLavaEnableSound && ((this.roomCompleted && !Globals.cheatRepeatLevel) || (this.gameover && !Globals.checkpointCheat)))
        {
			BossMusic.GetInstance().StopBossMusic(true);
		}
	}

    // Modified
	private void UpdateSliceIn()
	{
		this.roomTimer += Time.deltaTime;
		if (Globals.roomNumbers.GetIsNewNumberReady() && !DeathSwipeManager.respawnActive)
		{
			Globals.roomNumbers.FinishSlide();
			if (!Globals.cheatRepeatLevel)
			{
				if (this.roomID == 4)
				{
					BossMusic.GetInstance().PlayBossMusic();
				}
				else if (this.roomID < 0 && ProceduralRoom.roomCounter == ProceduralRoom.roomCounterTarget)
				{
					BossMusic.GetInstance().PlayBossMusic();
				}
			}
            // from here unchanged
			this.SetReadyState();
			this.wallsToColor[0].transform.position = this.walls0StartPos;
			this.wallsToColor[0].GetComponent<Collider>().isTrigger = false;
			if (this.disableWall0WhenSlideDone)
			{
				this.wallsToColor[0].gameObject.SetActive(false);
			}
			Globals.player.gameObject.SetActive(true);
			if (Room.coopRoom)
			{
				Globals.player2.gameObject.SetActive(true);
			}
			this.numberSlideActive = false;
		}
	}

    // Modified
	private void SetReadyEventsAndState()
	{
        if (Globals.inGameTime == null)
        {
            Room.roomStartedTime = TimeSpan.Zero;
        }
        else if (!Room.anotherChance || Globals.lavaAlwaysOn)
        {
            Room.roomStartedTime = Globals.inGameTime.Elapsed;
        }
        if (Globals.realTimeTimer == null)
        {
            Globals.realTimeTimer = Stopwatch.StartNew();
            Globals.loadlessTimer = Stopwatch.StartNew();
            Globals.inGameTime = Stopwatch.StartNew();
        }
        else
        {
            Globals.inGameTime.Start();
        }
        // from here unchanged
		if (this.doPlayFlowerSound)
		{
			this.doPlayFlowerSound = false;
			GeneralSoundBank.GetInstance().PlayFlowerSound();
		}
		this.myState = Room.MyState.ready;
		this.roomTimer = 0f;
		this.roomReady = true;
		this.playersAreActive = true;
		if (this.roomPlayersActiveEvent != null)
		{
			this.roomPlayersActiveEvent();
		}
		if (this.roomReadyEvent != null)
		{
			this.roomReadyEvent();
		}
	}

    // Modified
	private void SetGameOverState()
	{
		Room.spawnFromGameOver = true;
		this.myState = Room.MyState.gameOver;
		if (HardcoreArena.hardcoreModeActive && Room.anotherChance && RoomMusic.GetInstance() && !Globals.checkpointCheat)
		// from here unchanged
        {
			RoomMusic.GetInstance().StopRoomMusic(1f);
			BossMusic.GetInstance().StopBossMusic(false);
		}
		this.gameover = true;
		Globals.bulletPool.ClearAllBullets();
		if (this.gameoverEvent != null)
		{
			this.gameoverEvent();
		}
		this.roomTimer = 0f;
		this.setGameOverTimeScale = true;
	}

    // Modified
    private void SetSliceInState()
    {
        if (Room.doSlideAtStart)
        {
            Globals.loadlessTimer.Start();
            // from here unchanged
            this.roomTimer = -0f;
            this.myState = Room.MyState.slicingIn;
            this.walls0StartPos = this.wallsToColor[0].transform.position;
            if (!this.wallsToColor[0].gameObject.activeSelf)
            {
                this.wallsToColor[0].gameObject.SetActive(true);
                this.disableWall0WhenSlideDone = true;
            }
            this.wallsToColor[0].transform.position = Vector3.zero + Vector3.back * 10f;
            this.wallsToColor[0].GetComponent<Collider>().isTrigger = true;
            for (int i = 1; i < this.wallsToColor.Count; i++)
            {
                this.wallsToColor[i].GetComponent<Renderer>().enabled = false;
            }
            Globals.player.gameObject.SetActive(false);
            if (Room.coopRoom)
            {
                Globals.player2.gameObject.SetActive(false);
            }
            Globals.roomNumbers.SlideNumber();
            if (GeneralSoundBank.GetInstance())
            {
                GeneralSoundBank.GetInstance().PlayLevelComplete(this.roomID);
            }
            this.numberSlideActive = true;
            return;
        }
        this.SetReadyWhenReadySate();
    }

    // Modified
	private void SetSliceOutState()
	{
        Globals.inGameTime.Stop();
        if (!Globals.cheatRepeatLevel && (Globals.currentGlobalRoomID == 63 || (Globals.currentLevelID == 17 && ProceduralRoom.roomCounter == ProceduralRoom.roomCounterTarget)))
        {
            Globals.realTimeTimer.Stop();
            Globals.loadlessTimer.Stop();
        }
        if (!Globals.lavaAlwaysOn)
        {
            Room.anotherChance = false;
        }
        if (!Room.anotherChance || Globals.lavaAlwaysOn)
        {
            Globals.prevRoomTime = Globals.inGameTime.Elapsed.Subtract(Room.roomStartedTime);
        }
		if (Globals.cheatRepeatLevel)
		{
			this.transitionToStars = false;
		}
		for (int i = 0; i < this.wallsToColor.Count; i++)
		{
			if (Room.anotherChance && i > 3 && !this.transitionToStars)
			{
				this.wallsToColor[i].material.color = Globals.instance.levelEnemyMaterials[Globals.currentLevelID].color;
				this.wallsToColor[i].enabled = true;
			}
		}
		if (BossMusic.GetInstance() && !Globals.cheatRepeatLevel)
            // from here unchanged
		{
			BossMusic.GetInstance().StopBossMusic(false);
			if (this.roomID == 3 && !this.endOfTrailer)
			{
				BossMusic.GetInstance().PlayBossCymbal();
				RoomMusic.GetInstance().StopRoomMusic(0.025f);
			}
			else if (this.roomID < 0 && ProceduralRoom.roomCounter == ProceduralRoom.roomCounterTarget - 1)
			{
				BossMusic.GetInstance().PlayBossCymbal();
				RoomMusic.GetInstance().proceduralMusic.Stop(0.1f);
			}
		}
		this.myState = Room.MyState.slicingOut;
		for (int j = 0; j < this.receivers.Count; j++)
		{
			this.receivers[j].SetCompleteState();
		}
		Room.doSlideAtStart = true;
		this.roomCompleted = true;
		this.roomTimer = 0f;
		if (this.roomCompletedEvent != null)
		{
			this.roomCompletedEvent();
		}
		if (this.transitionToStars)
		{
			PlayerCursor.DisableCursor();
			for (int k = 0; k < this.receivers.Count; k++)
			{
				this.receivers[k].gameObject.SetActive(false);
			}
		}
		Globals.roomNumbersExtra.EnableEndOfLevelTransition();
		Globals.bulletPool.ClearAllBullets();
	}

    // Modified
	private void UpdateSliceOut()
	{
		this.roomTimer += Time.deltaTime * 0.65f;
		float num = 0.5f;
		if (this.roomID == 4)
		{
			num = 1.6f;
		}
		if (this.endOfTrailer)
		{
			return;
		}
        // add "Globals.loadlessTimer.Stop();" after each occurence of "this.myState = Room.MyState.loading;"
		if (Globals.cheatRepeatLevel)
		{
            GC.Collect();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            this.myState = Room.MyState.loading;
            Globals.loadlessTimer.Stop();
            return;
		}
		if ((this.roomID != 4 && Globals.roomNumbersExtra.GetIsCompleted()) || (this.roomID == 4 && Globals.roomNumbersExtra.GetIsCompleted() && this.roomTimer > num))
		{
			GC.Collect();
			if (this.roomID < 0)
			{
				if (ProceduralRoom.roomCounter < ProceduralRoom.roomCounterTarget)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					this.myState = Room.MyState.loading;
                    Globals.loadlessTimer.Stop();
				}
                else
                {
                    Globals.currentLevelID++;
                    if (!NewMenu.cheatsEnabled && Globals.save_progress < Globals.currentLevelID)
                    {
                        Globals.save_progress = Globals.currentLevelID;
                        SaveLoad.Save();
                    }
                    ProceduralRoom.lastAssignedSpecial = ProceduralRoom.SpecialTypes.none;
                    ProceduralRoom.roomCounter = 0;
                    if (Globals.currentLevelID == 17)
                    {
                        SceneManager.LoadScene("Procedural-ENDING1");
                    }
                    else if (Globals.currentLevelID == 18)
                    {
                        SceneManager.LoadScene("Proc-FINALENDING");
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    this.myState = Room.MyState.loading;
                    Globals.loadlessTimer.Stop();
                }
                return;
			}
            if (HardcoreArena.hardcoreModeActive && Globals.currentLevelID == 7 && this.roomID == 4)
            {
                Debug.Log("Unlocked final lava challenge");
                if (!NewMenu.cheatsEnabled && Globals.save_progress < 20)
                {
                    Globals.save_progress = 20;
                    SaveLoad.Save();
                }
            }
            else if (HardcoreArena.hardcoreModeActive && Globals.currentLevelID == 15 && this.roomID == 4)
            {
                Debug.Log("Completed final challenge");
                if (!NewMenu.cheatsEnabled && Globals.save_progress < 21)
                {
                    Globals.save_progress = 21;
                    SaveLoad.Save();
                }
            }
            Globals.currentGlobalRoomID++;
            this.nextRoomID = this.roomID + 1;
            if (Globals.currentLevelID == 15 && this.roomID == 4)
            {
                Globals.currentLevelID++;
                if (!NewMenu.cheatsEnabled && Globals.save_progress < Globals.currentLevelID)
                {
                    Globals.save_progress = Globals.currentLevelID;
                    SaveLoad.Save();
                }
                if (!this.nextRoomHasHardcoreVersion)
                {
                    SceneManager.LoadScene("Final4-ENDING");
                }
                else
                {
                    Globals.currentLevelID = 19;
                    SceneManager.LoadScene("Final4-ENDINGHARDCORE");
                }
                this.myState = Room.MyState.loading;
                Globals.loadlessTimer.Stop();
                return;
            }
            if (this.roomID == 4 && Globals.currentLevelID != 15)
            {
                Globals.currentLevelID++;
                if (!NewMenu.cheatsEnabled && Globals.save_progress < Globals.currentLevelID)
                {
                    Globals.save_progress = Globals.currentLevelID;
                    SaveLoad.Save();
                }
                this.roomFinishedLoadActive = true;
                Globals.currentLevelName = Globals.instance.levelNames[Globals.currentLevelID];
                this.nextRoomID = 1;
            }
            if (HardcoreArena.hardcoreModeActive)
            {
                if (this.nextRoomHasHardcoreVersion)
                {
                    SceneManager.LoadScene(string.Concat(new object[]
                    {
                        Globals.currentLevelName,
                        "-",
                        this.nextRoomID,
                        "-HARDCORE"
                    }));
                }
                else
                {
                    SceneManager.LoadScene(Globals.currentLevelName + "-" + this.nextRoomID);
                }
            }
            else
            {
                SceneManager.LoadScene(Globals.currentLevelName + "-" + this.nextRoomID);
            }
            this.myState = Room.MyState.loading;
            Globals.loadlessTimer.Stop();
        }
	}

    // Modified
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Globals.showRealTimeAndILTime = !Globals.showRealTimeAndILTime;
		}
        else if (NewMenu.cheatsEnabled && Input.GetKeyDown(KeyCode.N))
        {
            Globals.currentRoom.CheatRoomComplete();
        }
		else if (Globals.currentGlobalRoomID == 63 && this.myState == Room.MyState.slicingOut && Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("NewMenu");
			GeneralSoundBank.GetInstance().StopEndTheme();
			Globals.currentGlobalRoomID = -1;
		}
		if (this.gameover)
		{
			this.UpdateGameOver();
		}
	}

    // New
	public Room.MyState GetRoomState()
	{
		return this.myState;
	}

    // New
	public TimeSpan roomStartedTimeSpan;

    // New
	public static TimeSpan roomStartedTime;
}
