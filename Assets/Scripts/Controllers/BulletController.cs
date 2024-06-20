using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	[Header("Properties")]
	[SerializeField] private Vector3 direction;

	[Header("References")]
	[SerializeField] private GameObject shotPoit;
	[SerializeField] private GameObject scope;
	[SerializeField] private MovementBehaviour _mb;
	

	private void Awake() {

		_mb = GetComponent<MovementBehaviour>();
		shotPoit = GameObject.Find("ShotPoint");
		scope = GameObject.Find("Scope");
	}

	private void FixedUpdate() {

		if (!ScriptingManager.scriptingMode)
			_mb.MoveRb3D(direction);
	}

	public void OnEnable() {

		direction = scope.transform.position - shotPoit.transform.position;
		direction.y = 0;
	}

	public void OnDisable() {

		direction = Vector3.zero;
	}
}
