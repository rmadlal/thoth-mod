using System;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRoom : MonoBehaviour
{
    // Modified
    private void Awake()
    {
        if (Globals.currentLevelID <= 0)
        {
            if (this.room.roomID == -1)
            {
                Globals.currentLevelID = 16;
            }
            else if (this.room.roomID == -2)
            {
                Globals.currentLevelID = 17;
            }
            if (this.room.roomID == -3)
            {
                Globals.currentLevelID = 18;
            }
        }
        if (Globals.currentLevelID == 16)
        {
            this.currentLevelSequence = this.proceduralLevel1;
            ProceduralRoom.roomCounterTarget = 8;
        }
        else if (Globals.currentLevelID == 17)
        {
            this.currentLevelSequence = this.proceduralLevel2;
            ProceduralRoom.roomCounterTarget = 12;
        }
        else if (Globals.currentLevelID == 18)
        {
            this.currentLevelSequence = this.proceduralLevel3;
            ProceduralRoom.roomCounterTarget = 32;
        }
        if (ProceduralRoom.playerDied && !ProceduralRoom.respawnInLava && !NewMenu.checkpointCheat)
        {
            ProceduralRoom.roomCounter = 0;
            ProceduralRoom.lastAssignedSpecial = ProceduralRoom.SpecialTypes.none;
            ProceduralRoom.playerDied = false;
            Room.anotherChance = NewMenu.lavaAlwaysOn;
        }
        else if (ProceduralRoom.playerDied)
        {
            ProceduralRoom.playerDied = false;
        }
        if (NewMenu.lavaAlwaysOn)
        {
            Room.anotherChance = true;
        }
        // from here unchanged
        if (ProceduralRoom.roomCounter == 0)
        {
            BuildProcEndingData.ClearData();
            ProceduralRoom.preGeneratedPatternsList.Clear();
            List<int> list = new List<int>();
            for (int i = 0; i < this.currentLevelSequence.transform.childCount; i++)
            {
                list.Clear();
                for (int j = 0; j < this.currentLevelSequence.transform.GetChild(i).childCount; j++)
                {
                    list.Add(j);
                }
                for (int k = 0; k < this.currentLevelSequence.levelCount[i]; k++)
                {
                    int item = list[UnityEngine.Random.Range(0, list.Count)];
                    ProceduralRoom.preGeneratedPatternsList.Add(item);
                    list.Remove(item);
                }
            }
        }
        this.addBallsAfterGeneration = false;
        this.addTeleportersAfterGeneration = false;
        if (this.testPattern != null)
        {
            this.GenerateLevel(this.testPattern);
            this.AddArenaSpeciel(this.testPattern);
            this.AddBallsAndTeleporters();
            this.testPattern.gameObject.SetActive(false);
        }
        else
        {
            int index = this.RoomCounterToLevelID();
            Debug.Log("roomCounterAtLevelGen " + ProceduralRoom.roomCounter);
            Transform child = this.currentLevelSequence.transform.GetChild(index);
            Transform child2 = child.GetChild(ProceduralRoom.preGeneratedPatternsList[ProceduralRoom.roomCounter]);
            if (this.testPatternFolder)
            {
                child = this.testPatternFolder;
                child2 = child.GetChild(UnityEngine.Random.Range(0, child.childCount));
            }
            this.GenerateLevel(child2.GetComponent<ProceduralPattern>());
            this.AddArenaSpeciel(child2.GetComponent<ProceduralPattern>());
            this.AddBallsAndTeleporters();
            for (int l = 0; l < this.bouncersAdded.Count; l++)
            {
                Transform transform = this.bouncersAdded[l].transform;
                if (this.addBallsAfterGeneration)
                {
                    BuildProcEndingData.AddData(ProceduralRoom.roomCounter, transform.transform.position, transform.transform.localScale, BuildProcEndingData.EndShapes.squareWithBullet);
                }
                else if (this.addTeleportersAfterGeneration)
                {
                    BuildProcEndingData.AddData(ProceduralRoom.roomCounter, transform.transform.position, transform.transform.localScale, BuildProcEndingData.EndShapes.squareWithTeleporter);
                }
                else
                {
                    BuildProcEndingData.AddData(ProceduralRoom.roomCounter, transform.transform.position, transform.transform.localScale, BuildProcEndingData.EndShapes.square);
                }
            }
        }
        ProceduralRoom.respawnInLava = false;
    }

    // Modified
    private void Start()
    {
        // change at end of function
        Room currentRoom = Globals.currentRoom;
        currentRoom.gameoverEvent = (Room.RoomEvent)Delegate.Combine(currentRoom.gameoverEvent, new Room.RoomEvent(this.OnGameOver));
        Room currentRoom2 = Globals.currentRoom;
        currentRoom2.roomCompletedEvent = (Room.RoomEvent)Delegate.Combine(currentRoom2.roomCompletedEvent, new Room.RoomEvent(this.OnRoomCompleted));
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
        if (!NewMenu.cheatRepeatLevel || ProceduralRoom.roomCounter <= 0)
        {
            ProceduralRoom.roomCounter++;
        }
        if (ProceduralRoom.roomCounterTarget == 12 && ProceduralRoom.roomCounter == 12 && !NewMenu.cheatRepeatLevel)
        {
            Globals.currentRoom.transitionToStars = true;
        }
    }

    // Modified
    private void OnGameOver()
    {
        ProceduralRoom.playerDied = true;
        if (NewMenu.checkpointCheat)
        {
            ProceduralRoom.roomCounter--;
            return;
        }
        if (!Room.anotherChance && ProceduralRoom.roomCounter > 1)
        {
            Room.anotherChance = true;
            ProceduralRoom.respawnInLava = true;
            ProceduralRoom.roomCounter--;
            DeathSwipeManager.playLavaEnableSound = true;
        }
    }
}
