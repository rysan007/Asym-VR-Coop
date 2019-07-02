using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonVRUI : MonoBehaviour
{
    public Text helpText;
    public Color halfAlphaBlack;
    public Color noAlphaBlack;

    void Start()
    {
        //Display

        helpText.text = "Move: WASD \n" +
                        "Move Fast: Hold Shift \n" +
                        "Jump: Space (multiple times to fly) \n" +
                        "Grab: E \n" +
                        "Drop: R \n" +
                        "Look/Rotate: Hold Right Click \n" +
                        "Exit: Esc ";
    }

}
