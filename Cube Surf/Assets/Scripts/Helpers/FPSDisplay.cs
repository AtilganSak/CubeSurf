﻿using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
	GUIStyle style = new GUIStyle();
	void OnGUI()
	{
		Rect rect = new Rect(30, 10, Screen.width, Screen.height * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = Screen.height * 2 / 100;
		style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}