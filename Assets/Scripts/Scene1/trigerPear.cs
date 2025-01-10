using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerPear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
}
