using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

	public Color32 successColor;
	public Color32 failureColor;

	Text score;
	RectTransform healthbar;
	GameObject healthBarObject;
	Image healthbarImage;
	RectTransform airSupply;
	GameObject airSupplyObject;
	public CharacterMovementBehavior character;


	void Awake(){
		GameManager.OnGameStateChange += GameStateChange;
	}

	// Use this for initialization
	void Start () {
		score = transform.Find("Score").GetComponent<Text>();
		healthBarObject = transform.Find("HealthContainer/HealthBar").gameObject;
		healthbar = healthBarObject.GetComponent<RectTransform>();
		healthbarImage = healthBarObject.GetComponent<Image>();
		//airSupplyObject = transform.Find("AirContainer/AirBar").gameObject;
		//airSupply = airSupplyObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		score.text = GameManager.years + " Million Years";
		healthbar.localPosition = new Vector2(-500+500*GameManager.health/GameManager.maxHealth, 0);
		healthbarImage.color = Color32.Lerp(failureColor, successColor, GameManager.health/GameManager.maxHealth);
	}	

	void GameStateChange(GameManager.State state){
		
	}
}
