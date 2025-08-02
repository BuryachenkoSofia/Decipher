using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Text text;
    public Slider slider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
        }
    }
    private void Start()
    {
        PlayerPrefs.GetFloat("volume");
        text.text = "Volume: " + Mathf.FloorToInt(slider.value * 100);
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    void Update()
    {
        text.text = "Volume: " + Mathf.FloorToInt(slider.value * 100);
        PlayerPrefs.SetFloat("volume", slider.value);
    }
}
