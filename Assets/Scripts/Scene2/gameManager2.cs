using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager2 : MonoBehaviour
{

    public GameObject BadObj;
    public GameObject GoodObj;
    public GameObject triggerPlat;
    public GameObject firstPlat;

    public GameObject Bomb;
    public GameObject Bomb2;
    public GameObject platform;
    private float obstacleSpeed = 18.0f;

    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        BadObj.SetActive(false);
        GoodObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPlatform()
    {
        if (!hasTriggered)
        {
            hasTriggered = true; // Ensure it triggers only once
            StartCoroutine(HandleObjects());
        }

    }

    private IEnumerator HandleObjects()
    {
        // Activate BadObj for 2 seconds
        BadObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        BadObj.SetActive(false);
        yield return new WaitForSeconds(1f);
        firstPlat.SetActive(false);

        // Wait 1 second before activating GoodObj
        yield return new WaitForSeconds(1f);
        GoodObj.SetActive(true);
        firstPlat.SetActive(true);
    }

    public void TriggerBombAndPlatform()
    {
        StartCoroutine(HandleBombAndPlatform());
    }

    private IEnumerator HandleBombAndPlatform()
    {
        if (platform != null)
        {
            Collider platformCollider = platform.GetComponent<Collider>();
            if (platformCollider != null)
            {
                platformCollider.isTrigger = false;
            }
        }

        yield return new WaitForSeconds(1f); // Wait 1 second

        // Move bombs downward using Translate
        if (Bomb != null)
        {
            StartCoroutine(MoveBombDown(Bomb));
        }

        if (Bomb2 != null)
        {
            StartCoroutine(MoveBombDown(Bomb2));
        }

        
        
    }

    private IEnumerator MoveBombDown(GameObject bomb)
    {
        while (bomb != null)
        {
            bomb.transform.Translate(Vector3.down * obstacleSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
