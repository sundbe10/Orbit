using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBehavior : MonoBehaviour {

	AudioSource playerAudio;
	public List<AudioClip> impacts;
	public UnityEngine.Audio.AudioMixerGroup master;

	// Use this for initialization
	void Start () {
		playerAudio = gameObject.AddComponent<AudioSource>();
		playerAudio.outputAudioMixerGroup = master;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Planet")
		{
			AudioClip impact = impacts[Random.Range(0, impacts.Count)];
			playerAudio.pitch = Random.Range(0.95f, 1.02f);
			float volume = Mathf.Clamp((col.relativeVelocity.magnitude/25f),0f,1f);
			playerAudio.PlayOneShot(impact, volume);
		}
	}
}
