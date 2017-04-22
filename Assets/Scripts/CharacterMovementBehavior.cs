using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementBehavior : MonoBehaviour {

	float acceleration = 10f;
	float maxjumpForce = 5000f;
	float jumpForce;
	float angVelocity;
	Vector2 gravVelocity;
	float maxSpeed = 2f;
	public bool isGrounded;
	public GameObject attachedPlanet;
	float characterGravityConstant = 10E-4f;

	// Use this for initialization
	void Awake () {
		angVelocity = 0;
		gravVelocity = Vector2.zero;
		isGrounded = false;
		jumpForce = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		// Walking
		if (Input.GetAxis("Horizontal") < 0 && angVelocity > -maxSpeed)
		{
			angVelocity -= acceleration*Time.deltaTime;
		}
		else if (Input.GetAxis("Horizontal") > 0 && angVelocity < maxSpeed)
		{
			angVelocity += acceleration*Time.deltaTime;
		}
		else if (angVelocity != 0)
			angVelocity = Mathf.Lerp(angVelocity, 0f, acceleration*Time.deltaTime);
		else
			angVelocity = 0f;

		float radius = Vector2.Distance((Vector2)transform.localPosition, attachedPlanet.transform.position);
		transform.RotateAround(attachedPlanet.transform.position, Vector3.forward, -angVelocity/radius);

		// Auto-turning
		if (attachedPlanet != null)
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward,(transform.position - attachedPlanet.transform.position)), 5f);

		// Jumping
		if (Input.GetButton("Jump") && jumpForce > 0f)
		{
			Vector2 jumpVec = (transform.position - attachedPlanet.transform.position) * jumpForce;
			Fall(jumpVec);
			jumpForce = Mathf.Lerp(jumpForce,0,50f*Time.deltaTime);
		}

		Debug.Log(gravVelocity);
	}

	public void Fall(Vector2 force)
	{
		gravVelocity += force*characterGravityConstant;
		Vector3 newPos = (Vector2)transform.position + gravVelocity*Time.deltaTime;
		transform.position = newPos;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "planet")
		{
			Debug.Log("Hit!!!!");
			gravVelocity = Vector2.zero;
			isGrounded = true;
			jumpForce = maxjumpForce;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (isGrounded && col.gameObject.tag == "planet")
			isGrounded = false;
	}
}
