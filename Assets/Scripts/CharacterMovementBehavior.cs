using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementBehavior : MonoBehaviour {

	float acceleration = .1f;
	float angVelocity;
	Vector2 gravVelocity;
	float friction = 5f;
	float maxSpeed = 10f;
	bool isGrounded;
	public GameObject attachedPlanet;

	// Use this for initialization
	void Awake () {
		angVelocity = 0;
		gravVelocity = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") < 0)
		{
			//Mathf.SmoothDamp
			//transform.RotateAround(attachedPlanet.transform.position, Vector3.forward, angVelocity);
		}
		else if (Input.GetAxis("Horizontal") > 0)
		{
			//angVelocity -= Mathf.Lerp(angVelocity, -maxSpeed, acceleration*Time.deltaTime);
			//transform.RotateAround(attachedPlanet.transform.position, Vector3.forward, angVelocity);
		}
		else
			angVelocity += Mathf.Lerp(angVelocity, 0, acceleration*Time.deltaTime);

		if (attachedPlanet != null)
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward,(transform.position - attachedPlanet.transform.position)), 5f);
		
	}

	public void Fall(Vector2 force)
	{
		gravVelocity += force;
		Vector3 newPos = (Vector2)transform.position + gravVelocity*Time.deltaTime;
		transform.position = newPos;
	}
}
