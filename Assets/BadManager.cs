using UnityEngine;
using System.Collections;

public class BadManager : MonoBehaviour {

	public GameObject[] obj;
	public GameObject[] pattern;

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	// Update is called once per frame
	void Spawn () {
		Invoke ("Spawn", 5);
		Transform p = pattern[Mathf.FloorToInt (Random.value * pattern.Length)].transform;
		bool red = Random.value < .5f;
		bool green = Random.value < .5f;
		bool blue = Random.value < .5f;
		if (!red && !green && !blue) {
			red = green= blue = true;
		}
		for (int i =0; i<p.childCount; i++) {
			int j = Mathf.FloorToInt(Random.value * obj.Length);
			GameObject g = Instantiate<GameObject> (obj [j]);
			Vector3 v = p.GetChild(i).localPosition;
			g.transform.position = new Vector3 (v.x, v.y, -35);
			BadScript b = g.GetComponent<BadScript> ();
			b.red = red;
			b.green = green;
			b.blue = blue;
			b.SetTime(-i*.1f);
		}
	}
}
