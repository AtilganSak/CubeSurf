using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float speed = 5;

    Transform c_Transform;

    private void Start()
    {
        c_Transform = transform;
    }
    private void Update()
    {
        if (ReferenceKeeper.Instance.GameManager.gameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                c_Transform.Rotate(0, 0, (Input.GetAxisRaw("Mouse X") * -speed), Space.World);
            }
        }
    }
}
