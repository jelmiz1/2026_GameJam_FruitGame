using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public TMP_Text healthText;
    public static GameManager Instance { get; private set; }
    public GameObject mainMenuPanel;
    public GameObject hudPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    bool isPaused;
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    // Runs every time a scene loads, including the first one
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (scene.name == "MainMenu")
            ShowOnly(mainMenuPanel);
        else
            ShowOnly(hudPanel);
    }

    void ShowOnly(GameObject panel)
    {
        mainMenuPanel.SetActive(panel == mainMenuPanel);
        hudPanel.SetActive(panel == hudPanel);
        pausePanel.SetActive(panel == pausePanel);
        gameOverPanel.SetActive(panel == gameOverPanel);
        levelCompletePanel.SetActive(panel == levelCompletePanel);
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        ShowOnly(isPaused ? pausePanel : hudPanel);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        ShowOnly(gameOverPanel);
    }
    public void LevelComplete()
    {
        Time.timeScale = 0f;
        ShowOnly(levelCompletePanel);
    }

public void SetHealthUI(int health)
{

    healthText.text = "Health: " + health;
}

}
