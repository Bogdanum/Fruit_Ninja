using UnityEngine;
using System.Collections.Generic;

public static class PlayerData
{
    public static int BestScore;

    private const string KeyBestScore = "BestScore";
    
    public static readonly Dictionary<int, float> HealthGridSizes = new Dictionary<int, float>()
    {
        {8, 100},
        {32, 50},
        {128, 25},
        {512, 12.5f},
        {2048, 6.25f}
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Load()
    {
        BestScore = LoadPref(KeyBestScore, 0);
    }
    
    public static void Refresh() => Load();

    public static void SaveBestScore(int value)
    {
        SavePref(KeyBestScore, value);
    }
    
    #region PLAYER PREFS

    private static void SavePref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    private static void SavePref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private static void SavePref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    private static string LoadPref(string key, string default_value)
    {
        return PlayerPrefs.GetString(key, default_value);
    }

    private static int LoadPref(string key, int default_value)
    {
        return PlayerPrefs.GetInt(key, default_value);
    }

    private static float LoadPref(string key, float default_value)
    {
        return PlayerPrefs.GetFloat(key, default_value);
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
