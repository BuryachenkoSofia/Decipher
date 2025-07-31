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
            if (time < PlayerPrefs.GetFloat("record") || !PlayerPrefs.HasKey("record"))
            {
                PlayerPrefs.SetFloat("record", time);
            }
        }
    }
}
