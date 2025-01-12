using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPlatform : MonoBehaviour
{
    public Transform pivot; // Objek pivot (Empty GameObject) sebagai sumbu rotasi
    public float rotationSpeed; // Kecepatan rotasi dalam derajat per detik
    public string playerTag = "Player"; // Tag untuk objek Player
    private bool isRotating = false; // Flag untuk memulai rotasi
    

    // Update is called once per frame
    void Update()
    {
        if (isRotating && pivot != null)
        {
            // Hitung rotasi per frame
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.RotateAround(pivot.position, Vector3.forward, rotationThisFrame);

            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Periksa apakah objek yang bertabrakan memiliki tag "Player"
        if (collision.gameObject.CompareTag(playerTag))
        {
            isRotating = true; // Mulai rotasi
            this.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Periksa apakah player sudah tidak menyentuh
        if (collision.gameObject.CompareTag(playerTag))
        {
            isRotating = false; // Hentikan rotasi
            
            ResetRotation();
        }
    }

    private void ResetRotation()
    {
        // Reset rotasi objek ke posisi awal pada sumbu Z
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        transform.position = new Vector3(2.05f, 2.88f, transform.position.z);
    }
}
