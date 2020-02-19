using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    [SerializeField] int sCurrentLevel = 1;
    [SerializeField] string sCurrentLevelName = "MainMenu";
    [SerializeField] int previousLevel = 1;
    [SerializeField] string previousLevelName = "";

    bool levelStart = false;
    bool newGame = true;
    static int numberOfScenes;
    public List<string> pathOfScenes;

    private void Start()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
        pathOfScenes = new List<string>();
        for (int i = 0; i < numberOfScenes; i++)
        {

            pathOfScenes.Add(SceneUtility.GetScenePathByBuildIndex(i).Substring(14));
        }  
    }

    public void StartGame()
    {
        levelStart = true;
        newGame = false;
        Toolbox.Instance.GetObject<PixelLord>("PixelLord").SetGameStatus(levelStart);

        sCurrentLevel = 2;
        sCurrentLevelName = pathOfScenes[2];

        SceneManager.LoadScene(sCurrentLevel);
    }

    public void NextLevel()
    {
        previousLevel = sCurrentLevel;
        previousLevelName = sCurrentLevelName;
        sCurrentLevel++;
        sCurrentLevelName = pathOfScenes[sCurrentLevel];
        levelStart = true;
        switch(sCurrentLevel)
        {
            //Barista
            case 3:
                Toolbox.Instance.AddObject<BaristaScript>("BaristaScript");
                break;

            //Cleaner
            case 5:
                //Toolbox.Instance.AddObject<CleanerScript>("CleanerScript");
                break;

            //Banker
            case 7:

                break;
        }
        SceneManager.LoadScene(sCurrentLevel);
    }

    public void LoadByIndex(int index)
    {
        if (Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").activateLevels[index] == true)
        {
            previousLevel = sCurrentLevel;
            previousLevelName = sCurrentLevelName;
            sCurrentLevel = index;
            sCurrentLevelName = pathOfScenes[sCurrentLevel];
            Debug.Log(sCurrentLevelName);
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").StartLevel(sCurrentLevelName);
            levelStart = true;
            SceneManager.LoadScene(sCurrentLevel);
        }
    }
    public void LoadByName(string name)
    {
        for(int i = 0; i < numberOfScenes; i++)
        {       
            if (pathOfScenes[i] == name+".unity" && Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").activateLevels[i] == true)
            {
                previousLevel = sCurrentLevel;
                previousLevelName = sCurrentLevelName;
                sCurrentLevel = i;
                sCurrentLevelName = name;
                Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").StartLevel(sCurrentLevelName);
                levelStart = true;
                SceneManager.LoadScene(name);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        sCurrentLevel = 1;
        sCurrentLevelName = "MainMenu";
        SceneManager.LoadScene(sCurrentLevel);
    }

    public void RetryLevel()
    {
        levelStart = true;
        sCurrentLevel = previousLevel;
        sCurrentLevelName = previousLevelName;
        SceneManager.LoadScene(sCurrentLevel);
    }

    //Getters and Setters
    public void SetCurrentLevel(int levelID) { sCurrentLevel = levelID; }

    public void SetCurrentLevel(string levelName) {  sCurrentLevelName = levelName; }

    public int GetCurrentLevel() { return sCurrentLevel; }

    public string GetCurrentLevelString() { return sCurrentLevelName; }

    public bool GetLevelStartStatus() { return levelStart; }

    //This gets the status that means has the game already started before.
    public bool GetNewStartStatus() { return newGame; }
}
