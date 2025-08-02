using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private GameObject audioPrefab;

    public void PlaySound()
    {
        if (audioPrefab != null)
        {
            GameObject audioInstance = Instantiate(audioPrefab);
            AudioSource audioSource = audioInstance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = PlayerPrefs.GetFloat("volume");
                audioSource.Play();
                Destroy(audioInstance, audioSource.clip.length);
            }
        }
    }
}