using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : InteractableObject
{
    public int sceneToBeLoaded;
    public Animator loadingScreen;

    private AsyncOperation async;

    public override void TriggerInteraction(GameObject source)
    {
        StartCoroutine(LoadingScreen(sceneToBeLoaded));
    }

    private IEnumerator LoadingScreen(int sceneIndex)
    {
        async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;
        loadingScreen.SetBool("Transition", true);
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForEndOfFrame();

        while (async.isDone == false)
        {

            yield return new WaitForEndOfFrame();
            if (async.progress == 0.9f)
            {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
        loadingScreen.SetBool("Transition", false);
    }
}
