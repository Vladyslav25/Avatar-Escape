using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private string m_EndSceneName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this; 
    }

    public void GameLost()
    {
        SceneManager.LoadScene(m_EndSceneName);
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
