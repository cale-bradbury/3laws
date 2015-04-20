using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/HeatWave")]
public class HeatWave : ImageEffectBase {
	public Texture  depthMap;
	public float    strength = .1f;
	public float	phase = 1;
	public float	freq = 1;
	public int		taps = 3;
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		material.SetTexture("_DepthMap", depthMap);
		material.SetFloat("_Strength", strength);
		material.SetFloat("_Phase",phase);
		material.SetFloat("_Taps",taps);
		material.SetFloat("_Freq",freq);
		Graphics.Blit (source, destination, material);
	}
}
