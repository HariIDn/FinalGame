using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 0.0f;
    public float move_input;

    public float jumpForce = 5.0f;
    private int jumpCount = 2; // Menghitung jumlah lompatan
    private int maxJumps = 2; // Batas maksimal lompatan

    // Batas pergerakan pada sumbu X
    private float minX = -9.4f;
    private float maxX = 9.4f;

    private gameManager2 gameMgrScript;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameMgrScript = GameObject.Find("Game Manager").GetComponent<gameManager2>();

    }

    // Update is called once per frame
    void Update()
    {
        // Gerakan horizontal
        move_input = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * Mathf.Abs(move_input));

        // Rotasi sederhana
        if (move_input > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // Menghadap ke depan
        }
        else if (move_input < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0); // Membalikkan badan
        }

        // Membatasi pergerakan dalam boundary pada sumbu X
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Lompat dan double jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumps)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // Reset kecepatan vertikal
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        // Reset lompatan saat menyentuh tanah atau pilar
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pillar"))
        {
            jumpCount = 0;
        }

        // Menangani tabrakan dengan musuh
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Obstacle"))
        {
            // Menghancurkan player ketika bertabrakan dengan musuh
            Destroy(gameObject);
            gameMgrScript.GameOver();

        }


    }
}