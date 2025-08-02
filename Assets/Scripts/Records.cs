using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour
{
    public Text text;
    void Start()
    {
        int m0 = Mathf.FloorToInt(PlayerPrefs.GetFloat("record0") / 60F);
        int m1 = Mathf.FloorToInt(PlayerPrefs.GetFloat("record1") / 60F);
        int m2 = Mathf.FloorToInt(PlayerPrefs.GetFloat("record2") / 60F);
        int s0 = Mathf.FloorToInt(PlayerPrefs.GetFloat("record0") % 60F);
        int s1 = Mathf.FloorToInt(PlayerPrefs.GetFloat("record1") % 60F);
        int s2 = Mathf.FloorToInt(PlayerPrefs.GetFloat("record2") % 60F);
        text.text = $"Easy: {string.Format("{0:00}:{1:00}", m0, s0)}\nMedium: {string.Format("{0:00}:{1:00}", m1, s1)}\nHard: {string.Format("{0:00}:{1:00}", m2, s2)}";
    }
}