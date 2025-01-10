using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject UpDownPlatform;

    private Vector3 startPosition; // Posisi awal platform
    public float movementDistance = 2.0f; // Jarak gerakan platform ke atas atau ke bawah
    public float speed = 2.0f; // Kecepatan gerakan

    private bool isMoving = false; // Menandai apakah platform sedang bergerak

    // Start is called before the first frame update
    void Start()
    {
        if (UpDownPlatform != null)
        {
            startPosition = UpDownPlatform.transform.position; // Simpan posisi awal platform
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemyandPlatform() // nama SpawnObject harus sama dengan di InvokeRepeating
    {
        Instantiate(Enemy, new Vector3(9.5f, -3.455f, 0.0f), Enemy.transform.rotation);
        UpDownPlatform.SetActive(true);

        if (!isMoving)
        {
            StartCoroutine(MovePlatform());
        }
    }

    public IEnumerator SpawnEnemyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Menunggu selama waktu yang ditentukan
        SpawnEnemyandPlatform(); // Memanggil SpawnEnemy setelah delay
    }

    private IEnumerator MovePlatform()
    {
        isMoving = true; // Menandai platform sedang bergerak

        while (true)
        {
            // Gerakan ke atas selama 2 detik
            yield return MoveForDuration(Vector3.up, 2.0f);

            // Gerakan ke bawah selama 2 detik
            yield return MoveForDuration(Vector3.down, 2.0f);
        }
    }

    private IEnumerator MoveForDuration(Vector3 direction, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (UpDownPlatform != null)
            {
                UpDownPlatform.transform.position += direction * speed * Time.deltaTime;
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Tunggu frame berikutnya
        }
    }
}
