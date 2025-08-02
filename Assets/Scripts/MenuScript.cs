using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame(int level)
    {
        if (level < 0 || level > 2) return;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }
}
