using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerPear : MonoBehaviour
{
    private gameManager gameMgrScript; // Referensi ke game manager

    void Start()
    {
        gameMgrScript = GameObject.Find("Game Manager").GetComponent<gameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang masuk adalah player
        if (other.CompareTag("Player"))
        {
            // Panggil fungsi untuk memunculkan platform di posisi yang sudah diatur di gameManager
            gameMgrScript.SpawnBridgePlatform();

            Destroy(gameObject); // Hancurkan pear
        }
    }
}
