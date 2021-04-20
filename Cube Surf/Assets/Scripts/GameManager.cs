using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameStarted { get; private set; }

    public Player player;

    public int score;
    public int objectScoreAmount = 1;

    DOMove playerDOMove;

    private void OnEnable()
    {
        playerDOMove = player.GetComponent<DOMove>();

        if (PlayerPrefs.HasKey(PlayerKeys.SCORE))
            score = PlayerPrefs.GetInt(PlayerKeys.SCORE);

        ReferenceKeeper.Instance.MenuCanvas.UpdateScore(score);

        Application.targetFrameRate = 60;
    }
    public void StartGame()
    {
        //Bring player to up for appear
        playerDOMove.DO();

        gameStarted = true;

        ActionManager.Instance.Fire(Actions.GameStart);
    }
    public void EndGame()
    {
        gameStarted = false;

        score = 0;
        ReferenceKeeper.Instance.MenuCanvas.UpdateScore(score);

        //ReferenceKeeper.Instance.MaterialMovement.ResetParameters();
        ReferenceKeeper.Instance.ObstacleController.ResetSystem();
        ReferenceKeeper.Instance.MenuCanvas.tapHereObject.SetActive(true);

        playerDOMove.DORevert();

        ResetAllDatas();

        ActionManager.Instance.Fire(Actions.GameEnd);
    }
    public void TakeScore()
    {
        score += objectScoreAmount;

        ReferenceKeeper.Instance.MenuCanvas.UpdateScore(score);

        PlayerPrefs.SetInt(PlayerKeys.SCORE, score);

        //CheckOutScore();
    }
    private void CheckOutScore()
    {
        //                          4 Time                                                          3 Time                              2 Time                   1 Time
        //|----------------------------------------------------------------------||-------------------------------------------||----------------------------||-------------||...
        //                                                               +2                                            +4                          +6              +8            +10
        //             +2            +2            +2             +2             +4             +4             +4             +8             +8            +14            +22            +33
        if (score == 5 || score == 7 || score == 9 || score == 11 || score == 13 || score == 17 || score == 21 || score == 25 || score == 33 || score == 41 || score == 55 || score == 77 || score == 110)
        {
            ActionManager.Instance.Fire(Actions.ChangeDifficulty);
        }
    }

    [Button]
    public void ResetAllDatas()
    {
        PlayerPrefs.DeleteKey(PlayerKeys.PLATFORM_YSPEED);
        PlayerPrefs.DeleteKey(PlayerKeys.PLATFORM_XSPEED);
        PlayerPrefs.DeleteKey(PlayerKeys.SCORE);
        PlayerPrefs.DeleteKey(PlayerKeys.OBSTACLE_SPAWN_INTERVAL);
        PlayerPrefs.DeleteKey(PlayerKeys.OBSTACLE_DIF_LEVEL);
        PlayerPrefs.DeleteKey(PlayerKeys.OBSTACLE_ROTATE_SPEED);
        PlayerPrefs.DeleteKey(PlayerKeys.OBSTACLE_SPEED);
    }
}
