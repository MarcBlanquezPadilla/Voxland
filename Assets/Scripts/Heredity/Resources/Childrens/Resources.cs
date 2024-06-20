using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resources : MonoBehaviour {

    private bool target = false;

    public void Target() {

        EnableEmision();
        target = true;
    }

    public void Untarget() {

        DisableEmision();
        target = false;
    }

    public bool ReturnTarget() {

        return target;
    }

    #region BaseColor

    public void ChangeBaseColorSinceATime() {

        StopAllCoroutines();

        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < mesh.Length; i++) {

            mesh[i].material.color = new Color(1, 0, 0, 0.2f);
        }

        StartCoroutine(C_ReturnToBaseColor(mesh));
    }

    IEnumerator C_ReturnToBaseColor(MeshRenderer[] mesh) {

        int correctMeshes = 0;

        for (int i = 0; i < mesh.Length; i++) {

            mesh[i].material.color = Vector4.MoveTowards(mesh[i].material.color, new Color(1, 1, 1, 0.2f), 0.05f);

            if (mesh[i].material.color == Color.white)
                correctMeshes++;
        }

        yield return new WaitForSeconds(0);

        if (correctMeshes != mesh.Length)
            StartCoroutine(C_ReturnToBaseColor(mesh));
    }

    #endregion

    #region Emision

    public void EnableEmision() {

        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < mesh.Length; i++) {

            mesh[i].material.EnableKeyword("_EMISSION");
        }
    }

    public void DisableEmision() {

        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < mesh.Length; i++) {

            mesh[i].material.DisableKeyword("_EMISSION");
        }
    }

    #endregion
}
