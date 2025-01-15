using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{

    public TextMeshProUGUI bestscoretext;
    // Start is called before the first frame update
    void Start()
    {
        mainManager.Instance.LoadLivesData();
        bestscoretext.text = "Best Score: " + mainManager.Instance.bestScore;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // Pastikan mainManager.Instance tidak null sebelum memanggil PlayBGM
        if (mainManager.Instance != null)
        {
            mainManager.Instance.PlayBGM(); // Mainkan musik BGM
            mainManager.Instance.live = 3;
        }
        Time.timeScale = 1f;
        // Pindah ke Scene berikutnya (Scene 1)
        SceneManager.LoadScene(1);
    }

}
