using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Equipment {

    public override void Use() {

        if (!GameManager.Instance.playerController.ReturnUsingEquip())
            GameManager.Instance.player.GetComponent<PlayerController>().ChangeAnimation(3);
    }
}
