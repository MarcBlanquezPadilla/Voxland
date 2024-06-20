using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    [Header("On Collision")]
    [SerializeField] private bool destroyOnCollision = false;
    [SerializeField] private bool deactivateOnCollision = false;

    [Header("On Became Invisible")]
    [SerializeField] private bool destroyOnBecameInvisible = false;
    [SerializeField] private bool deactivateOnBecameInvisible = false;

    [Header("References")]
    private DestroyDeactivateBehavoiur _ddb;

    void Awake() {

        _ddb = GetComponentInParent<DestroyDeactivateBehavoiur>();
    }

    private void OnTriggerEnter(Collider other) {

        if (destroyOnCollision) _ddb.DestroyGameObject();
        else if (deactivateOnCollision) _ddb.DeactivateGameObject();
    }

    private void OnCollisionEnter(Collision collision) {

        if (destroyOnCollision) _ddb.DestroyGameObject();
        else if (deactivateOnCollision) _ddb.DeactivateGameObject();
    }

    private void OnBecameInvisible() {

        if (destroyOnBecameInvisible) _ddb.DestroyGameObject();
        else if (deactivateOnBecameInvisible) _ddb.DeactivateGameObject();
    }
}
