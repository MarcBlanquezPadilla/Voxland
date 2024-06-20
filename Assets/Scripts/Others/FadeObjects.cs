using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjects : MonoBehaviour {

	public Material defaultMaterial;
	public Material fadeMaterial;

	#region RenderMode

	public void Fade() {

		MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();

		for (int i = 0; i < mesh.Length; i++) {

			mesh[i].material = fadeMaterial;
		}

		StopAllCoroutines();
		StartCoroutine(C_ReturnToOpaqueMaterial(mesh));
	}

	IEnumerator C_ReturnToOpaqueMaterial(MeshRenderer[] mesh) {

		yield return new WaitForSeconds(0.2f);

		for (int i = 0; i < mesh.Length; i++) {

			mesh[i].material = defaultMaterial;
		}
	}

	public void ToOpaqueMode(Material material) {

		material.SetOverrideTag("RenderType", "");
		material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		material.SetInt("_ZWrite", 1);
		material.DisableKeyword("_ALPHATEST_ON");
		material.DisableKeyword("_ALPHABLEND_ON");
		material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		material.renderQueue = -1;
		material.color = new Color(1,1,1,1);
	}

	public void ToFadeMode(Material material) {


		material.SetOverrideTag("RenderType", "Transparent");
		material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		material.SetInt("_ZWrite", 0);
		material.DisableKeyword("_ALPHATEST_ON");
		material.EnableKeyword("_ALPHABLEND_ON");
		material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
		material.color = new Color(1, 1, 1, 0.2f);
	}

	#endregion

}
