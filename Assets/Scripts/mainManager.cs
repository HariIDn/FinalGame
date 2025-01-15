using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class mainManager : MonoBehaviour
{
    public int live = 3;
    public int bestScore;

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
        public int livex;          // Jumlah nyawa tertinggi
        public int bestScorex;     // Menyimpan best score
    }

    public void SaveLivesData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        PlayerData data = new PlayerData();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData oldData = JsonUtility.FromJson<PlayerData>(json);

            if (live > oldData.bestScorex)
            {
                data.bestScorex = live;
                Debug.Log("New highest lives saved: " + live);
            }
            else
            {
                data.livex = oldData.bestScorex;
                Debug.Log("Lives did not exceed previous highest: " + oldData.bestScorex);
            }
        }
        else
        {
            data.bestScorex = live;
            Debug.Log("First time saving lives: " + live);
        }

        string newJson = JsonUtility.ToJson(data);
        File.WriteAllText(path, newJson);
    }

    public void LoadLivesData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            bestScore = data.bestScorex;
            Debug.Log("Loaded highest lives: " + live);
        }
        else
        {
            Debug.Log("No save file found. Starting with default lives: " + live);
        }
    }

    public void ReduceLives()
    {
        live -= 1;
    }

    public void StopBGM()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("BGM stopped.");
        }
    }

    public void PlayBGM()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("BGM played.");
        }
    }
}
