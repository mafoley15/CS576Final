using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BurnerSwitch : MonoBehaviour
{
    public GameObject fire;
    public TMP_Text instructions;

    void Start()
    {
        fire.SetActive(false);
    }
   
    void OnMouseDown()
    {
        fire.SetActive(true);
        instructions.text = "Now pick a salt on the left to test on the flame";
    }
}
