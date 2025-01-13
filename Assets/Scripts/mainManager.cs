using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class mainManager : MonoBehaviour
{

    public int live;

    public static mainManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
