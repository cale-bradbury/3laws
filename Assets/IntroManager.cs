using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroManager : MonoBehaviour {
	public List<TextMesh> texts;
	List<string> text = new List<string>();
	TextMesh mesh;
	string fillText;
	public bool canPress = false;

	bool destroying  = false;
	bool filling = false;
	int index = 0;
	public string endEvent;
	public TextMesh winText;
	public string winEvent;
	public TextMesh loseText;
	public string loseEvent;

	// Use this for initialization
	void Start () {
		int i = 0;
		Cursor.visible = false;
		foreach (TextMesh t in texts) {
			text.Add(t.text);
			if(i!=0)t.gameObject.SetActive(false);
			i++;
		}
		winText.gameObject.SetActive (false);
		loseText.gameObject.SetActive (false);
		Messenger.AddListener (winEvent, OnWin);
		Messenger.AddListener (loseEvent, OnLose);

		mesh = GetComponent<TextMesh> ();
		mesh.text = " \n  ";
		index = -1;
		Next ();
	}
	
	void OnWin(){
		ShowText (winText.text);
		index = -1;
	}
	void OnLose(){
		ShowText (loseText.text);
		index = -1;
	}

	void ShowText(string text){
		fillText = text;
		destroying = false;
		filling = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (canPress && Input.GetButtonDown ("next"))
			Next ();
		if (destroying) {
			mesh.text = mesh.text.Substring (0, Mathf.Max (0,mesh.text.LastIndexOf("\n")));
			if (mesh.text.Length == 0) {
				Fill ();
			}
		}else if (filling) {
			mesh.text = fillText.Substring(0,mesh.text.Length+1);
			if(mesh.text.Length==fillText.Length){
				EndFill();
			}
		}
	}

	void EndFill(){
		filling = false;
		canPress = true;
	}

	void Next(){
		index++;
		canPress = false;
		destroying = true;
	}

	void Fill(){
		if (index >= text.Count) {
			Messenger.Broadcast(endEvent);
			filling = false;
			destroying = false;
			return;
		}
		ShowText(text [index]);
	}
}
