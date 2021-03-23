using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceKeeper : Singleton<ReferenceKeeper>
{
    public GameManager GameManager;
    public MenuCanvas MenuCanvas;

    private void OnEnable()
    {
        GameManager = FindObjectOfType<GameManager>();
        MenuCanvas = FindObjectOfType<MenuCanvas>();
    }
}
