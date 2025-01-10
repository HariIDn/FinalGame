using System.Collections;
using UnityEngine;

public class trigerChallange : MonoBehaviour
{
    // Referensi ke objek obstacle pertama dan kedua
    public GameObject pilarObstacle; // Objek kedua
    public GameObject hiddenWall;
    private float moveSpeed2 = 8.0f; // Kecepatan gerakan ke kanan untuk obstacle2
    private float destroyLimitX = 20f;

    public GameObject thorn;

    // Referensi ke gameManager untuk memanggil SpawnEnemy
    private gameManager gameMgrScript;

    private void Start()
    {
        // Mendapatkan referensi gameManager secara dinamis
        gameMgrScript = GameObject.Find("Game Manager").GetComponent<gameManager>();
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

            gameMgrScript.SpawnApple();

            // Pastikan obstacle kedua tidak null dan menunggu 2 detik sebelum bergerak
            StartCoroutine(WaitAndMoveObstacleToRight(pilarObstacle, 1.5f)); // Menunggu 2 detik dan bergerak ke kanan

            // Memanggil fungsi SpawnEnemy dari gameManager setelah trigger aktif

            StartCoroutine(gameMgrScript.SpawnEnemyWithDelay(4f)); // Menunggu 2 detik sebelum spawn enemy // Memanggil SpawnEnemy di gameManager

            GetComponent<Collider>().enabled = false; // Menonaktifkan trigger agar hanya sekali

            thorn.SetActive(true); // Aktifkan
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
}
