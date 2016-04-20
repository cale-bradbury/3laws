using UnityEngine;
using System.Collections.Generic;

public class EventCreateAudio : ccEventBase {

	public AudioClip clip;
	public float vol = 1;
	public float pitch = 1;
	List<AudioSource> source = new List<AudioSource>();

	protected override void OnEvent ()
	{
		AudioSource s = gameObject.AddComponent<AudioSource> ();
		s.clip = clip;
		s.volume = vol;
		s.pitch = pitch;
		s.Play ();
	}

	void Update(){
		foreach (AudioSource s in source) {
			if(!s.isPlaying){
				source.Remove(s);
				Destroy(s);
			}
		}
	}
}
