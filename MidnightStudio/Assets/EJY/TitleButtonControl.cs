using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonControl : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionPanel;
    
    public void OnStartClicked()
    {
        SceneManager.LoadScene("Synopsis");
    }

    public void OnOptionClicked()
    {
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void OnExitClosed()
    {
        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
