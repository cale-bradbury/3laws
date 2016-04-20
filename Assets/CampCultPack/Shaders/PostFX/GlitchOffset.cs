using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/HeatWave")]
public class GlitchOffset : ImageEffectBase {
	public float    xStrength = .1f;
	public float	yStrength = 1;
	public float	radialStrength = 1;
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		material.SetVector("_Shape", new Vector4(xStrength,yStrength,radialStrength,0));
		Graphics.Blit (source, destination, material);
	}
}
