using System;
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
        int rand = UnityEngine.Random.Range(0, sentences.Length);
        return sentences[rand];
    }

    public Keys GenerateParameters(int level)
    {
        int a;
        if (level == 0)
        {
            a = 1;
        }
        else if (level == 1)
        {
            int randInt = UnityEngine.Random.Range(0, 3);
            a = relativelyPrime[randInt];
        }
        else
        {
            int randInt = UnityEngine.Random.Range(0, relativelyPrime.Length);
            a = relativelyPrime[randInt];

        }
        int b = UnityEngine.Random.Range(1, alphabetLength);
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
        Debug.Log(originalSentence + " " + result + " a=" + keys.a + " b=" + keys.b);
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
                inputFields.Add(obj.GetComponentInChildren<InputField>());
            }
            if (currentLength != maxLength)
            {
                Instantiate(spacePrefab, this.gameObject.transform);
                currentLength++;
            }

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

    public GameObject charPrefab, spacePrefab, winPanel;
    public bool win = false;
    public List<Text> inputs = new List<Text>();
    public List<InputField> inputFields  = new List<InputField>();
    public Text hintText;
    private string originalSentence, encryptedSentence;

    private void Start()
    {
        Time.timeScale = 1f;
        win = false;
        winPanel.SetActive(false);

        TextAsset textFile = Resources.Load<TextAsset>("proverbs");
        sentences = textFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < sentences.Length; i++) sentences[i] = sentences[i].Trim();
        Array.Sort(sentences, (a, b) => a.Length.CompareTo(b.Length));

        //Keys keys = new Keys { a = 1, b = 25 };
        //originalSentence="hello";

        Keys keys = GenerateParameters(PlayerPrefs.GetInt("level"));
        originalSentence = RandomSentence();

        encryptedSentence = GenerateEncryptedSentence(originalSentence, keys, alphabetLength);
        GenerateUI(encryptedSentence);

        hintText.text = $"The letter '{originalSentence[0]}' becomes '{encryptedSentence[0]}' in the encrypted sentence";
    }

    private void Update()
    {
        if (Check(originalSentence) && !win)
        {
            win = true;
            winPanel.SetActive(true);
            winPanel.GetComponentInChildren<Text>().text = encryptedSentence.ToUpper() + "\n" + originalSentence.ToUpper();
            Time.timeScale = 0;
        }
    }
}