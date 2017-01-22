using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemsManager : MonoBehaviour {

    [Header("Array of poems to show")]
    public PoemText[] poemTexts;
    private int currentPoemIndex;

    void Start()
    {
        EventManager.StartListening("ReadPoem", ReadPoem);
        currentPoemIndex = 0;
    }

    void ReadPoem()
    {
        // TODO
        // read(PoemText[currentPoemIndex]);
        if(currentPoemIndex < poemTexts.Length)
        {
            currentPoemIndex++;
        }
    }
}