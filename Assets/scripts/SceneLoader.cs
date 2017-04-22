using UnityEngine;
using System.Collections;

public class SceneLoader : Singleton<SceneLoader> {

	bool allowButtonPresses = true;

	void Start () {

	}

	void Awake() {
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}

	void Update (){

	}

	static public void FadeIn(){
		//Instance.transform.Find("Fade").GetComponent<FadeOut>().StartFadeIn();
	}

	static public void FadeOut(){
		//Instance.transform.Find("Fade").GetComponent<FadeOut>().StartFadeOut();
	}

	static public void GoToScene(string scene){
		Instance.LoadScene(scene);
	}


	//Private 
	void AllowButtonPresses(){
		allowButtonPresses = true;
	}


	void LoadScene(string scene){
		Debug.Log ("New Level load: " + scene);
		Application.LoadLevel (scene);
	}


}