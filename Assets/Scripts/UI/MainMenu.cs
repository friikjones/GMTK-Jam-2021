using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuObject, optionsMenuObject, trackMenuObject, tutorialMenuObject;

    public GameObject MainFirstButton, trackFirstButton, trackCloseButton, optionsFirstButton, optionsCloseButton, tutorialFirstButton, tutorialCloseButton;


    private void Start()
    {
        //Make sure that only the Main Menu is active in the heirarchy
        mainMenuObject.SetActive(true);
        optionsMenuObject.SetActive(false);
        tutorialMenuObject.SetActive(false);
        trackMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the Main Menu
        EventSystem.current.SetSelectedGameObject(MainFirstButton);
    }

    #region Track Selection
    public void OpenTrackSelection()
    {
        mainMenuObject.SetActive(false);
        trackMenuObject.SetActive(true);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the options menu
        EventSystem.current.SetSelectedGameObject(trackFirstButton);
    }
    public void CloseTrackSelection()
    {
        mainMenuObject.SetActive(true);
        trackMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the first button on the options menu
        EventSystem.current.SetSelectedGameObject(trackCloseButton);
    }

    #endregion


    #region How To Play

    public void OpenTutorialMenu()
    {
        mainMenuObject.SetActive(false);
        tutorialMenuObject.SetActive(true);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the options button on the Main Menu
        EventSystem.current.SetSelectedGameObject(tutorialFirstButton);
    }

    public void CloseTutorialMenu()
    {
        mainMenuObject.SetActive(true);
        tutorialMenuObject.SetActive(false);

        //Make sure current selected object is not selected on anything
        EventSystem.current.SetSelectedGameObject(null);

        //Change current selected object is now the options button on the Main Menu
        EventSystem.current.SetSelectedGameObject(tutorialCloseButton);
    }

    #endregion

    public void StartEightTrack()
    {
        //Get first scene in the build index
        LoadLevelAssync("T1_EightTrack");
    }
    public void StartIceTrack()
    {
        //Get first scene in the build index
        LoadLevelAssync("T2_IceTrack");
    }

    public void StartThirdTrack()
    {
        //Get first scene in the build index
        LoadLevelAssync("T3_Wipeout");
    }

    #region OptionsMenu

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

    #endregion

    public void LoadLevelAssync(string sceneName, bool additive = false)
    {
        AsyncOperation operation;
        //Resolve additive mode
        if (additive)
        {
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            operation = SceneManager.LoadSceneAsync(sceneName);

        }
        //Stops scene transition 
        operation.allowSceneActivation = false;
        StartCoroutine(WaitForLoading(operation));
    }


    IEnumerator WaitForLoading(AsyncOperation operation)
    {
        //Holds the loading in place until progress is 90%
        while (operation.progress < 0.9f)
        {
            yield return null;
        }
        //Allows scene transition
        operation.allowSceneActivation = true;
    }
}
