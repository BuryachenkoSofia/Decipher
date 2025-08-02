using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float time = 0;
    public Text text;
    public CipherGenerator cipherGenerator;

    private void Update()
    {
        time += Time.deltaTime;
        int m = Mathf.FloorToInt(time / 60F);
        int s = Mathf.FloorToInt(time % 60F);
        text.text = string.Format("{0:00}:{1:00}", m, s);
        if (cipherGenerator.win)
        {
            if (PlayerPrefs.GetInt("level") == 0 && (time < PlayerPrefs.GetFloat("record0") || !PlayerPrefs.HasKey("record0")))
            {
                PlayerPrefs.SetFloat("record0", time);
            }
            if (PlayerPrefs.GetInt("level") == 1 && (time < PlayerPrefs.GetFloat("record1") || !PlayerPrefs.HasKey("record1")))
            {
                PlayerPrefs.SetFloat("record1", time);
            }
            if (PlayerPrefs.GetInt("level") == 2 && (time < PlayerPrefs.GetFloat("record2") || !PlayerPrefs.HasKey("record2")))
            {
                PlayerPrefs.SetFloat("record2", time);
            }
        }
    }
}
