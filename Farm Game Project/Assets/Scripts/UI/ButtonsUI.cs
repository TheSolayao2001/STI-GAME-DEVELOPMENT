using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameUI;
    public GameObject GetGameUI { get { return _gameUI; } }
    [SerializeField] private GameObject _gameOverUI;
    public GameObject GetGameOverUI { get { return _gameOverUI; } }
    [SerializeField] private GameObject _settingsMenu;
    public GameObject GetSettingsMenu { get { return _settingsMenu; } }

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _finalScore;
    public TextMeshProUGUI GetFinalScore { get { return _finalScore; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BTN_Settings()
    {
        Time.timeScale = 0f;
        _settingsMenu.SetActive(true);
        _gameUI.SetActive(false);
    }

    public void BTN_Resume()
    {
        Time.timeScale = 1f;
        _gameUI.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void BTN_RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BTN_MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
