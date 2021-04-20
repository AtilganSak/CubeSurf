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
    public void Pressed_30FPS_Button()
    {
        Application.targetFrameRate = 30;
    }
    public void Pressed_45FPS_Button()
    {
        Application.targetFrameRate = 45;
    }
    public void Pressed_60FPS_Button()
    {
        Application.targetFrameRate = 60;
    }
    public void Pressed_DontV()
    {
        QualitySettings.vSyncCount = 0;
    }
    public void Pressed_EveryV()
    {
        QualitySettings.vSyncCount = 1;
    }
    public void Pressed_EveryVSec()
    {
        QualitySettings.vSyncCount = 2;
    }
}
