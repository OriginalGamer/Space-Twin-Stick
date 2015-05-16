using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerLoadCustomization : MonoBehaviour {

	string bodyColourPath = @"Assets\Scripts\Text Data\Player Customization\bodyColour.txt";
	string gunColourPath = @"Assets\Scripts\Text Data\Player Customization\gunColour.txt";
	string bodyHelmet = @"Assets\Scripts\Text Data\Player Customization\bodyHelmet.txt";

	public Renderer playerBody;
	public Renderer playerGun;

	public GameObject armyHelm, tacHelm, vendettaHel, romanHelm;

	// Use this for initialization
	void Start () {
		BodyColourCheck ();
		GunColourCheck ();
		HelmetCheck ();
		Destroy (this);
	}

	void BodyColourCheck(){
		Color playerMaterial = playerBody.material.color;
		string type = System.IO.File.ReadAllText (bodyColourPath);
		if (type == "normal")
			playerMaterial = new Vector4 (0.6f, 0.6f, 0.6f, 1);
		if (type == "red")
			playerMaterial = Color.red;
		if (type == "green")
			playerMaterial = Color.green;
		if (type == "blue")
			playerMaterial = Color.blue;
		if (type == "yellow")
			playerMaterial = Color.yellow;

		playerBody.material.color = playerMaterial;
	}

	void GunColourCheck(){
		Color gunMaterial = playerGun.material.color;
		string type = System.IO.File.ReadAllText (gunColourPath);
		if (type == "red")
			gunMaterial = Color.red;
		if (type == "green")
			gunMaterial = Color.green;
		if (type == "blue")
			gunMaterial = Color.blue;
		if (type == "normal")
			gunMaterial = new Vector4 (0.6f, 0.6f, 0.6f, 1);
		if (type == "yellow")
			gunMaterial = Color.yellow;

		playerGun.material.color = gunMaterial;
	}

	void HelmetCheck(){
		string type = System.IO.File.ReadAllText (bodyHelmet);
		if (type == "normal") {
			armyHelm.SetActive (false);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (false);
		}
		if (type == "army") {
			print ("Hello");
			armyHelm.SetActive (true);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (false);
		}
		if (type == "tactical") {
			armyHelm.SetActive (false);
			tacHelm.SetActive (true);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (false);
		}
		if (type == "vendetta") {
			armyHelm.SetActive (false);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (true);
			romanHelm.SetActive (false);
		}
		if (type == "roman") {
			armyHelm.SetActive (false);
			tacHelm.SetActive (false);
			vendettaHel.SetActive (false);
			romanHelm.SetActive (true);
		}
	}
}
