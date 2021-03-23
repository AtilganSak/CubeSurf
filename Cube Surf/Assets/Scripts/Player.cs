using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string obstacleTag = "Obstacle";
    public string scoreObject = "ScoreObject";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(obstacleTag))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
        else if (other.gameObject.CompareTag(scoreObject))
        {
            ReferenceKeeper.Instance.GameManager.TakeScore();
            Destroy(other.gameObject);
        }
    }
}
