using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO; // Pastikan untuk menambahkan namespace ini untuk mengakses File

public class mainManager : MonoBehaviour
{

    public static mainManager Instance;

    public GameObject memeImages;
    public AudioClip gameOverSound1; // Tambahkan AudioClip untuk Game Over pertama
    public AudioClip gameOverSound2; // Tambahkan AudioClip untuk Game Over kedua
    private AudioSource audioSource; // Komponen AudioSource

    private bool isGameOver = false;
    public TextMeshProUGUI livesText;
    private int lives = 3;  // Inisialisasi jumlah nyawa dengan 3


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to the GameManager!");
        }

        // Memuat nilai lives dari PlayerPrefs atau JSON jika ada
        LoadPlayerData();
        UpdateLivesUI(); // Memperbarui tampilan UI
    }

    // Update is called once per frame
    void Update()
    {
        // Bisa menambahkan update lain di sini jika diperlukan
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
        public int lives;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        // Mengurangi lives dan memperbarui UI
        ReduceLives();

        // Mainkan suara Game Over pertama
        if (audioSource != null && gameOverSound1 != null)
        {
            audioSource.PlayOneShot(gameOverSound1);
        }

        // Mainkan suara Game Over kedua setelah 1 detik
        StartCoroutine(PlaySecondGameOverSound());
    }

    private IEnumerator PlaySecondGameOverSound()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (audioSource != null && gameOverSound2 != null)
        {
            audioSource.PlayOneShot(gameOverSound2);
            memeImages.SetActive(true);
        }

        // Tunggu hingga suara kedua selesai sebelum restart
        yield return new WaitForSecondsRealtime(gameOverSound2.length);

        // Restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Kembalikan Time.timeScale ke 1 sebelum memuat ulang scene
        Time.timeScale = 1f;
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = ": " + lives.ToString();  // Memperbarui tampilan nyawa
        }
    }

    private void ReduceLives()
    {
        if (lives == 1)
        {
            lives = -1; // Jika nyawa sudah 1, maka langsung menjadi -1
        }
        else
        {
            lives--; // Mengurangi 1 nyawa
        }

        SavePlayerData();  // Simpan juga ke file JSON
        
    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.lives = lives;

        // Mengubah playerData ke format JSON
        string json = JsonUtility.ToJson(playerData);

        // Menyimpan JSON ke file
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public void LoadPlayerData()
    {
        // Cek apakah file JSON ada
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            lives = playerData.lives; // Memuat data lives dari JSON
        }

        // Memastikan UI diupdate setelah memuat data
        UpdateLivesUI();
    }

}
