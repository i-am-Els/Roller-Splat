using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private int maxLevelIndex;
    private GroundPiece[] allgroundPieces;
    void Start()
    {
        maxLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
        Debug.Log(maxLevelIndex);
        SetupNewLevel();
    }

    private void SetupNewLevel()
    {
        allgroundPieces = FindObjectsOfType<GroundPiece>();
    }

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLeveLFinishedLoading;   
    }

    private void OnLeveLFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool isFinished = true;

        for(int i = 0; i < allgroundPieces.Length; i++)
        {
            if(allgroundPieces[i].isColored == false)
            {
                isFinished = false;
            }
        }

        if (isFinished)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == maxLevelIndex)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
