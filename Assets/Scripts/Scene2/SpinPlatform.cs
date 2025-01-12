using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPlatform : MonoBehaviour
{
    public Transform pivot; // Objek pivot (Empty GameObject) sebagai sumbu rotasi
    public float rotationSpeed; // Kecepatan rotasi dalam derajat per detik
    public string playerTag = "Player"; // Tag untuk objek Player
    private bool isRotating = false; // Flag untuk memulai rotasi
    private float currentRotation = 0f; // Rotasi saat ini dalam derajat

    // Update is called once per frame
    void Update()
    {
        if (isRotating && pivot != null)
        {
            // Hitung rotasi per frame
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.RotateAround(pivot.position, Vector3.forward, rotationThisFrame);

            // Tambahkan rotasi saat ini
            currentRotation += rotationThisFrame;

            // Hentikan rotasi setelah satu putaran penuh
            if (currentRotation >= 360f)
            {
                isRotating = false;
                currentRotation = 0f; // Reset rotasi
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Periksa apakah objek yang bertabrakan memiliki tag "Player"
        if (collision.gameObject.CompareTag(playerTag))
        {
            isRotating = true; // Mulai rotasi
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Periksa apakah player sudah tidak menyentuh
        if (collision.gameObject.CompareTag(playerTag))
        {
            isRotating = false; // Hentikan rotasi
            currentRotation = 0f; // Reset rotasi jika ingin mulai lagi dari awal
            ResetRotation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isRotating = true; // Mulai rotasi
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isRotating = false; // Hentikan rotasi
            currentRotation = 0f; // Reset rotasi jika ingin mulai lagi dari awal
            ResetRotation();
        }
    }

    private void ResetRotation()
    {
        // Reset rotasi objek ke posisi awal pada sumbu Z
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        transform.position = new Vector3(2.05f, 4.82f, transform.position.z);
    }
}
