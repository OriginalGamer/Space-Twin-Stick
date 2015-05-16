using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class PlayerSetCustomization : MonoBehaviour {

	string bodyColourPath = @"Assets\Scripts\Text Data\Player Customization\bodyColour.txt";
	string gunColourPath = @"Assets\Scripts\Text Data\Player Customization\gunColour.txt";
	string bodyHelmetPath = @"Assets\Scripts\Text Data\Player Customization\bodyHelmet.txt";
	
	public Renderer playerBody;
	public Renderer playerGun;

	public Slider bodyColourChoice;
	public Image body1, body2;
	public Slider gunColourChoice;
	public Image gun1, gun2;
	public Slider helmetChoice;
	public Text helmetName;

	public GameObject armyHelm, tacHelm, vendettaHel, romanHelm;

	// Use this for initialization
	void Start () {
	}

	void Update(){
		ChangeBody ();
		ChangeGun ();
		HelmetChange ();
	}

	void ChangeBody(){
		float lerpSpeed = 2f;
		Color playerColour = playerBody.material.color;
		if (bodyColourChoice.value == 1) {
			Color norm = new Vector4 (0.6f, 0.6f, 0.6f, 1);
			playerColour = Color.Lerp (playerColour, norm, lerpSpeed * Time.deltaTime);
			body1.color = Color.Lerp (body1.color, norm, lerpSpeed * Time.deltaTime);
			body2.color = Color.Lerp (body1.color, norm, lerpSpeed * Time.deltaTime);
		}
		if (bodyColourChoice.value == 2) {
			playerColour = Color.Lerp (playerColour, Color.red, lerpSpeed * Time.deltaTime);
			body1.color = Color.Lerp (body1.color, Color.red, lerpSpeed * Time.deltaTime);
			body2.color = Color.Lerp (body1.color, Color.red, lerpSpeed * Time.deltaTime);
		}
		if (bodyColourChoice.value == 3) {
			playerColour = Color.Lerp (playerColour, Color.green, lerpSpeed * Time.deltaTime);
			body1.color = Color.Lerp (body1.color, Color.green, lerpSpeed * Time.deltaTime);
			body2.color = Color.Lerp (body1.color, Color.green, lerpSpeed * Time.deltaTime);
		}
		if (bodyColourChoice.value == 4) {
			playerColour = Color.Lerp (playerColour, Color.blue, lerpSpeed * Time.deltaTime);
			body1.color = Color.Lerp (body1.color, Color.blue, lerpSpeed * Time.deltaTime);
			body2.color = Color.Lerp (body1.color, Color.blue, lerpSpeed * Time.deltaTime);
		}
		if (bodyColourChoice.value == 5) {
			playerColour = Color.Lerp (playerColour, Color.yellow, lerpSpeed * Time.deltaTime);
			body1.color = Color.Lerp (body1.color, Color.yellow, lerpSpeed * Time.deltaTime);
			body2.color = Color.Lerp (body1.color, Color.yellow, lerpSpeed * Time.deltaTime);
		}
		playerBody.material.color = playerColour;
	}

	void ChangeGun(){
		float lerpSpeed = 2f;
		if (gunColourChoice.value == 1) {
			Color norm = new Vector4 (0.6f, 0.6f, 0.6f, 1);
			playerGun.material.color = Color.Lerp (playerGun.material.color, norm, lerpSpeed * Time.deltaTime);
			gun1.color = Color.Lerp (gun1.color, norm, lerpSpeed * Time.deltaTime);
			gun2.color = Color.Lerp (gun2.color, norm, lerpSpeed * Time.deltaTime);
		}
		if (gunColourChoice.value == 2) {
			playerGun.material.color = Color.Lerp (playerGun.material.color, Color.red, lerpSpeed * Time.deltaTime);
			gun1.color = Color.Lerp (gun1.color, Color.red, lerpSpeed * Time.deltaTime);
			gun2.color = Color.Lerp (gun2.color, Color.red, lerpSpeed * Time.deltaTime);
		}
		if (gunColourChoice.value == 3) {
			playerGun.material.color = Color.Lerp (playerGun.material.color, Color.green, lerpSpeed * Time.deltaTime);
			gun1.color = Color.Lerp (gun1.color, Color.green, lerpSpeed * Time.deltaTime);
			gun2.color = Color.Lerp (gun2.color, Color.green, lerpSpeed * Time.deltaTime);
		}
		if (gunColourChoice.value == 4) {
			playerGun.material.color = Color.Lerp (playerGun.material.color, Color.blue, lerpSpeed * Time.deltaTime);
			gun1.color = Color.Lerp (gun1.color, Color.blue, lerpSpeed * Time.deltaTime);
			gun2.color = Color.Lerp (gun2.color, Color.blue, lerpSpeed * Time.deltaTime);
		}
		if (gunColourChoice.value == 5) {
			playerGun.material.color = Color.Lerp (playerGun.material.color, Color.yellow, lerpSpeed * Time.deltaTime);
			gun1.color = Color.Lerp (gun1.color, Color.yellow, lerpSpeed * Time.deltaTime);
			gun2.color = Color.Lerp (gun2.color, Color.yellow, lerpSpeed * Time.deltaTime);
		}
	}

	void HelmetChange(){
		if (helmetChoice.value == 1) {
			armyHelm.SetActive (false);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (false);
		}
		if (helmetChoice.value == 2) {
			armyHelm.SetActive (true);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (false);
		}
		if (helmetChoice.value == 3) {
			armyHelm.SetActive (false);
			tacHelm.SetActive (true);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (false);
		}
		if (helmetChoice.value == 4) {
			armyHelm.SetActive (false);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (true);
			romanHelm.SetActive (false);
		}
		if (helmetChoice.value == 5) {
			armyHelm.SetActive (false);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (true);
		}
	}

	public void AcceptChanges(){
		AcceptBodyColour ();
		AccectGuncolour ();
		AcceptHelmet ();
		Application.LoadLevel (0);
	}
	void AcceptBodyColour(){
		if (bodyColourChoice.value == 1)
			System.IO.File.WriteAllText(bodyColourPath, "normal");
		if (bodyColourChoice.value == 2)
			System.IO.File.WriteAllText(bodyColourPath, "red");
		if (bodyColourChoice.value == 3)
			System.IO.File.WriteAllText(bodyColourPath, "green");
		if (bodyColourChoice.value == 4)
			System.IO.File.WriteAllText(bodyColourPath, "blue");
		if (bodyColourChoice.value == 5)
			System.IO.File.WriteAllText(bodyColourPath, "yellow");
	}

	void AccectGuncolour(){
		if (gunColourChoice.value == 1)
			System.IO.File.WriteAllText(gunColourPath, "normal");
		if (gunColourChoice.value == 2)
			System.IO.File.WriteAllText(gunColourPath, "red");
		if (gunColourChoice.value == 3)
			System.IO.File.WriteAllText(gunColourPath, "green");
		if (gunColourChoice.value == 4)
			System.IO.File.WriteAllText(gunColourPath, "blue");
		if (gunColourChoice.value == 5)
			System.IO.File.WriteAllText(gunColourPath, "yellow");
	}

	void AcceptHelmet(){
		if (helmetChoice.value == 1)
			System.IO.File.WriteAllText(bodyHelmetPath, "normal");
		if (helmetChoice.value == 2)
			System.IO.File.WriteAllText(bodyHelmetPath, "army");
		if (helmetChoice.value == 3)
			System.IO.File.WriteAllText(bodyHelmetPath, "tactical");
		if (helmetChoice.value == 4)
			System.IO.File.WriteAllText(bodyHelmetPath, "vendetta");
		if (helmetChoice.value == 5)
			System.IO.File.WriteAllText(bodyHelmetPath, "roman");
	}
}