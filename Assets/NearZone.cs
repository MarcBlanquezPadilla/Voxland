using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NearZone : MonoBehaviour
{
    public UnityEvent onJoin;

    private void OnTriggerEnter(Collider other)
    {
        onJoin.Invoke();
    }
}
