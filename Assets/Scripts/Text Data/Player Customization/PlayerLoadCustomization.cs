using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerLoadCustomization : MonoBehaviour {

	string bodyColourPath = @"Assets\Scripts\Text Data\Player Customization\bodyColour.txt";
	string gunColourPath = @"Assets\Scripts\Text Data\Player Customization\gunColour.txt";

	public Renderer playerBody;
	public Renderer playerGun;

	// Use this for initialization
	void Start () {
		BodyColourCheck ();
		GunColourCheck ();
		Destroy (this);
	}

	void BodyColourCheck(){
		string type = System.IO.File.ReadAllText (bodyColourPath);
		if (type == "red")
			playerBody.material.color = Color.red;
		if (type == "green")
			playerBody.material.color = Color.green;
		if (type == "blue")
			playerBody.material.color = Color.blue;
		if (type == "normal")
			playerBody.material.color = new Vector4 (0.6f, 0.6f, 0.6f, 1);
	}

	void GunColourCheck(){
		string type = System.IO.File.ReadAllText (gunColourPath);
		if (type == "red")
			playerGun.material.color = Color.red;
		if (type == "green")
			playerGun.material.color = Color.green;
		if (type == "blue")
			playerGun.material.color = Color.blue;
		if (type == "normal")
			playerGun.material.color = new Vector4 (0.6f, 0.6f, 0.6f, 1);
	}
}
