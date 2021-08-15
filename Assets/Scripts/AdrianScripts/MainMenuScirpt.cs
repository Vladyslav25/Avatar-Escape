using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScirpt : MonoBehaviour
{
    [SerializeField]
    private string m_NameOfGameLevel;

    [SerializeField]
    private string m_NameOfTutorial;


    public void StartLevel()
    {
        SceneManager.LoadScene(m_NameOfGameLevel);
    }

    public void EndGame()
    {
        Application.Quit(); 
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene(m_NameOfTutorial);
    }
}
