using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy {

	private void OnTriggerStay(Collider other) {

		action = Action.Attacking;
	}
}
