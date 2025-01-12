using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTriggerScript : MonoBehaviour
{
    private gameManager2 gm2;

    // Start is called before the first frame update
    void Start()
    {
        gm2 = GameObject.Find("GameManager").GetComponent<gameManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm2.TriggerBombAndPlatform();
        }
    }
}
