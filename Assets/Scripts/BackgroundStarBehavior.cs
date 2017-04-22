using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStarBehavior : MonoBehaviour {

	void OnBecameInvisible() {
		//Debug.Log("I'm not being rendered!!!");
		BackgroundStarManager.MoveStar(this);
	}
}
