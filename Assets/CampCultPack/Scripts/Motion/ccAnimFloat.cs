using UnityEngine;
using System.Collections;
using System.Reflection;

[System.Serializable]
public class ccAnimFloat: MonoBehaviour{

	public AnimationCurve animation = new AnimationCurve();
	public float animationTime = 1;
	public float minValue = 0;
	public float maxValue = 1;
	public string varName;
	string _varName;
	public MonoBehaviour obj;
	MonoBehaviour _obj;
	float time = 0;
	FieldInfo field;
	bool playing = false;

	void OnEnable(){
		time = animationTime;
		SetField ();
	}

	void SetField(){
		if (_varName != varName||_obj!=obj) {
			_varName = varName;
			_obj = obj;
			field = _obj.GetType ().GetField (_varName);
		}
	}

	void Update(){
		if (playing) {
			if (field == null)
				SetField ();
			if (field == null)
				return;
			field.SetValue (_obj, Value ());
		}
	}

	public void Play(){
		playing = true;
		SetField ();
		time = Time.time;
	}

	public float Value(){
		float t = (Time.time - time) / animationTime;
		if(t>1){
			t = 1;
			playing = false;
		}
		return Mathf.Lerp(minValue,maxValue,animation.Evaluate (t));
	}
}