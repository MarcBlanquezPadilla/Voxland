using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour {

	[Header("Properties")]
	[SerializeField] private int damage;

	private void OnTriggerEnter(Collider collision) {

		if (collision.gameObject.TryGetComponent(out HealthBehaviour _hb))
			_hb.Hurt(damage);
	}

	private void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.TryGetComponent(out HealthBehaviour _hb))
			_hb.Hurt(damage);
	}
}
