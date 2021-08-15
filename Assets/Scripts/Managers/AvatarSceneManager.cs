using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum SCENETYPE
{
    NONE = 0,
    [InspectorName("Main Menu")]
    MAIN_MENU = 10,
    [InspectorName("Game Scene")]
    GAME_SCENE = 20,
    [InspectorName("Demo Scene")]
    DEMO_SCENE,
}

[System.Serializable]
public class SceneListEntry
{
    public SCENETYPE sceneType;
    public int buildId;
}

public class AvatarSceneManager : MonoBehaviour
{
    public static AvatarSceneManager Instance;

    [SerializeField]
    private List<SceneListEntry> m_scenes = new List<SceneListEntry>();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(SCENETYPE _sceneType)
    {
        SceneManager.LoadScene(m_scenes.First(_item => _item.sceneType == _sceneType)
            .buildId);
    }
}
