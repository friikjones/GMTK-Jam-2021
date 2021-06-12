using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuObject, optionsMenuObject;

    public GameObject MainFirstButton, optionsFirstButton, optionsCloseButton;


    private void Start()
    {
        mainMenuObject.SetActive(true);
        optionsMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the Main Menu
        EventSystem.current.SetSelectedGameObject(MainFirstButton);
    }

    public void StartGame()
    {
        //Get first scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //Quit the game
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenOptions()
    {
        mainMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the options menu
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()
    {
        mainMenuObject.SetActive(true);
        optionsMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the options button on the Main Menu
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }
}
