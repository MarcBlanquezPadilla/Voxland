using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : Enemy {

	private void OnTriggerStay(Collider other) {

		action = Action.Fleeing;
	}
}
