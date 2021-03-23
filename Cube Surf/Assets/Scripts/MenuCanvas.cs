using UnityEngine;
using TMPro;

public class MenuCanvas : MonoBehaviour
{
    public GameObject tapHereObject;
    public TMP_Text scoreText;

    public void UpdateScore(int value)
    {
        scoreText.text = value.ToString();
    }
    public void Pressed_TapHereButton()
    {
        tapHereObject.SetActive(false);

        ReferenceKeeper.Instance.GameManager.StartGame();
    }
}
