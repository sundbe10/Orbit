using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetExplosionBehavior : MonoBehaviour {

	AudioSource sound;
	public List<AudioClip> sounds;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
		sound.clip = sounds[Random.Range(0, sounds.Count)];
		sound.pitch = Random.Range(.85f,.95f);
		sound.volume = Random.Range(.9f,1f);
		sound.Play();
	}
}
