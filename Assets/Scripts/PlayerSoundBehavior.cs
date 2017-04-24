using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBehavior : MonoBehaviour {

	AudioSource playerImpactAudio;
	public AudioSource playerMoveAudio;
	public AudioSource playerWindAudio;
	Rigidbody2D body;
	public List<AudioClip> impacts;
	public UnityEngine.Audio.AudioMixerGroup master;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		playerImpactAudio = gameObject.AddComponent<AudioSource>();
		playerImpactAudio.outputAudioMixerGroup = master;
	}

	void FixedUpdate()
	{
		float speed = body.velocity.magnitude;
		playerMoveAudio.volume = ScaleClamp(speed, 1f, 30f, 0f, .3f);
		playerMoveAudio.pitch = ScaleClamp(speed, 1f, 30f, 1f, 3f);


		if (playerWindAudio.isPlaying)
		{
			if (speed > 10)
			{
				playerWindAudio.volume = ScaleClamp(speed, 10f, 30f, 0f, .06f);
				playerWindAudio.pitch = ScaleClamp(speed, 10f, 30f, .8f, 1.5f);
			}
			else
				playerWindAudio.Stop();
		}
		else if (speed > 10)
			playerWindAudio.Play();
		
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Planet")
		{
			AudioClip impact = impacts[Random.Range(0, impacts.Count)];
			playerImpactAudio.pitch = Random.Range(0.95f, 1.02f);
			float volume = Mathf.Clamp((col.relativeVelocity.magnitude/25f),0f,1f);
			playerImpactAudio.PlayOneShot(impact, volume);
		}
	}

	float ScaleClamp(float input, float inMin, float inMax, float outMin, float outMax)
	{
		return (((Mathf.Clamp(input, inMin, inMax) - inMin) * (outMax - outMin)) / (inMax - inMin)) + outMin;
	}
}
