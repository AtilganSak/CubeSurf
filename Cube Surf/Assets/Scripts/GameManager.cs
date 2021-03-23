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
    }
    public void StartGame()
    {
        playerDOMove.DO();

        gameStarted = true;
    }
    public void EndGame()
    {
        gameStarted = false;
    }
    public void TakeScore()
    {
        score += objectScoreAmount;

        ReferenceKeeper.Instance.MenuCanvas.UpdateScore(score);

        PlayerPrefs.SetInt(PlayerKeys.SCORE, score);
    }

    [Button]
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey(PlayerKeys.SCORE);
    }
}
