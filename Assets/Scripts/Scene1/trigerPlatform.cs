using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerPlatform : MonoBehaviour
{
    public GameObject platform; // Objek yang ingin dinonaktifkan sementara
    private float disableDuration = 1f; // Durasi objek dinonaktifkan (dalam detik)

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan hanya player yang memicu event ini (ganti "Player" dengan tag yang sesuai)
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisableObjectTemporarily());
            GetComponent<Collider>().enabled = false; // Menonaktifkan trigger agar hanya sekali
        }
    }

    private IEnumerator DisableObjectTemporarily()
    {
        if (platform != null)
        {
            platform.SetActive(false); // Nonaktifkan objek
            yield return new WaitForSeconds(disableDuration); // Tunggu selama durasi yang ditentukan
            platform.SetActive(true); // Aktifkan kembali objek
        }
        else
        {
            Debug.LogWarning("Target Object belum diatur!");
        }
    }
}
