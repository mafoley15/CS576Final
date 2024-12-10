using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mix : MonoBehaviour
{
    private Animator animation_controller;
    public Text gradeUI;

    string mixingGrade;
    int count;
    bool clicked;
    int goal;
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        count = 0;
        clicked = false;
        goal = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);
        if(count == goal){
            finishedMixing();
            count += 1;
        }
    }

    void OnMouseDown(){
        animation_controller.SetTrigger("Active");
    }

    void OnEndOfAnimation(){
        count += 1;
    }

    // based off of Adam's finishedCleaning method
    public void finishedMixing(){
        float timeToBeat = Time.timeSinceLevelLoad;
        if (timeToBeat < 10){
            mixingGrade = "A";
        } else if(timeToBeat < 15){
            mixingGrade = "B";
        } else if (timeToBeat < 20){
            mixingGrade = "C";
        } else {
            mixingGrade = "F";
        }
        gradeUI.text = "Grade: " + mixingGrade;
    }
}