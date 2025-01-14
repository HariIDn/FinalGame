using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class mainManager : MonoBehaviour
{

    public int live = 3;
    public int bestScore = 0;

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
        public int bestScorex;   // Menyimpan best score
    }

    public void SaveLivesData()
    {
        PlayerData data = new PlayerData();
        data.livex = live;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadLivesData()
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
    public void StopBGM()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop(); // Hentikan musik
            Debug.Log("BGM stopped.");
        }
    }

    public void PlayBGM()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Putar musik
            Debug.Log("BGM played.");
        }
    }

}
