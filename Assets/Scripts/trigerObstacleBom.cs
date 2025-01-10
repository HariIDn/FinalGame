using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerObstacleBom : MonoBehaviour
{
    // Referensi ke objek obstacle
    public GameObject obstacle;
    public float speed = 0.0f;

    // Fungsi untuk mendeteksi player yang masuk trigger
    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang masuk adalah player
        if (other.CompareTag("Player"))
        {
            // Pastikan objek obstacle ada dan memiliki Rigidbody
            if (obstacle != null)
            {
                Rigidbody obstacleRb = obstacle.GetComponent<Rigidbody>();

                // Jika obstacle memiliki Rigidbody, aktifkan gravity-nya
                if (obstacleRb != null)
                {
                    //obstacleRb.useGravity = true; // Mengaktifkan gravity pada obstacle
                    obstacle.transform.Translate(Vector3.down * speed);
                    
                }
            }
            GetComponent<Collider>().enabled = false;
        }
    }
}
