using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicatorController : MonoBehaviour {

    public void EnableIndicator() {

        PlayerController _pc = GameManager.Instance.playerController;

        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(_pc.hitPosition.x, _pc.hitPosition.y + 0.5f, _pc.hitPosition.z);
    }

    public void DisableIndicator() {

        gameObject.SetActive(false);
    }
}
