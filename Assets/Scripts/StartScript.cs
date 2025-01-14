using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
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
        }

        // Pindah ke Scene berikutnya (Scene 1)
        SceneManager.LoadScene(1);
    }

}
