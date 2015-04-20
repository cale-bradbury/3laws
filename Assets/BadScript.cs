using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class BadScript : MonoBehaviour {
	
	public AnimationCurve depth;
	public float maxDepth = -35;
	public float animationTime;
	float time = 0;
	public bool red;
	public bool green;
	public bool blue;
	Material mat;
	public float kill = 0;
	bool destroying = false;
	bool dirty = true;
	bool isOver = false;
	
	void Start () {
		mat = GetComponent<Renderer> ().material;
		UpdateColor ();
	}

	public void SetTime(float f){
		time = f;
	}

	void Update(){
		if (destroying) {
			UpdateDestroy ();
		} else {
			time+=Time.deltaTime;
			Vector3 v = transform.position;
			v.z = depth.Evaluate(time/animationTime)*maxDepth;
			transform.position = v;
			if(v.z<0){
				Destroy(gameObject);
				Messenger.Broadcast("dmg");
			}
		}
	}

	public void Hit () {
		if (destroying)
			return;
		if (PlayerScript.red)
			RemoveRed ();
		if (PlayerScript.green)
			RemoveGreen ();
		if (PlayerScript.blue)
			RemoveBlue ();
		if (dirty)
			UpdateColor ();
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
		if(kill==1)Destroy(gameObject);
		mat.SetFloat ("_Kill", kill);
	}
}
