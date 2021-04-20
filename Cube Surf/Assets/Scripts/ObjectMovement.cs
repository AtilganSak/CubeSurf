using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float speed = 10;

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
                c_Transform.Rotate(0, 0, (Input.GetAxisRaw("Mouse X") * -speed * Time.deltaTime), Space.World);
            }
        }
    }
    public void EditedSpeed(string valuie)
    {
        int val = int.Parse(valuie);
        speed = val;
    }
}
