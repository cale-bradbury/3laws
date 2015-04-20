using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public static bool red;
	public static bool green;
	public static bool blue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		red = Input.GetButton ("red");
		green = Input.GetButton ("green");
		blue = Input.GetButton ("blue");
	}
}
