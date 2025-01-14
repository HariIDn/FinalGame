using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgePlatform : MonoBehaviour
{
    public delegate void PlatformDestroyed(); // Event delegate
    public event PlatformDestroyed OnPlatformDestroyed; // Event untuk mendengarkan platform yang dihancurkan

    public float disappearDelay = 1f; // Waktu sebelum platform dihancurkan

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Periksa apakah player menyentuh platform
        {
            // Hancurkan platform setelah delay
            Destroy(gameObject, disappearDelay);
        }
    }

    // Fungsi ini akan dipanggil ketika platform dihancurkan
    private void OnDestroy()
    {
        if (OnPlatformDestroyed != null && gameObject != null)
        {
            OnPlatformDestroyed.Invoke();
        }
    }
}