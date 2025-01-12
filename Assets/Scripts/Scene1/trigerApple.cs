using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerApple : MonoBehaviour
{
    // Referensi ke gameManager untuk memanggil SpawnEnemy
    private gameManager gameMgrScript;
    

    // Start is called before the first frame update
    void Start()
    {
        // Mendapatkan referensi gameManager secara dinamis
        gameMgrScript = GameObject.Find("Game Manager").GetComponent<gameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang masuk adalah player
        if (other.CompareTag("Player"))
        {
            

            gameMgrScript.SpawnPlatform();
            gameMgrScript.SpawnPear();
            
            Destroy(gameObject);
        }
    }
}
