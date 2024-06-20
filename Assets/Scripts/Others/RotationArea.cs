using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationArea : MonoBehaviour
{
    [SerializeField] private bool invert;

    public UnityEvent onJoinArea;

    private void OnTriggerStay(Collider other) {

        if (!ScriptingManager.scriptingMode) {

            onJoinArea.Invoke();

            Vector3 direction = other.gameObject.transform.position - transform.position;
            direction.Normalize();

            if (invert)
                direction *= -1;

            Quaternion target = Quaternion.LookRotation(direction);
            target.x = 0;
            target.z = 0;
            transform.parent.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);
        }
    }

    public void Invert() {

        invert = !invert;
    }
}
