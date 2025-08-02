using UnityEngine;
using UnityEngine.EventSystems;

public class InputFieldSound : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject audioPrefab;

    public void OnSelect(BaseEventData eventData)
    {
        if (audioPrefab != null)
        {
            GameObject audioInstance = Instantiate(audioPrefab);
            AudioSource audioSource = audioInstance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
                Destroy(audioInstance, audioSource.clip.length);
            }
        }
    }
}
