using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour {

	public GameObject tempComp, tempDrone;

    PlayerStats Stats;

    public GameObject healthBarFill;
    float fillScale;

	public GameObject energyBarFill;

	// Use this for initialization
	void Start () {
	    Stats = GetComponent<PlayerStats>();
		fillScale = healthBarFill.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		MonitorHealthUI ();
		MonitorEnergyUI ();
		if (Input.GetButtonDown ("Button_X")){
			Vector3 spawnPos = new Vector3(Stats.myTransform.position.x,Stats.myTransform.position.y + 5,Stats.myTransform.position.z);
			Instantiate (tempComp, spawnPos, Quaternion.identity);
		}

		if (Input.GetButtonDown ("Button_B")){
			Vector3 spawnPos = new Vector3(Stats.myTransform.position.x,Stats.myTransform.position.y + 5 ,Stats.myTransform.position.z);
			Instantiate (tempDrone, spawnPos, Quaternion.identity);
		}
	}

    void MonitorHealthUI() {
		float xScale = Mathf.Clamp (fillScale / 100 * Stats.playerHeath, 0, 1);
		Vector3 scale = new Vector3 (xScale, 1, 1);
		healthBarFill.transform.localScale = Vector3.Slerp (healthBarFill.transform.localScale, scale, 5 * Time.deltaTime);
    }

	void MonitorEnergyUI(){
		float xScale = Mathf.Clamp (fillScale / 100 * Stats.playerEnergy, 0, 1);
		Vector3 scale = new Vector3 (xScale, 1, 1);
		energyBarFill.transform.localScale = Vector3.Slerp (energyBarFill.transform.localScale, scale, 5 * Time.deltaTime);
	}

	void TakeHit(int damage){
		Stats.MovementContoller.canRoll = true;
		if (Stats.playerHeath <= damage) {
			Stats.playerHeath -= damage;
			Stats.myAnim.SetTrigger ("death");
			Stats.isAlive = false;
		} else {
			Stats.playerHeath -= damage;
			Stats.myAnim.SetTrigger ("getHit");
		} 
	}
}
