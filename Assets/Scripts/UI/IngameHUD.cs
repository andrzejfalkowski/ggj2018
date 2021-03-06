﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject Result;
    [SerializeField]
    private Text Message;

    public void OnRestartPressed()
    {
        SceneManager.LoadScene("Test");
    }

    public void Show(float elapsedTime)
    {
        Message.text = "You survived " + elapsedTime.ToString("N1") + " seconds!";
        Result.SetActive(true);
    }
}