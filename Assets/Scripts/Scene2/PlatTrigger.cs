using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatTrigger : MonoBehaviour
{

    private gameManager2 gm2;
    
    // Start is called before the first frame update
    void Start()
    {
        gm2 = GameObject.Find("Game Manager").GetComponent<gameManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            gm2.SpawnPlatform();
        }
    }
}
