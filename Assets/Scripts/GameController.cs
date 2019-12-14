using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Adventurers"), LayerMask.NameToLayer("Adventurers"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelCompleted()
    {
        uiController.ShowCompletedLevelScreen(currentLevel);
    }
}
