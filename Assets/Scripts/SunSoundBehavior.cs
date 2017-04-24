using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSoundBehavior : MonoBehaviour {

	AudioSource sunSound;
	AudioLowPassFilter lowPass;
	AudioHighPassFilter highPass;
	AudioListener listener;
	float maxDistance;
	public AudioClip collapsing;
	public bool isCollapsing;
	[HideInInspector] public float size;

	// Use this for initialization
	void Start () {
		sunSound = GetComponent<AudioSource>();
		lowPass = GetComponent<AudioLowPassFilter>();
		highPass = GetComponent<AudioHighPassFilter>();
		listener = GameObject.FindObjectOfType<AudioListener>();
		maxDistance = sunSound.maxDistance;
		lowPass.cutoffFrequency = 1100f;
		highPass.cutoffFrequency = 400f;
		isCollapsing = false;
		sunSound.spatialBlend = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (sunSound.isVirtual)
			return;

		float soundDistance = (listener.transform.position - transform.position).magnitude;
		if (soundDistance < maxDistance)
		{
			lowPass.cutoffFrequency = ScaleClamp(soundDistance, 5f, maxDistance-5f, 22000f, 1100f);
			highPass.cutoffFrequency = ScaleClamp(soundDistance, 6f, maxDistance-5f, 80f, 400f);
			sunSound.spatialBlend = ScaleClamp(soundDistance, 5f, 10f, .5f, 1f);
			sunSound.pitch = ScaleClamp(size,4,8,1.2f,.9f);
		}
	}

	float ScaleClamp(float input, float inMin, float inMax, float outMin, float outMax)
	{
		return (((Mathf.Clamp(input, inMin, inMax) - inMin) * (outMax - outMin)) / (inMax - inMin)) + outMin;
	}

	public void Collapse() {
		sunSound.PlayOneShot(collapsing, .9f);
		isCollapsing = true;
	}
}
