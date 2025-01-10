using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveEnemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject PlayerObject;
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        PlayerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Cek apakah PlayerObject masih ada
        if (PlayerObject != null)
        {
            // Hitung perbedaan posisi musuh dan pemain
            float distance = PlayerObject.transform.position.x - transform.position.x;

            // Jika pemain berada di kanan
            if (distance > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0); // Musuh menghadap kanan
            }
            // Jika pemain berada di kiri
            else if (distance < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0); // Musuh menghadap kiri
            }

            // Gerakkan musuh menuju pemain di kiri atau kanan
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
