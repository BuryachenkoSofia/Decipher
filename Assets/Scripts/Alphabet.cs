using UnityEngine;

public class Alphabet : MonoBehaviour
{
    public GameObject alphabetPanel;
    public void OpenAlphabet()
    {
        alphabetPanel.SetActive(!alphabetPanel.activeSelf);
    }
}
