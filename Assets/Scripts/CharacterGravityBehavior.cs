using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravityBehavior : GravityBehavior {

	// Use this for initialization
	override public void Start () {
		body = null;
		mass = 10f;
		GravityManager.RegisterGravityObject(this, true);
	}

	public Vector2 GetFootPosition() {
		return (Vector2)transform.localPosition - Vector2.up;
	}
}
