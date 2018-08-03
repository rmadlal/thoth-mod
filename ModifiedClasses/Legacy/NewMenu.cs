using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMenu : MonoBehaviour
{
    // Modified
    private void Awake()
    {
        if (BossMusic.GetInstance())
        {
            BossMusic.GetInstance().StopBossMusic(true);
        }
        Globals.inGameTime = null;
        Globals.realTimeTimer = null;
        Globals.loadlessTimer = null;
        Globals.prevRoomTime = TimeSpan.Zero;
        HardcoreArena.hardcoreModeActive = false;
        DeathSwipeManager.respawnActive = false;
        this.progress = Globals.save_progress + 1;
        Debug.Log("progress : " + this.progress);
        if (this.progress > 20)
        {
            this.progress = 20;
        }
        this.selectedNumberID = Globals.currentLevelID;
        if (this.selectedNumberID > 19)
        {
            this.selectedNumberID = 19;
        }
        Time.timeScale = 1f;
        if (Globals.currentLevelID < 0)
        {
            this.selectedNumberID = this.progress - 1;
        }
    }

    // Modified
    private void Start()
    {
        GC.Collect();
        PlayerInput.lockPlayer1ToKeyboardAndMouse = false;
        RoomMusic.GetInstance().StopRoomMusic(1f);
        Room.doSlideAtStart = false;
        this.cheatCode = new KeyCode[5];
        this.cheatCode[0] = KeyCode.C;
        this.cheatCode[1] = KeyCode.H;
        this.cheatCode[2] = KeyCode.E;
        this.cheatCode[3] = KeyCode.A;
        this.cheatCode[4] = KeyCode.T;
        this.roomWarpCode = new int[2];
        this.roomWarpIndex = 0;
        this.nonHardcoreLevels = new int[]
        {
            59,
            48,
            44,
            43,
            40,
            37,
            27,
            34,
            24,
            12,
            7,
            3
        };
        // from here unchanged
        if (Room.coopRoom)
        {
            this.EnableCoop();
        }
        int num = this.progress;
        if (NewMenu.cheatsEnabled)
        {
            num = 20;
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int num2 = i * 4 + j;
                if (num2 >= this.numbersList.Count)
                {
                    break;
                }
                float arg_F7_0 = -23.039999f + (float)i * 11.5199995f;
                float num3 = 9.09f - (float)j * 6.06f;
                Vector3 vector = new Vector3(arg_F7_0, num3 + -2f, 0f) * 1.25f;
                this.numbersList[num2].transform.position = vector;
                this.numbersList[num2].transform.localScale = Vector3.one * 1.25f;
                this.numbersList[num2].transform.gameObject.SetActive(true);
                Material material = new Material(this.greyWithMask);
                this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material = material;
                this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material = material;
                this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", this.grey.color);
                this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color1", this.grey.color);
                this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color2", this.grey.color);
                this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color3", this.grey.color);
                this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", this.grey.color);
                this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color1", this.grey.color);
                this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color2", this.grey.color);
                this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color3", this.grey.color);
                if (num2 < num)
                {
                    if (num2 <= 15 || num2 >= 18)
                    {
                        Color color = Globals.instance.levelBGNumberMaterials[num2].color;
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", color);
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color1", color);
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color2", color);
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color3", color);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", color);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color1", color);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color2", color);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color3", color);
                    }
                    else
                    {
                        Color color2 = Globals.instance.levelBGNumberMaterials[num2].GetColor("_Color");
                        Color color3 = Globals.instance.levelBGNumberMaterials[num2].GetColor("_Color1");
                        Color color4 = Globals.instance.levelBGNumberMaterials[num2].GetColor("_Color2");
                        Color color5 = Globals.instance.levelBGNumberMaterials[num2].GetColor("_Color3");
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", color2);
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color1", color3);
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color2", color4);
                        this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color3", color5);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", color2);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color1", color3);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color2", color4);
                        this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color3", color5);
                        if (num2 == 16)
                        {
                            this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_YOffset", 1.2f);
                            this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetFloat("_YOffset", 1.2f);
                        }
                        else
                        {
                            this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_YOffset", -0.1f + 0.2f * (float)(num2 - 15));
                            this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetFloat("_YOffset", -0.1f + 0.2f * (float)(num2 - 15));
                        }
                    }
                }
                this.numbersList[num2].GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_TopMaskY", vector.y + 3.57f);
                this.numbersList[num2].GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetFloat("_BottomMaskY", vector.y - 3.57f - 1f);
            }
        }
        Debug.Log("selectedNumberID : " + this.selectedNumberID);
        this.SelectNumber(this.selectedNumberID);
    }

    // Modified
    private void UpdateCheat()
    {
        if (!NewMenu.cheatsEnabled && Input.anyKeyDown && Input.GetKeyDown(this.cheatCode[this.cheatIndex]))
        {
            this.cheatIndex++;
            if (this.cheatIndex == this.cheatCode.Length)
            {
                NewMenu.cheatsEnabled = true;
                SceneManager.LoadScene("NewMenu");
                this.loadingLevel = true;
                return;
            }
        }
        else
        {
            if (NewMenu.cheatsEnabled && Input.anyKeyDown)
            {
                for (KeyCode keyCode = KeyCode.Keypad0; keyCode <= KeyCode.Keypad9; keyCode++)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        this.ProcessWarpCode(keyCode - KeyCode.Keypad0);
                        return;
                    }
                }
                if (Input.GetKeyDown(KeyCode.C) && Event.current.alt)
                {
                    DeathSwipeManager.checkpointCheat = !DeathSwipeManager.checkpointCheat;
                }
                else if (Input.GetKeyDown(KeyCode.R) && Event.current.alt)
                {
                    NewMenu.repeatLevel = !NewMenu.repeatLevel;
                }
                else if (Input.GetKeyDown(KeyCode.L) && Event.current.alt)
                {
                    NewMenu.warpToLava = !NewMenu.warpToLava;
                }
                this.roomWarpIndex = 0;
                return;
            }
            if (Input.anyKeyDown)
            {
                this.cheatIndex = 0;
            }
        }
    }

    // Modified
    private void Update()
    {
        // change at end of function
        if (this.loadingLevel)
        {
            return;
        }
        this.UpdateCoopEnabled();
        if (!this.isReady)
        {
            return;
        }
        this.UpdateCursorMove();
        if (PlayerInput.cursorActive && this.IsMenuReady())
        {
            this.UpdateMOUSECursorMove();
            this.UpdateMouseCoopToggle();
            this.stickHasRested = true;
        }
        else if (PlayerInput.cursorActive && !this.IsMenuReady())
        {
            this.SelectNumber(-1);
        }
        this.UpdateChooseLevel();
        this.UpdateCheat();
    }

    // New
    private void ProcessWarpCode(int digit)
    {
        int[] arg_18_0 = this.roomWarpCode;
        int num = this.roomWarpIndex;
        this.roomWarpIndex = num + 1;
        arg_18_0[num] = digit;
        if (this.roomWarpIndex == this.roomWarpCode.Length)
        {
            this.roomWarpIndex = 0;
            int num2 = this.roomWarpCode[0] * 10 + this.roomWarpCode[1];
            if (num2 >= 1 && num2 <= 64)
            {
                this.WarpToLevel(num2);
            }
        }
    }

    // New
    private void WarpToLevel(int levelNum)
    {
        this.loadingLevel = true;
        Globals.warpedIntoRoom = true;
        Globals.currentGlobalRoomID = 64 - levelNum;
        Globals.currentLevelID = (64 - levelNum) / 4;
        int num = (64 - levelNum) % 4 + 1;
        this.selectedNumberID = Globals.currentLevelID;
        if (NewMenu.coopEnabled && !PlayerInput.TwoControllersConnected())
        {
            PlayerInput.lockPlayer1ToKeyboardAndMouse = true;
        }
        else
        {
            PlayerInput.lockPlayer1ToKeyboardAndMouse = false;
        }
        GeneralSoundBank.GetInstance().LowerTitleScreenPressed();
        Globals.currentLevelName = Globals.instance.levelNames[this.selectedNumberID];
        RoomMusic.GetInstance().StartRoomSound();
        HardcoreArena.hardcoreModeActive = NewMenu.warpToLava;
        if (num == 4)
        {
            BossMusic.GetInstance().ForcePlayBossMusic(0);
        }
        SceneManager.LoadScene(string.Concat(new object[]
        {
            string.Empty,
            Globals.instance.levelNames[this.selectedNumberID],
            "-",
            num,
            (NewMenu.warpToLava && Array.IndexOf<int>(this.nonHardcoreLevels, levelNum) == -1) ? "-HARDCORE" : ""
        }));
    }

    // New
    private void OnGUI()
    {
        if (NewMenu.cheatsEnabled)
        {
            if (DeathSwipeManager.checkpointCheat)
            {
                GUI.Label(new Rect(10f, 30f, 150f, 100f), "Checkpoint cheat enabled");
            }
            if (NewMenu.repeatLevel)
            {
                GUI.Label(new Rect(10f, 50f, 150f, 100f), "Repeat completed level");
            }
            if (NewMenu.warpToLava)
            {
                GUI.Label(new Rect(10f, 70f, 150f, 100f), "Warp to Lava levels");
            }
        }
    }

    // New
    private int[] roomWarpCode;

    // New
    public int roomWarpIndex;

    // New
    public static bool repeatLevel;

    // New
    public static bool warpToLava;

    // New
    public int[] nonHardcoreLevels;
}
