using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Objects")]
    public Transform startPoint;
    public Transform endPoint;
    public Transform platform;
    public Explode obstacleExpPrefab;
    //Per index represented one level
    public Obstacle[] obstacleObjects;

    [Header("Interval")]
    public float spawnInterval = 6;
    public float minSpawnInterval = 0.7F;
    public float decreaseIntervalPerLevel = 0.4076F;
    [Header("Speed")]
    public float speedOfObstacle = 50;
    public float maxSpeed = 130;
    public float increaseSpeedPerLevel = 6.1538F;
    [Header("Rotate")]
    public float rotateSpeedOfObstacle = 12;
    public float maxRotateSpeed = 25;
    public float increaseRotateSpeedPerLevel = 4.3F;
    [Header("Difficulty")]
    public int difficultyLevel = 1;
    public int maxDiffLevel = 13;

    List<Obstacle> spawnedObjects;
    WaitForSeconds waitForSpawn;

    Vector3 randomRot;

    private void OnEnable()
    {
        waitForSpawn = new WaitForSeconds(spawnInterval);
        spawnedObjects = new List<Obstacle>();

        if (PlayerPrefs.HasKey(PlayerKeys.OBSTACLE_DIF_LEVEL))
            difficultyLevel = PlayerPrefs.GetInt(PlayerKeys.OBSTACLE_DIF_LEVEL);
        if (PlayerPrefs.HasKey(PlayerKeys.OBSTACLE_DIF_LEVEL))
            spawnInterval = PlayerPrefs.GetFloat(PlayerKeys.OBSTACLE_SPAWN_INTERVAL);
        if (PlayerPrefs.HasKey(PlayerKeys.OBSTACLE_ROTATE_SPEED))
            rotateSpeedOfObstacle = PlayerPrefs.GetFloat(PlayerKeys.OBSTACLE_ROTATE_SPEED);
        if (PlayerPrefs.HasKey(PlayerKeys.OBSTACLE_SPEED))
            speedOfObstacle = PlayerPrefs.GetFloat(PlayerKeys.OBSTACLE_SPEED);

        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            spawnedObjects[i].listIndex = i;
        }

        ActionManager.Instance.AddListener(Actions.GameStart, GameStarted);
        //ActionManager.Instance.AddListener(Actions.ChangeDifficulty, ChangedDifficulty);
    }
    private void Update()
    {
        if (ReferenceKeeper.Instance.GameManager.gameStarted)
        {
            if (spawnedObjects.Count > 0)
            {
                for (int i = 0; i < spawnedObjects.Count; i++)
                {
                    spawnedObjects[i].c_Transform.position = Vector3.MoveTowards(spawnedObjects[i].c_Transform.position, endPoint.position, Time.smoothDeltaTime * speedOfObstacle);
                    //if (difficultyLevel >= 6 && difficultyLevel <= 8)
                    //{
                    //    spawnedObjects[i].Rotate(0, 0, Time.smoothDeltaTime * rotateSpeedOfObstacle);
                    //}
                    if ((spawnedObjects[i].c_Transform.position - endPoint.position).sqrMagnitude < 0.1F)
                    {
                        Destroy(spawnedObjects[i].gameObject);
                        spawnedObjects.RemoveAt(i);
                    }
                    //if (spawnedObjects[i].destroy)
                    //{
                    //    spawnedObjects[i].destroy = false;
                    //    Explode explode = Instantiate(obstacleExpPrefab, spawnedObjects[i].c_Transform.position, spawnedObjects[i].c_Transform.rotation);
                    //    explode.ExplodeObject();
                    //    Destroy(spawnedObjects[i].gameObject);
                    //    spawnedObjects.RemoveAt(i);
                    //}
                }
            }
        }
    }
    private void GameStarted()
    {
        StartCoroutine(SpawnMachine());
    }
    private void ChangedDifficulty()
    {
        if (difficultyLevel + 1 <= maxDiffLevel)
            difficultyLevel++;
        else
            difficultyLevel = maxDiffLevel;
        PlayerPrefs.SetInt(PlayerKeys.OBSTACLE_DIF_LEVEL, difficultyLevel);

        if (spawnInterval >= 1)
            spawnInterval -= decreaseIntervalPerLevel;
        else
            spawnInterval = 1;
        waitForSpawn = new WaitForSeconds(spawnInterval);
        PlayerPrefs.SetFloat(PlayerKeys.OBSTACLE_SPAWN_INTERVAL, spawnInterval);

        if (speedOfObstacle + increaseSpeedPerLevel <= maxSpeed)
            speedOfObstacle += increaseSpeedPerLevel;
        else
            speedOfObstacle = increaseSpeedPerLevel;
        PlayerPrefs.SetFloat(PlayerKeys.OBSTACLE_SPEED, speedOfObstacle);

        if (difficultyLevel >= 9 && difficultyLevel <= 11)
        {
            if (rotateSpeedOfObstacle + increaseRotateSpeedPerLevel <= maxRotateSpeed)
                rotateSpeedOfObstacle += increaseRotateSpeedPerLevel;
            else
                rotateSpeedOfObstacle = maxRotateSpeed;

            PlayerPrefs.SetFloat(PlayerKeys.OBSTACLE_ROTATE_SPEED, rotateSpeedOfObstacle);
        }
    }
    private void SpawnObstacle()
    {
        if (ReferenceKeeper.Instance.GameManager.gameStarted)
        {
            Obstacle ins = Instantiate(obstacleObjects[0], startPoint.position, Quaternion.identity);
            //if (difficultyLevel < 3)
            //    ins = Instantiate(obstacleObjects[0], startPoint.position, Quaternion.identity).transform;
            //else if (difficultyLevel >= 3 && difficultyLevel < 6)
            //    ins = Instantiate(obstacleObjects[1], startPoint.position, Quaternion.identity).transform;
            //else if (difficultyLevel >= 6)
            //    ins = Instantiate(obstacleObjects[2], startPoint.position, Quaternion.identity).transform;

            randomRot.z = Random.Range(0, 360);
            ins.c_Transform.eulerAngles = randomRot;
            ins.c_Transform.SetParent(platform);
            spawnedObjects.Add(ins);
            ins.listIndex = spawnedObjects.Count - 1;
        }
    }
    private IEnumerator SpawnMachine()
    {
        while (true)
        {
            SpawnObstacle();

            yield return waitForSpawn;
        }
    }
    public void DestroyObstacle(int index)
    {
        Explode explode = Instantiate(obstacleExpPrefab, spawnedObjects[index].c_Transform.position, spawnedObjects[index].c_Transform.rotation);
        explode.OnStart();
        explode.ExplodeObject();
        Destroy(spawnedObjects[index].gameObject);
        spawnedObjects.RemoveAt(index);
    }
    public void ResetSystem()
    {
        StopCoroutine(SpawnMachine());

        if (spawnedObjects != null && spawnedObjects.Count > 0)
        {
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                if (!spawnedObjects[i].destroy)
                {
                    Destroy(spawnedObjects[i].gameObject);
                }
            }
            spawnedObjects.Clear();
        }

        //ResetParameters();
    }
    [Button]
    private void ResetParameters()
    {
        spawnInterval = 6;
        minSpawnInterval = 0.7F;
        decreaseIntervalPerLevel = 0.4076F;
        speedOfObstacle = 50;
        maxSpeed = 130;
        increaseSpeedPerLevel = 6.1538F;
        rotateSpeedOfObstacle = 12;
        maxRotateSpeed = 25;
        increaseRotateSpeedPerLevel = 4.3F;
        difficultyLevel = 1;
        maxDiffLevel = 13;
    }
}
