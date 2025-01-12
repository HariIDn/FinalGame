using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Obstacle;
    public GameObject Apple;
    public GameObject Pear;
    public GameObject UpDownPlatform;
    

    public GameObject BridgePlatform;
    public Vector3 spawnPositionBridge = new Vector3(-5.68f, 3.59f, 0); // Posisi awal spawn platform
    public int numberOfBridgePlatforms = 5; // Jumlah platform maksimum
    private int currentPlatformIndex = 0; // Indeks platform saat ini


    private Vector3 spawnPositionEnemy = new Vector3(9.5f, -3.455f, 0.0f); // Posisi spawn enemy
    private Vector3 spawnPositionObstacle = new Vector3(0f, 7f, 0f); // Posisi awal obstacle
    private Vector3 spawnPositionApple = new Vector3(-4.4f, -3.366f, 0f); // Posisi awal apel
    private Vector3 spawnPositionPear = new Vector3(-8.06f, 4f, 0f); // Posisi awal pear
    public Vector3 bridgePlatformPosition = new Vector3(0f, 2f, 0f);

    private float obstacleSpeed = 18.0f; // Kecepatan obstacle turun
    public float movementDistance = 2.0f; // Jarak gerakan platform ke atas atau ke bawah
    public float platformSpeed = 2.0f; // Kecepatan gerakan platform

    private bool isMovingPlatform = false; // Menandai apakah platform sedang bergerak

    // Start is called before the first frame update
    void Start()
    {
        currentPlatformIndex = 0; // Reset indeks
    }

    // Update is called once per frame
    void Update()
    {

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
    
    // Fungsi untuk spawn satu bridge platform
    public void SpawnBridgePlatform()
    {
        // Spawn platform di posisi tertentu
        Vector3 spawnPosition = spawnPositionBridge + new Vector3(-5.68f, 3.59f, currentPlatformIndex * 2f);
        GameObject platform = Instantiate(BridgePlatform, spawnPosition, Quaternion.identity);

        // Tambahkan script bridgePlatform untuk mendeteksi saat platform dihancurkan
        platform.GetComponent<bridgePlatform>().OnPlatformDestroyedCallback = OnPlatformDestroyed;

        currentPlatformIndex++; // Increment indeks platform
    }

    public void OnPlatformDestroyed()
    {
        if (currentPlatformIndex < numberOfBridgePlatforms) // Pastikan tidak melebihi batas
        {
            SpawnBridgePlatform(); // Spawn platform berikutnya
        }
    }
}
