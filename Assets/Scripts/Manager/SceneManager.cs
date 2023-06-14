using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    BaseScene curScene;
    LoadingUI loadingUI;
    public BaseScene CurScene
    {
        get 
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();
            return curScene; 
        }
    }

    private void Awake()
    {
        loadingUI = Resources.Load<LoadingUI>("UI/LoadingUI");
        loadingUI = Instantiate(loadingUI);
        loadingUI.transform.SetParent(transform);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.FadeOut();
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncOperation = UnitySceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, asyncOperation.progress));
            yield return null;
        }

        CurScene.LoadAsync();
        while(CurScene.progress < 1f)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
        }

        loadingUI.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }
}
