using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	public List<GameObject> SoundEffects;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void CreateSound(int index)
	{
		GameObject NewSound = Instantiate(SoundEffects[index]);
		Destroy(NewSound, 1f);
	}

	public void CreateSoundtrack(int index)
	{
		GameObject NewSound = Instantiate(SoundEffects[index]);
	}

}
