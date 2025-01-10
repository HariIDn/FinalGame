using System.Collections;
using UnityEngine;

public class trigerChallange : MonoBehaviour
{
    // Referensi ke objek obstacle pertama dan kedua
    public GameObject obstacle;
    public GameObject obstacle2;
    
    private float speed = 18.0f; // Kecepatan turun
    private float moveSpeed2 = 8.0f; // Kecepatan gerakan ke kanan untuk obstacle2
    private float destroyLimitX = 20f;

    // Referensi ke gameManager untuk memanggil SpawnEnemy
    public gameManager gameMgrScript;

    private void Start()
    {
        // Mendapatkan referensi gameManager secara dinamis
        gameMgrScript = GameObject.Find("Game Manager").GetComponent<gameManager>();
    }


    // Fungsi untuk mendeteksi player yang masuk trigger
    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang masuk adalah player
        if (other.CompareTag("Player"))
        {
            // Pastikan obstacle pertama tidak null
            if (obstacle != null)
            {
                // Memulai Coroutine untuk menurunkan obstacle pertama
                StartCoroutine(Bomb(obstacle));
            }

            // Pastikan obstacle kedua tidak null dan menunggu 2 detik sebelum bergerak
            if (obstacle2 != null)
            {
                StartCoroutine(PillarObs(obstacle2, 1.5f)); // Menunggu 2 detik dan bergerak ke kanan
            }

            // Memanggil fungsi SpawnEnemy dari gameManager setelah trigger aktif
            if (gameMgrScript != null)
            {
                StartCoroutine(gameMgrScript.SpawnEnemyWithDelay(7f)); // Menunggu 2 detik sebelum spawn enemy // Memanggil SpawnEnemy di gameManager
            }

            GetComponent<Collider>().enabled = false; // Menonaktifkan trigger agar hanya sekali
        }
    }

    // Coroutine untuk memindahkan obstacle secara bertahap (ke bawah)
    private IEnumerator Bomb(GameObject obstacleToMove)
    {
        while (obstacleToMove != null && obstacleToMove.transform.position.y > -10f) // Batas bawah
        {
            obstacleToMove.transform.Translate(Vector3.down * speed * Time.deltaTime); // Gerakan perlahan ke bawah
            yield return null; // Tunggu frame berikutnya
        }

        // Hancurkan obstacle pertama jika mencapai batas bawah
        if (obstacleToMove != null && obstacleToMove.transform.position.y <= -10f)
        {
            Destroy(obstacleToMove); // Menghapus objek dari scene
        }
    }

    // Coroutine untuk menunggu sebelum memindahkan obstacle kedua ke kanan
    private IEnumerator PillarObs(GameObject obstacleToMove, float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Menunggu selama waktu yang ditentukan

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
}
