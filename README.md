# Thoth Speedrun Mod

### [Download here](https://github.com/rmadlal/thoth-mod/releases)

## Installation

1. Go to your game's installation location (by default: `C:\Program Files (x86)\Steam\steamapps\common\THOTH\THOTH_Data\Managed` (on Windows)).
2. Back-up the file Assembly-CSharp.dll. You can rename the file to any other name (e.g Assembly-CSharp-backup.dll), or move it someplace else.
3. Copy the new Assembly-CSharp.dll into this folder.

## Features

### Game behaviour

- Window size is determined via the `settings.txt` file located in the main folder of the game (`..\THOTH`).  
It also stores the last window size for the next time the game is launched.
- The game doesn't pause in the background.
- You can skip the ending cutscenes with `Esc`.

### Timers

A loadless timer is displayed In the top-left, and real-time timer below it.  
Press T to toggle additional IL timers at the top-right: in-game timer and previous level time.

### Cheats

| Key Combo | Effect |
| :---: | --- |
| `c` `h` `e` `a` `t` | Enable cheats and unlock all levels. |
| `01`-`64` | Warp to the specified level
| `Alt` + `C` | Toggle checkpoint cheat |
| `Alt` + `R` | Toggle repeating level cheat |
| `Alt` + `L` | Toggle lava always on cheat [*] |
| `Alt` + `I` [**]| Toggle warping to inverted levels |
| `N` | Complete the current level |

\* In the Legacy version, `Alt` + `L` toggles warping to Lava levels.  
\** Only in the latest version of Thoth.