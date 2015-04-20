using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class BadScript : MonoBehaviour {
	
	public bool red;
	public bool green;
	public bool blue;
	Material mat;
	public float kill = 0;
	bool destroying = false;
	public bool dirty = true;
	
	void Start () {
		mat = GetComponent<Renderer> ().material;
		UpdateColor ();
	}

	void Update(){
		if (destroying) {
			UpdateDestroy ();
		} else if (dirty) {
			UpdateColor();
		}
	}

	void OnMouseOver () {
		if (PlayerScript.red)RemoveRed ();
		if (PlayerScript.green)RemoveGreen ();
		if (PlayerScript.blue)RemoveBlue ();
	}

	void RemoveRed(){
		if (!red)return;
		red = false;
		dirty = true;
	}
	void RemoveGreen(){
		if (!green)return;
		green = false;
		dirty = true;
	}
	void RemoveBlue(){
		if (!blue)return;
		blue = false;
		dirty = true;
	}

	void UpdateColor(){
		if (destroying)
			return;
		Color c = new Color (0, 0, 0,.5f);
		if (red)c.r = 1;
		if (green)c.g = 1;
		if (blue)c.b = 1;
		if (!red && !green && !blue) {
			destroying = true;
			StartDestroy ();
		} else {
			mat.SetColor ("_FaceColor", c);
		}
	}

	void StartDestroy(){
		destroying = true;

		HOTween.To (this, 2, new TweenParms ().Prop ("kill",1));
	}

	void UpdateDestroy(){
		mat.SetFloat ("_Kill", kill);
	}
}
