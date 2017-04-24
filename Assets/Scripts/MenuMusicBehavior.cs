using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicBehavior : MonoBehaviour {

	AudioSource music;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		music = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!music.isPlaying)
			Destroy(gameObject);
	}
}
