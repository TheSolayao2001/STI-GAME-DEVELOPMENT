using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BTN_PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BTN_Quit()
    {
        Application.Quit();
    }
}
