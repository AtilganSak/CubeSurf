using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Objects")]
    public Transform startPoint;
    public Transform endPoint;
    public Transform obstacleObject;
    public Transform platform;

    [Header("Parameters")]
    public float spawnInterval = 3;
    public float speedOfObstacle = 100;

    List<Transform> spawnedObjects;
    WaitForSeconds waitForSpawn;

    Vector3 randomRot;

    private void OnEnable()
    {
        waitForSpawn = new WaitForSeconds(spawnInterval);
        spawnedObjects = new List<Transform>();
    }
    private void Start()
    {
        StartCoroutine(SpawnMachine());
    }
    private void Update()
    {
        if (ReferenceKeeper.Instance.GameManager.gameStarted)
        {
            if (spawnedObjects.Count > 0)
            {
                for (int i = 0; i < spawnedObjects.Count; i++)
                {
                    spawnedObjects[i].position = Vector3.MoveTowards(spawnedObjects[i].position, endPoint.position, Time.smoothDeltaTime * speedOfObstacle);
                    if ((spawnedObjects[i].position - endPoint.position).sqrMagnitude < 0.1F)
                    {
                        Destroy(spawnedObjects[i].gameObject);
                        spawnedObjects.RemoveAt(i);
                    }
                }
            }
        }
    }
    private IEnumerator SpawnMachine()
    {
        while (true)
        {
            if (ReferenceKeeper.Instance.GameManager.gameStarted)
            {
                Transform ins = Instantiate(obstacleObject, startPoint.position, Quaternion.identity).transform;
                randomRot.z = Random.Range(0, 360);
                ins.transform.eulerAngles = randomRot;
                ins.SetParent(platform);
                spawnedObjects.Add(ins);
            }

            yield return waitForSpawn;
        }
    }
}
