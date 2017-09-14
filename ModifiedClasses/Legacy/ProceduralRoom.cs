using System;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoom : MonoBehaviour
{
	// Modified
	private void Start()
	{
		Room expr_05 = Globals.currentRoom;
		expr_05.gameoverEvent = (Room.RoomEvent)Delegate.Combine(expr_05.gameoverEvent, new Room.RoomEvent(this.OnGameOver));
		Vector3 position = this.activePattern.player1Start.position;
		if (Room.coopRoom)
		{
			position = this.activePattern.player1StartCoop.position;
		}
		if (this.mirrorSign < 0f)
		{
			if (this.activePattern.isHorizontal)
			{
				position.y = -position.y;
			}
			else
			{
				position.x = -position.x;
			}
		}
		Globals.player.transform.position = position;
		if (Room.coopRoom)
		{
			Vector3 position2 = this.activePattern.player2Start.position;
			if (this.mirrorSign < 0f)
			{
				if (this.activePattern.isHorizontal)
				{
					position2.y = -position2.y;
				}
				else
				{
					position2.x = -position2.x;
				}
			}
			Globals.player2.transform.position = position2;
		}
		if (Room.coopRoom)
		{
			if (Globals.player2.transform.position.y > Globals.player.transform.position.y)
			{
				Vector3 position3 = Globals.player.transform.position;
				Globals.player.transform.position = Globals.player2.transform.position;
				Globals.player2.transform.position = position3;
			}
			if (Mathf.Abs(Globals.player2.transform.position.y - Globals.player.transform.position.y) < 0.25f && Globals.player2.transform.position.x < Globals.player.transform.position.x)
			{
				Vector3 position4 = Globals.player.transform.position;
				Globals.player.transform.position = Globals.player2.transform.position;
				Globals.player2.transform.position = position4;
			}
		}
		if (!NewMenu.repeatLevel || ProceduralRoom.roomCounter <= 0)
		{
			ProceduralRoom.roomCounter++;
		}
		if (ProceduralRoom.roomCounterTarget == 16 && ProceduralRoom.roomCounter == 16 && !NewMenu.repeatLevel)
		{
			Globals.currentRoom.transitionToStars = true;
		}
	}
}
