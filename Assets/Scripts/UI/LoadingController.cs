using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public AsyncOperation operation;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevelAssync("MainMenu", false);

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = operation.progress;
    }

    public void LoadLevelAssync(string sceneName, bool additive = true)
    {
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
