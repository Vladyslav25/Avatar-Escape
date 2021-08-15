using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuController : MonoBehaviour
{
    [SerializeField]
    private string m_MainMenuSceneName;

    [SerializeField]
    private TextMeshPro m_ScoreText;

    [SerializeField]
    private ScriptableScoreData m_ScoreData;

    private void Awake()
    {
        m_ScoreText.text = m_ScoreData.Score.ToString("0");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(m_MainMenuSceneName);
    }
}
