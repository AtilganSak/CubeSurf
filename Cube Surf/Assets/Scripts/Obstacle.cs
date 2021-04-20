using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool destroy { get; set; }
    public int listIndex { get; set; }

    public Transform c_Transform { get; private set; }

    private void OnEnable()
    {
        c_Transform = transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //destroy = true;
            ReferenceKeeper.Instance.ObstacleController.DestroyObstacle(listIndex);
            ReferenceKeeper.Instance.GameManager.EndGame();
        }
    }
}
