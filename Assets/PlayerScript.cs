using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public static bool red;
	public static bool green;
	public static bool blue;

	public float energyLossPerSecond = .3f;
	public float energyGainPerSecond = .2f;
	public float energyCooldownRelease= .3f;
	public GameObject energyDisplay;
	public float healthPerHit = .1f;
	public GameObject healthDisplay;
	Material energyMat;
	Material healthMat;
	float energy = 1;
	float health = 1;
	bool canFire;
	Vector2 target = Vector2.zero;
	public GameObject[] targets;
	public float targetLerp = .1f;

	// Use this for initialization
	void Start () {
		energyMat = energyDisplay.GetComponentInChildren<Renderer>().material;
		healthMat = healthDisplay.GetComponentInChildren<Renderer>().material;
		Messenger.AddListener ("dmg", Hit);
	}

	void Hit(){
		health -= healthPerHit;
	}
	
	// Update is called once per frame
	void Update () {
		MoveTargets ();
		red = blue = green = false;
		if (canFire) {
			red = Input.GetButton ("red");
			green = Input.GetButton ("green");
			blue = Input.GetButton ("blue");
			CastRay();

			float loss = energyLossPerSecond * ((red ? 1 : 0) + (blue ? 1 : 0) + (green ? 1 : 0))*Time.deltaTime;
			if(loss!=0)energy-=loss;
			else energy+=energyGainPerSecond*Time.deltaTime;
			energyMat.SetColor("_Color",Color.white);
			if (energy < 0)canFire = false;
		} else {
			energy += energyGainPerSecond*Time.deltaTime;
			if(energy>energyCooldownRelease)canFire = true;
			energyMat.SetColor("_Color",Color.red);
		}
		energy = Mathf.Clamp (energy, 0, 1);
		Vector3 v = energyDisplay.transform.localScale;
		v.x = energy;
		energyDisplay.transform.localScale = v;
		v = healthDisplay.transform.localScale;
		v.x = Mathf.Lerp(v.x,health,.5f);
		healthMat.SetColor ("_Color", new Color (1, v.x, v.x));
		healthDisplay.transform.localScale = v;
	}

	void MoveTargets(){
		target = Vector2.Lerp(target,Input.mousePosition,targetLerp);
		Vector3 v = Camera.main.ScreenToWorldPoint (new Vector3 (target.x,target.y,1));
		bool[] a = new bool[3];
		a [0] = red;
		a [1] = green;
		a [2] = blue;
		for(int i = 0; i<targets.Length;i++){
			targets[i].transform.localPosition = v;
			targets[i].transform.localScale = Vector3.Lerp(targets[i].transform.localScale,a[i]?Vector2.one*.2f:Vector2.one*.1f,.5f);
		}
	}

	void CastRay(){
		RaycastHit info = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay (target);
		Physics.Raycast (ray ,out info, 30);
		if (info.collider == null)
			return;
		BadScript b = info.collider.GetComponent<BadScript> ();
		if (b != null)
			b.Hit ();
	}
}
