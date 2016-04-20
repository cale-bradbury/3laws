using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BadManager : MonoBehaviour {


	public enum Colors{
		Red,
		Green,
		Blue,
		Cyan,
		Yellow,
		Magenta,
		White
	}
	public Mesh[] models;
	public GameObject[] obj;
	public GameObject[] pattern;
	public List<List<Colors>> colors = new List<List<Colors>>();
	public int[] dificultys;
	public int level = 0;
	public float spawnRate= 4;
	public float startDelay = 2;
	public string endGame;
	public AudioSource song;
	public AudioSource spawn;
	int safe = 0;

	// Use this for initialization
	void Start () {
		AddColorPattern (Colors.Red,Colors.Red,Colors.Red,Colors.Red,Colors.Red);
		AddColorPattern (Colors.Blue,Colors.Blue,Colors.Blue,Colors.Blue,Colors.Blue);
		AddColorPattern (Colors.Green,Colors.Green,Colors.Green,Colors.Green,Colors.Green);

		AddColorPattern (Colors.Red,Colors.Green,Colors.Green,Colors.Green,Colors.Red);
		AddColorPattern (Colors.Green,Colors.Blue,Colors.Green,Colors.Blue,Colors.Green);
		AddColorPattern (Colors.Blue,Colors.Red,Colors.Green,Colors.Red,Colors.Blue);
		
		AddColorPattern (Colors.Cyan,Colors.Cyan,Colors.Red,Colors.Cyan,Colors.Cyan);
		AddColorPattern (Colors.Blue,Colors.Yellow,Colors.Yellow,Colors.Yellow,Colors.Blue);
		AddColorPattern (Colors.Magenta,Colors.Green,Colors.White,Colors.Green,Colors.Magenta);

		AddColorPattern (Colors.White,Colors.Green,Colors.Red,Colors.White,Colors.Cyan);
		AddColorPattern (Colors.Blue,Colors.White,Colors.Blue,Colors.Yellow,Colors.Red);
		AddColorPattern (Colors.Red,Colors.Green,Colors.White,Colors.Green,Colors.Yellow);
	}

	void OnEnable(){
		Invoke ("Spawn", startDelay);
		//Invoke ("Play", startDelay);
		Play ();
		Messenger.AddListener ("dmg", Hit);
	}

	void OnDisable(){
		
		song.Stop ();
		Messenger.RemoveListener("dmg",Hit);
	}


	void Play(){
		song.Play();
	}
	void Hit(){
		safe = 0;
	}

	void AddColorPattern(Colors c1, Colors c2, Colors c3, Colors c4, Colors c5){
		colors.Add (new List<Colors> ());
		int i = colors.Count - 1;
		colors [i].Add (c1);
		colors [i].Add (c2);
		colors [i].Add (c3);
		colors [i].Add (c4);
		colors [i].Add (c5);
	}

	public void EndGame(bool fromGame = false){
		PlayerScript.red = PlayerScript.blue = PlayerScript.green = true;
		foreach (BadScript b in Component.FindObjectsOfType<BadScript> ()) {
			b.StartDestroy();
		}
		CancelInvoke ();
		Component.FindObjectOfType<PlayerScript> ().health = 1;
		if(fromGame)Messenger.Broadcast (endGame);
	}

	// Update is called once per frame
	void Spawn () {
		safe++;
		if (safe == 5) {
			level++;
			safe = 0;
			if(level == dificultys.Length){
				EndGame(true);
				return;
			}
		}
		Invoke ("Spawn", spawnRate);
		spawn.Play ();
		float pat = Random.value * dificultys [level];
		if (level != 0)pat = Random.Range (dificultys [level - 1], dificultys [level]);

		List<Colors> k =colors[Mathf.FloorToInt (pat)];
		Transform p = pattern[Mathf.FloorToInt (Random.value * pattern.Length)].transform;
		for (int i =0; i<p.childCount; i++) {
			int j = Mathf.FloorToInt(Random.value * obj.Length);
			GameObject g = Instantiate<GameObject> (obj [j]);
			Vector3 v = p.GetChild(i).localPosition;
			g.transform.position = new Vector3 (v.x, v.y, -35);
			BadScript b = g.GetComponent<BadScript> ();
			SetColors(b,k[i%k.Count]);
			b.SetTime(-i*.1f);
		}
	}

	void SetColors(BadScript b, Colors c){
		b.blue = b.green = b.red = false;
		if (c == Colors.Blue) {
			b.blue = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[0];
		}else if (c == Colors.Green) {
			b.green = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[1];
		}else if (c == Colors.Red) {
			b.red = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[2];
		}else if (c == Colors.Yellow) {
			b.red = b.green = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[3];
		}else if (c == Colors.Cyan) {
			b.blue = b.green = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[4];
		}else if (c == Colors.Magenta) {
			b.blue = b.red = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[5];
		}else if (c == Colors.White) {
			b.blue = b.red = b.green = true;
			b.gameObject.GetComponent<MeshFilter>().mesh = models[6];
		}
	}
}
