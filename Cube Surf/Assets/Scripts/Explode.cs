using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float explosionForce = 5;
    public float radius = 10;

    Rigidbody[] rigidbodies;

    Transform c_Transform;

    public void OnStart()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        c_Transform = transform;
    }
    [Button]
    public void ExplodeObject()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].AddForce((rigidbodies[i].position - c_Transform.position) * explosionForce);
            Destroy(rigidbodies[i].gameObject, 1.5F);
        }
        Destroy(gameObject, 1.5F);
    }
}
