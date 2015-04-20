using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Cale/Kalidescope")]
public class Kalidescope : ImageEffectBase {
	public float angle;
	public float baseAngle;
	public float spinAngle;

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		material.SetFloat ("_Angle", Mathf.PI*2/angle);
		material.SetFloat ("_BaseAngle", Mathf.PI*2*baseAngle);
		material.SetFloat("_SpinAngle",Mathf.PI*2/angle*spinAngle);
		Graphics.Blit (source, destination, material);
	}
}
