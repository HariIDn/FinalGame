using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager2 : MonoBehaviour
{

    public GameObject BadObj;
    public GameObject GoodObj;
    public GameObject triggerPlat;
    public GameObject firstPlat;
    public GameObject memeImages;

    public GameObject Bomb;
    public GameObject Bomb2;
    public GameObject platform;
    private float obstacleSpeed = 18.0f;

    public AudioClip gameOverSound1; // Tambahkan AudioClip untuk Game Over pertama
    public AudioClip gameOverSound2; // Tambahkan AudioClip untuk Game Over kedua
    private AudioSource audioSource; // Komponen AudioSource

    private bool isGameOver = false;
    public TextMeshProUGUI livestext;

    private bool hasTriggered = false;

    public AudioClip triggerSound; // Klip suara untuk trigger

    // Start is called before the first frame update
    void Start()
    {
        BadObj.SetActive(false);
        GoodObj.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to the GameManager!");
        }

        livestext.text = ": " + mainManager.Instance.live;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        // Mainkan suara Game Over pertama
        if (audioSource != null && gameOverSound1 != null)
        {
            audioSource.PlayOneShot(gameOverSound1);
        }

        // Mainkan suara Game Over kedua setelah 1 detik
        StartCoroutine(PlaySecondGameOverSound());

    }

    private IEnumerator PlaySecondGameOverSound()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (audioSource != null && gameOverSound2 != null)
        {
            audioSource.PlayOneShot(gameOverSound2);
            memeImages.SetActive(true);
            ReduceLive();
        }

        // Tunggu hingga suara kedua selesai sebelum restart
        if (gameOverSound2 != null && gameOverSound2 != null)
        {
            yield return new WaitForSecondsRealtime(gameOverSound2.length);
        }

        // Restart scene
        SceneManager.LoadScene(2);

        // Kembalikan Time.timeScale ke 1 sebelum memuat ulang scene
        Time.timeScale = 1f;
    }

    public void ReduceLive()
    {
        mainManager.Instance.ReduceLives();
        livestext.text = ": " + mainManager.Instance.live;
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

        // Mainkan suara trigger jika ada AudioSource dan AudioClip
        if (audioSource != null && triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound); // Memainkan suara satu kali
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

    public void MovetoMainMenuGame()
    {
        mainManager.Instance.StopBGM();
        SceneManager.LoadScene(0);
    }
}
