using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class SettingsService
{
    private static string settingPath = "GameSettings";

    private static GameSettings gameSettings;

    public static GameSettings GameSettings
    {
        get
        {
            if(gameSettings == null)
            {
                gameSettings = Resources.Load<GameSettings>(settingPath);
            }
            return gameSettings; 
        }
    }
}