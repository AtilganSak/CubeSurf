using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceKeeper : Singleton<ReferenceKeeper>
{
    public GameManager GameManager;
    public MenuCanvas MenuCanvas;
    public ObstacleController ObstacleController;
    public MaterialMovement MaterialMovement;

    private void OnEnable()
    {
        GameManager = FindObjectOfType<GameManager>();
        MenuCanvas = FindObjectOfType<MenuCanvas>();
        ObstacleController = FindObjectOfType<ObstacleController>();
        MaterialMovement = FindObjectOfType<MaterialMovement>();
    }
}
