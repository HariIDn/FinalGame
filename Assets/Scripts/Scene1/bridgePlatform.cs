using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgePlatform : MonoBehaviour
{
    public float disappearDelay = 1f; // Waktu sebelum platform dihancurkan
    public System.Action OnPlatformDestroyedCallback; // Callback saat platform dihancurkan

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, disappearDelay); // Hancurkan platform setelah delay
            Invoke(nameof(NotifyPlatformDestroyed), disappearDelay); // Panggil callback setelah delay
        }
    }

    private void NotifyPlatformDestroyed()
    {
        // Panggil callback jika ada
        OnPlatformDestroyedCallback?.Invoke();
    }
}
