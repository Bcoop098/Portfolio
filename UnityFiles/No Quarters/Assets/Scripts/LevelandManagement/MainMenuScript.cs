using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] List<Canvas> allCanvi = new List<Canvas>();
    GameObject obj;
    EventSystem eventObj;

    int numberOfCanvas;

    private void Awake()
    {
        int i = 0;
        Canvas[] tmpCanvi = FindObjectsOfType<Canvas>();
       
        foreach (Canvas canvi in tmpCanvi)
        {
            if(canvi.name != "MainCanvas")
            {
                allCanvi.Add(canvi);
            }
        }

        eventObj = GameObject.FindObjectOfType<EventSystem>();

        if(Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetNewGameStatus() == false)
        {
            for (int j = 0; i < allCanvi.Count; i++)
            {
                if (allCanvi[j].name == "MainMenuCanvas")
                {
                    allCanvi[j].transform.Find("StartButton").transform.Find("Text").GetComponent<Text>().text = "Continue";
                }
            }
        }
    }

    private void Start()
    {      
        TurnSelectedCanvasOnByName("MainMenuCanvas");
    }

    public void StartGame()
    {
        if (Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetNewGameStatus() == true)
        {
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").SetNewGameStatus(false);
            TurnSelectedCanvasOnByName("PlayerSelectCanvas");
        }
        else
            TurnSelectedCanvasOnByName("LevelSelectCanvas");

    }

    public void Retry()
    {
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").RetryLevel();
    }
    public void LoadGame()
    {
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").StartGame();
    }

    public void LoadByIndex(int index)
    {      
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").LoadByIndex(index);
    }

    public void LoadByName(string name)
    {
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").LoadByName(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        TurnSelectedCanvasOnByName("MainMenuCanvas");
    }

    public void TurnSelectedCanvasOnByIndex(int index)
    {
        for(int i = 0; i < allCanvi.Count;i++)
        {
            if (allCanvi[i] == allCanvi[index])
            {
                eventObj.SetSelectedGameObject(allCanvi[index].transform.GetChild(2).gameObject);
                allCanvi[index].gameObject.SetActive(true);
            }             
            else
                allCanvi[i].gameObject.SetActive(false);
        }
    }

    public void TurnSelectedCanvasOnByName(string canvasName)
    {
        for (int i = 0; i < allCanvi.Count; i++)
        {
            if (allCanvi[i].name == canvasName)
            {
                eventObj.SetSelectedGameObject(allCanvi[i].transform.GetChild(2).gameObject);
                allCanvi[i].gameObject.SetActive(true);
            }               
            else
                allCanvi[i].gameObject.SetActive(false);
        }
    }

    public void SelectNumberOfPlayers(int num)
    {
        Toolbox.Instance.GetObject<PlayerData>("PlayerData").setOfPlayersChosen(num);
    }

    public void ToggleMusic()
    {
        var buttonColor = GameObject.Find("AudioButton").GetComponent<Button>().colors;
        var buttonColor2 = GameObject.Find("AudioButton").GetComponent<Button>().colors.normalColor;
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").ToggleMusic();
        switch (Toolbox.Instance.GetObject<AudioManager>("AudioManager").audioEnable)
        {
            case true:         
                buttonColor.normalColor = Color.white;

                GameObject.Find("AudioButton").GetComponent<Button>().colors = buttonColor;
                break;

            case false:
                buttonColor.normalColor = Color.grey;
                GameObject.Find("AudioButton").GetComponent<Button>().colors = buttonColor;
                break;
        }
    }
}
