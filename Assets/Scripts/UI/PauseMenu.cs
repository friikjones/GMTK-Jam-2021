using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject, optionsMenuObject, quitMenuObject;

    public GameObject pauseFirstButton, optionsFirstButton, optionsCloseButton, quitFirstButton, quitCloseButton;


    private void Start()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);
        quitMenuObject.SetActive(false);
    }

    private void Update()
    {
        //Pause the game if the Escape key or the Backspace button is pressed
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            PauseUnpause();
        }
    }
    
    public void Restart()
    {
        //Reload the active scene
        Debug.Log("Reload Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Main Pause Menu


    public void PauseUnpause()
    {
        //if pause menu is not active in the heirachy then pause the game
        if (!pauseMenuObject.activeInHierarchy)
        {
            pauseMenuObject.SetActive(true);
            Time.timeScale = 0f;

            //Make sure current selected object is not selected on anything
            EventSystem.current.SetSelectedGameObject(null);

            //Change current selected object is now the first button on the options menu
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);

            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
        //else pause menu is active in the heirachy then unpause the game
        else
        {
            pauseMenuObject.SetActive(false);
            Time.timeScale = 1f;
            optionsMenuObject.SetActive(false);
        }
    }

    #endregion

    #region Options Menu

    public void OpenOptions()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the options menu
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()
    {
        pauseMenuObject.SetActive(true);
        optionsMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the options button on the Main Menu
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }

    #endregion

    #region Quit Menu

    public void OpenQuitMenu()
    {
        //Close pause menu
        pauseMenuObject.SetActive(false);

        //Open quit menu
        quitMenuObject.SetActive(true);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the quit menu
        EventSystem.current.SetSelectedGameObject(quitFirstButton);
    }

    public void CloseQuitMenu()
    {
        //Open pause menu
        pauseMenuObject.SetActive(true);

        //Close quit menu
        quitMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the quit button on the pause menu
        EventSystem.current.SetSelectedGameObject(quitCloseButton);
    }

    public void MainMenu()
    {
        //Load Main Menu
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //Quit the game
        Debug.Log("Quit Game");
        Application.Quit();
    }



    #endregion
}
