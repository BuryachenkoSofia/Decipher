using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Controls : MonoBehaviour
{
    public CipherGenerator cipherGenerator;
    private void Update()
    {
        for (int i = 0; i < cipherGenerator.inputFields.Count; i++)
        {
            if (cipherGenerator.inputFields[i].isFocused)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (i + 1 < cipherGenerator.inputFields.Count)
                    {
                        FocusInput(cipherGenerator.inputFields[i + 1]);
                    }
                    else
                    {
                        FocusInput(cipherGenerator.inputFields[0]);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (i - 1 >= 0)
                    {
                        FocusInput(cipherGenerator.inputFields[i - 1]);
                    }
                    else
                    {
                        FocusInput(cipherGenerator.inputFields[cipherGenerator.inputFields.Count - 1]);
                    }
                }
                break;
            }
        }
    }

    void FocusInput(InputField field)
    {
        EventSystem.current.SetSelectedGameObject(field.gameObject);
        field.ActivateInputField();
        field.caretPosition = field.text.Length;
    }
}
