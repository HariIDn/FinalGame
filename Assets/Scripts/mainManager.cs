using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class mainManager : MonoBehaviour
{

    public int live = 3;

    public static mainManager Instance;

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class PlayerData
    {
        public int livex;
    }

    public void SaveScoreData()
    {
        PlayerData data = new PlayerData();
        data.livex = live;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            live = data.livex;
        }

    }

    public void ReduceLives()
    {
        live -= 1;
    }
}