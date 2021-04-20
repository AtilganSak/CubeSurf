using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMovement : MonoBehaviour
{
    public float xSpeed = 0;
    public float maxXSpeed = 0;
    public float ySpeed = 2;
    public float maxYSpeed = 9;

    public float incSpeedAmountPerLevel = 0.875F;

    public bool reverse;

    Material material;
    MeshRenderer meshRenderer;

    Vector2 offset;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(PlayerKeys.PLATFORM_YSPEED))
            ySpeed = PlayerPrefs.GetFloat(PlayerKeys.PLATFORM_YSPEED);
        if (PlayerPrefs.HasKey(PlayerKeys.PLATFORM_XSPEED))
            xSpeed = PlayerPrefs.GetFloat(PlayerKeys.PLATFORM_XSPEED);
    }
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;

        offset = material.GetTextureOffset("_BaseMap");

        //ActionManager.Instance.AddListener(Actions.ChangeDifficulty, ChangedDifficulty);
    }
    private void Update()
    {
        if (ReferenceKeeper.Instance.GameManager.gameStarted)
        {
            if (!reverse)
            {
                offset.y -= Time.deltaTime * ySpeed;
                offset.x -= Time.deltaTime * xSpeed;
            }
            else
            {
                offset.y += Time.deltaTime * ySpeed;
                offset.x += Time.deltaTime * xSpeed;
            }

            material.SetTextureOffset("_BaseMap", offset);
        }
    }
    private void ChangedDifficulty()
    {
        if (xSpeed != 0)
        {
            if (xSpeed + incSpeedAmountPerLevel <= maxXSpeed)
                xSpeed += incSpeedAmountPerLevel;
            else
                xSpeed = maxXSpeed;
            PlayerPrefs.SetFloat(PlayerKeys.PLATFORM_XSPEED, xSpeed);
        }
        if(ySpeed != 0)
        {
            if (ySpeed + incSpeedAmountPerLevel <= maxYSpeed)
                ySpeed += incSpeedAmountPerLevel;
            else
                ySpeed = maxYSpeed;
            PlayerPrefs.SetFloat(PlayerKeys.PLATFORM_YSPEED, ySpeed);
        }

    }
    [Button]
    public void ResetParameters()
    {
        ySpeed = 2;
        maxYSpeed = 9;
        incSpeedAmountPerLevel = 0.875F;
    }
}
