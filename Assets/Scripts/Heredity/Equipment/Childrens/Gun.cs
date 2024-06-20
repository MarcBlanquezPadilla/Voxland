using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Equipment {

    private Transform shotPoint;
    private PlayerController _pc;

    private void Start() {

        _pc = GameManager.Instance.player.GetComponent<PlayerController>();
        shotPoint = gameObject.transform.Find("ShotPoint").transform;
    }

    public override void Use() {

        _pc.ChangeAnimation(2);        
    }

    public void Shoot() {

        StartCoroutine(ShootDelay()); 
    }

    IEnumerator ShootDelay() {

        yield return new WaitForSeconds(GameManager.Instance.playerController.ReturnPlayerTransitionTime());

        GameObject shoot = PoolingManager.Instance.GetPooledObject("Bullet");

        if (shoot != null)
        {
            shoot.transform.position = shotPoint.position;
            shoot.transform.parent = null;
            shoot.SetActive(true);
        }
    }
}
