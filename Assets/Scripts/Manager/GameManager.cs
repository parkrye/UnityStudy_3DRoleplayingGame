using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static SceneManager sceneManager;

    public static GameManager Instance { get { return instance; } }
    public static SceneManager Scene { get { return sceneManager; } }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    void InitManagers()
    {
        GameObject sceneObj = new GameObject();
        sceneObj.name = "SceneManager";
        sceneObj.transform.parent = transform;
        sceneManager = sceneObj.AddComponent<SceneManager>();
    }
}
