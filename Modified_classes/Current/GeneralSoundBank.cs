using System;
using UnityEngine;

public class GeneralSoundBank : MonoBehaviour
{
	// New
	public void StopEndTheme()
	{
		this.endGameTheme.GetComponent<AudioSource>().Stop();
		this.endGameThemeLAVA.GetComponent<AudioSource>().Stop();
		this.endGameThemeProcedural.GetComponent<AudioSource>().Stop();
	}
}
