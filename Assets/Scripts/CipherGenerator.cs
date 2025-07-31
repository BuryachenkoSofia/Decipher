using Unity.VisualScripting;
using UnityEngine;

public class CipherGenerator : MonoBehaviour
{
    public struct Keys
    {
        public int a, b;
    }

    private const int alphabetLength = 26;
    private int[] relativelyPrime = { 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };

    public Keys GenerateParameters()
    {
        int randInt = Random.Range(0, relativelyPrime.Length);
        int a = relativelyPrime[randInt];
        int b = Random.Range(0, alphabetLength);
        return new Keys { a = a, b = b };
    }
    private char E(char x, Keys keys, int alphabetLength)
    {
        if (x < 'A' || x > 'Z') return x;
        return (char)((keys.a * (x - 'A') + keys.b) % alphabetLength + 'A');
    }
    public string GenerateEncryptedSentence(string x, Keys keys, int alphabetLength)
    {
        string result = "";
        x = x.ToUpper();
        foreach (char i in x)
        {
            result += E(i, keys, alphabetLength);
        }
        Debug.Log(result + " a=" + keys.a + " b=" + keys.b);
        return result;
    }
    private void Start()
    {
        //Keys keys = GenerateParameters();
        Keys keys = new Keys { a = 1, b = 25 };
        GenerateEncryptedSentence("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", keys, alphabetLength);
    }
}