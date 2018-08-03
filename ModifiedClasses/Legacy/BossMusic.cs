using System;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    // New
    public void ForcePlayBossMusic(int which)
    {
        switch (which)
        {
        case 0:
            this.currentBossMusic = this.standardBossMusic;
            break;
        case 1:
            this.currentBossMusic = this.proceduralBoss1;
            break;
        case 2:
            this.currentBossMusic = this.proceduralBoss2;
            break;
        case 3:
            this.currentBossMusic = this.proceduralBoss3;
            break;
        default:
            this.currentBossMusic = this.standardBossMusic;
            break;
        }
        this.currentBossMusic.Play();
        if (this.currentBossMusic.duckCymbal && this.currentCymbal != null)
        {
            this.currentCymbal.volume = this.cymbalVolume * 0.65f;
        }
        RoomMusic.GetInstance().StopRoomMusic(1f);
    }

    // New
    public BossTrack GetCurrentBossMusic()
    {
        return this.currentBossMusic;
    }
}
