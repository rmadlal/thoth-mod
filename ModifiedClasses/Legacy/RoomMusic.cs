using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusic : MonoBehaviour
{
	// Modified
	public void RoomStarted()
	{
		if (NewMenu.repeatLevel)
		{
			return;
		}
		if (Globals.currentLevelID <= 15)
		{
			RoomMusic.instance.musicList[Globals.currentLevelID].RestartAfterBossFade();
		}
	}

    // Modified
	private void Update()
	{
		this.UpdateTrailerLogic();
		if (this.teleportActive)
		{
			this.timeSinceTeleport += Time.deltaTime;
			if (this.timeSinceTeleport > 0.05f)
			{
				this.teleporterVolScale = Mathf.MoveTowards(this.teleporterVolScale, 1f, Time.deltaTime * 4f);
			}
			if (this.teleporterVolScale >= 1f)
			{
				this.teleportActive = false;
			}
		}
		if (Globals.currentRoom)
		{
			if (!this.musicChanged && Globals.currentRoom.IsRoomCompleted() && ((Globals.currentRoom.roomID == 4 && !NewMenu.repeatLevel) || (Globals.currentRoom.roomID < 0 && ProceduralRoom.roomCounter == ProceduralRoom.roomCounterTarget)))
			{
				if (Globals.currentRoom.roomID >= 0)
				{
					this.musicList[Globals.currentLevelID].StopTrack(1f);
					if (!Globals.currentRoom.transitionToStars && !Globals.currentRoom.endOfTrailer)
					{
						this.musicList[Globals.currentLevelID + 1].PlayTrack(false);
						this.musicList[Globals.currentLevelID + 1].usedMarkerLastTime = false;
					}
					else if (Globals.currentRoom.transitionToStars)
					{
						GeneralSoundBank.GetInstance().PlayEndGameTheme();
					}
					this.musicChanged = true;
				}
				else if (Globals.currentRoom.transitionToStars)
				{
					this.proceduralMusic.Stop(0.25f);
					GeneralSoundBank.GetInstance().PlayEndGameProceduralTheme();
					this.musicChanged = true;
				}
			}
			if (!Globals.currentRoom.IsRoomCompleted())
			{
				this.musicChanged = false;
			}
		}
	}
}
