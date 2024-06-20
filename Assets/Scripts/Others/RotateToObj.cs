using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToObj : MonoBehaviour
{
    [SerializeField] private GameObject obj; 

    void OnEnable()
    {
        Vector3 direction = obj.transform.position - transform.position;
        direction.Normalize();

        Quaternion target = Quaternion.LookRotation(direction);
        transform.rotation = target;
    }
}
