using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public UIController uiController;

    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private bool gamePaused = false;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Adventurers"), LayerMask.NameToLayer("Adventurers"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);

    }

    public void LevelCompleted()
    {
        uiController.ShowCompletedLevelScreen(currentLevel);
    }

    private void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene("Level " + level);

        if (gamePaused)
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGameMenu()
    {
        TogglePauseGame();
        uiController.ShowPauseMenu(gamePaused);
    }

    private void TogglePauseGame()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0 : 1;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }
}
