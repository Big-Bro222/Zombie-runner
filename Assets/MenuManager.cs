using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject LevelMenu;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameSettingsMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        LevelMenu.SetActive(false);
    }



    public void SetMainMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        LevelMenu.SetActive(false);
    }


    public void SetLevelMenu()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        LevelMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string Levelname)
    {
        SceneManager.LoadScene(Levelname);
    }
}
