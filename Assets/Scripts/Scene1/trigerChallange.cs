using System.Collections;
using UnityEngine;

public class trigerChallange : MonoBehaviour
{
    // Referensi ke objek obstacle pertama dan kedua
    public GameObject pilarObstacle; // Objek kedua
    public GameObject hiddenWall;
    private float moveSpeed2 = 8.0f; // Kecepatan gerakan ke kanan untuk obstacle2
    private float destroyLimitX = 20f;

    public AudioClip spawnSound; // Klip suara saat obstacle di-spawn
    private AudioSource audioSource; // Referensi ke AudioSource


    public GameObject thorn;

    // Referensi ke gameManager untuk memanggil SpawnEnemy
    private gameManager gameMgrScript;

    private void Start()
    {
        // Mendapatkan referensi gameManager secara dinamis
        gameMgrScript = GameObject.Find("Game Manager").GetComponent<gameManager>();

        // Ambil komponen AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource tidak ditemukan. Tambahkan AudioSource ke GameObject ini.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fungsi untuk mendeteksi player yang masuk trigger
    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang masuk adalah player
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi untuk spawn obstacle
            gameMgrScript.SpawnObstacle();

            // Mainkan suara spawn obstacle
            PlaySpawnSound();

            gameMgrScript.SpawnApple();

            // Pastikan obstacle kedua tidak null dan menunggu 2 detik sebelum bergerak
            StartCoroutine(WaitAndMoveObstacleToRight(pilarObstacle, 1.5f)); // Menunggu 2 detik dan bergerak ke kanan

            // Memanggil fungsi SpawnEnemy dari gameManager setelah trigger aktif

            StartCoroutine(gameMgrScript.SpawnEnemyWithDelay(4f)); // Menunggu 2 detik sebelum spawn enemy // Memanggil SpawnEnemy di gameManager

            GetComponent<Collider>().enabled = false; // Menonaktifkan trigger agar hanya sekali

            Invoke(nameof(ActivateThorn), 1f);
            hiddenWall.SetActive(false);
        }
    }

    // Coroutine untuk menunggu 2 detik sebelum memindahkan obstacle kedua ke kanan
    private IEnumerator WaitAndMoveObstacleToRight(GameObject obstacleToMove, float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Menunggu selama waktu yang ditentukan (2 detik)

        // Memindahkan obstacle kedua ke kanan secara perlahan
        while (obstacleToMove != null)
        {
            obstacleToMove.transform.Translate(Vector3.right * moveSpeed2 * Time.deltaTime); // Gerakan ke kanan

            // Periksa jika obstacle2 sudah melewati batas X
            if (obstacleToMove.transform.position.x >= destroyLimitX)
            {
                Destroy(obstacleToMove); // Hancurkan obstacle kedua
                break; // Keluar dari loop
            }


            yield return null; // Tunggu frame berikutnya
        }
    }
    private void ActivateThorn()
    {
        thorn.SetActive(true);
    }

    private void PlaySpawnSound()
    {
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound); // Mainkan suara spawn
        }
    }
}
