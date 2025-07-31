using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CipherGenerator : MonoBehaviour
{
    public struct Keys { public int a, b; }

    private const int alphabetLength = 26;
    private int[] relativelyPrime = { 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };

    private string[] sentences;

    private string RandomSentence()
    {
        int rand = Random.Range(0, sentences.Length);
        return sentences[rand];
    }

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

    public void GenerateUI(string sentence)
    {
        float width = ((RectTransform)this.gameObject.transform).sizeDelta.x;
        int maxLength = (int)(width / 40.0f);
        string[] words = sentence.Split(" ");
        int currentLength = 0;
        foreach (string word in words)
        {
            if (currentLength + word.Length > maxLength)
            {
                for (int i = 0; i < maxLength - currentLength; ++i)
                {
                    Instantiate(spacePrefab, this.gameObject.transform);
                }
                currentLength = 0;
            }
            foreach (char i in word)
            {
                GameObject obj = Instantiate(charPrefab, this.gameObject.transform);
                obj.GetComponentInChildren<Text>().text = i.ToString();
                currentLength++;

                inputs.Add(obj.GetComponentInChildren<InputField>().GetComponentInChildren<Text>());
            }
            Instantiate(spacePrefab, this.gameObject.transform);
            currentLength++;
        }
    }

    public bool Check(string originalSentence)
    {
        string inputStr = "";
        foreach (var i in inputs)
        {
            i.text = i.text.ToUpper();
            if (string.IsNullOrEmpty(i.text)) return false;
            inputStr += i.text.ToCharArray()[0];
        }
        originalSentence = originalSentence.ToUpper();
        originalSentence = Regex.Replace(originalSentence, "[^A-Z]", "");
        return inputStr == originalSentence;
    }

    public GameObject charPrefab, spacePrefab;
    public List<Text> inputs = new List<Text>();
    private string originalSentence;

    private void Start()
    {
        string path = Application.dataPath + "/proverbs.txt"; 
        sentences = File.ReadAllLines(path);

        //Keys keys = new Keys { a = 1, b = 25 };
        //originalSentence="hello";

        Keys keys = GenerateParameters();
        originalSentence = RandomSentence();
        
        string sentence = GenerateEncryptedSentence(originalSentence, keys, alphabetLength);
        GenerateUI(sentence);
    }

    private void Update()
    {
        if (Check(originalSentence))
        {
            Debug.Log("WIN");
        }
    }
}