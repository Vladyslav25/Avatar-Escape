using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    NONE = 0,
    MAIN_MENU = 10,
    GAME_SCENE = 20,
    DEMO_SCENE,
}

[System.Serializable]
public class SceneListEntry
{
    public SceneType sceneType;
    public int buildId;
}

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Dictionary<SceneType, int> m_
}
