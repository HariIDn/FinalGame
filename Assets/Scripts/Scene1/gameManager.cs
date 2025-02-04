using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Obstacle;
    public GameObject Apple;
    public GameObject Pear;
    public GameObject UpDownPlatform;
    public GameObject memeImages;

    public GameObject BridgePlatform;
    public Vector3[] spawnPositionsBridge; // Array untuk posisi spawn manual
    private int spawnedPlatforms = 0; // Indeks platform yang telah muncul

    private Vector3 spawnPositionEnemy = new Vector3(9.5f, -3.455f, 0.0f); // Posisi spawn enemy
    private Vector3 spawnPositionObstacle = new Vector3(0f, 7f, 0f); // Posisi awal obstacle
    private Vector3 spawnPositionApple = new Vector3(-4.4f, -3.366f, 0f); // Posisi awal apel
    private Vector3 spawnPositionPear = new Vector3(-8.06f, 4f, 0f); // Posisi awal pear

    private float obstacleSpeed = 18.0f; // Kecepatan obstacle turun
    public float movementDistance = 2.0f; // Jarak gerakan platform ke atas atau ke bawah
    public float platformSpeed = 2.0f; // Kecepatan gerakan platform

    private bool isMovingPlatform = false; // Menandai apakah platform sedang bergerak
    private bool isGameOver = false;
    private bool allowPlatformSpawn = true; // Defaultnya diaktifkan


    public AudioClip gameOverSound1; // Tambahkan AudioClip untuk Game Over pertama
    public AudioClip gameOverSound2; // Tambahkan AudioClip untuk Game Over kedua
    private AudioSource audioSource; // Komponen AudioSource

    public TextMeshProUGUI livestext;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to the GameManager!");
        }

        livestext.text = ": " + mainManager.Instance.live;

        // Reset flag spawn
        allowPlatformSpawn = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

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
            ReduceLive();
        }

        // Tunggu hingga suara kedua selesai sebelum restart
        if (gameOverSound2 != null && gameOverSound2 != null)
        {
            yield return new WaitForSecondsRealtime(gameOverSound2.length);
        }

        // Restart scene
        SceneManager.LoadScene(1);

        // Kembalikan Time.timeScale ke 1 sebelum memuat ulang scene
        Time.timeScale = 1f;
    }

    public void ReduceLive()
    {
        mainManager.Instance.ReduceLives();
        livestext.text = ": " + mainManager.Instance.live;
    }

    // Fungsi untuk Spawn Enemy
    public void SpawnEnemy()
    {
        Instantiate(Enemy, spawnPositionEnemy, Enemy.transform.rotation);
    }

    // Fungsi untuk Spawn Apple
    public void SpawnApple()
    {
        Instantiate(Apple, spawnPositionApple, Quaternion.identity); // Menambahkan rotasi default
    }

    public void SpawnPear()
    {
        Instantiate(Pear, spawnPositionPear, Quaternion.identity); // Menambahkan rotasi default
    }

    // Fungsi untuk Spawn Enemy dengan Delay
    public IEnumerator SpawnEnemyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Menunggu selama waktu yang ditentukan
        SpawnEnemy();
    }

    // Fungsi untuk Spawn Obstacle
    public void SpawnObstacle()
    {
        if (Obstacle != null)
        {
            GameObject spawnedObstacle = Instantiate(Obstacle, spawnPositionObstacle, Quaternion.identity);
            StartCoroutine(MoveObstacleDown(spawnedObstacle));
        }
    }

    // Coroutine untuk menggerakkan Obstacle ke bawah
    private IEnumerator MoveObstacleDown(GameObject obstacleToMove)
    {
        while (obstacleToMove != null && obstacleToMove.transform.position.y > -10f) // Batas bawah
        {
            obstacleToMove.transform.Translate(Vector3.down * obstacleSpeed * Time.deltaTime);
            yield return null; // Tunggu frame berikutnya
        }

        if (obstacleToMove != null && obstacleToMove.transform.position.y <= -10f)
        {
            Destroy(obstacleToMove); // Hapus obstacle jika melewati batas
        }
    }

    // Fungsi untuk Spawn Enemy dan Platform
    public void SpawnPlatform()
    {
        UpDownPlatform.SetActive(true); // Aktifkan platform

        if (!isMovingPlatform)
        {
            StartCoroutine(MovePlatform());
        }
    }

    // Coroutine untuk menggerakkan platform
    private IEnumerator MovePlatform()
    {
        isMovingPlatform = true; // Menandai platform sedang bergerak

        while (true)
        {

            // Gerakan ke bawah selama 2 detik
            yield return MoveForDuration(Vector3.down, 2.0f);

            // Gerakan ke atas selama 2 detik
            yield return MoveForDuration(Vector3.up, 2.0f);
        }
    }

    // Coroutine untuk menggerakkan platform selama durasi tertentu
    private IEnumerator MoveForDuration(Vector3 direction, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (UpDownPlatform != null)
            {
                UpDownPlatform.transform.position += direction * platformSpeed * Time.deltaTime;
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Tunggu frame berikutnya
        }
    }


    // Fungsi untuk spawn platform baru
    public void SpawnBridgePlatform()
    {
        if (!allowPlatformSpawn) return; // Jika spawn tidak diizinkan, hentikan fungsi

        if (spawnedPlatforms < spawnPositionsBridge.Length) // Cek apakah masih ada posisi untuk spawn
        {
            Vector3 spawnPosition = spawnPositionsBridge[spawnedPlatforms]; // Ambil posisi spawn berdasarkan urutan
            GameObject newPlatform = Instantiate(BridgePlatform, spawnPosition, Quaternion.identity); // Spawn platform
            spawnedPlatforms++; // Increment jumlah platform yang sudah di-spawn

            // Menambahkan event ketika platform dihancurkan untuk memicu spawn berikutnya
            bridgePlatform platformBehavior = newPlatform.GetComponent<bridgePlatform>();
            if (platformBehavior != null)
            {
                platformBehavior.OnPlatformDestroyed += HandlePlatformDestroyed; // Tambahkan event listener
            }
        }
        else
        {
            Debug.Log("No more platforms to spawn.");
        }
    }

    // Fungsi ini akan dipanggil ketika platform dihancurkan
    private void HandlePlatformDestroyed()
    {
        if (!isGameOver) // Cek apakah game belum selesai
        {
            SpawnBridgePlatform(); // Spawn platform berikutnya
        }
    }

    public void MovetoMainMenuGame()
    {
        // Hentikan musik di Main Manager jika ada
        if (mainManager.Instance != null)
        {
            mainManager.Instance.StopBGM(); // Panggil fungsi untuk menghentikan BGM
        }

        // Pindah ke Main Menu (scene 0)
        SceneManager.LoadScene(0);

        // Matikan mekanisme spawn platform
        allowPlatformSpawn = false;

        // Hapus semua BridgePlatform yang tersisa
        ClearAllPlatforms();
    }

    private void ClearAllPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("BridgePlatform");
        foreach (GameObject platform in platforms)
        {
            Destroy(platform);
        }
    }


}
