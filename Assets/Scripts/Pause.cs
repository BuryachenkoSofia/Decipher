using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public void PausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = !pausePanel.activeSelf ? 1f : 0f;
    }
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PausePanel();
        }
    }
}
