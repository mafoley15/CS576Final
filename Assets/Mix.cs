using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Mix : MonoBehaviour
{
    private Animator animation_controller;
    public Text gradeUI;
    private string mixingGrade;
    private int count;
    private int goal;
    public Material purple;
    public Material blue;
    public Material green;
    public Material orange_yellow;
    public Material clear;
    public Material black;
    public bool boiledOver;
    public bool tooSlow;

    public Text directionsText;
    private string directions;

    public Material white;
        GameObject liquid;
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        count = 0;
        goal = 5;
        liquid = GameObject.Find("Erlenmeyer_flask3");
        liquid.GetComponent<Renderer>().material = blue;
        //Lab.labStep = 11; //remove later
        boiledOver = false;
        if(Lab.labStep == 6){
            directions = "Tap the flask to mix the solution.";
        }else if(Lab.labStep == 9){
            directions = "Tap the flask to mix the solution. Mix slowly otherwise the reaction will boil over!";
        }else{
            directions = "Tap the flask to mix the solution. Mix constantly and fast, otherwise the reaction won't go to completion!";
        }
        directionsText.text = directions;
    }

    // Update is called once per frame
    void Update()
    {
        if(Lab.labStep == 6){
            goal = 3;
            if(count == goal){
                liquid.GetComponent<Renderer>().material = purple;
                finishedMixing();
            }
        } else if(Lab.labStep == 9){
            goal = 3;
            if(count == goal){
                Lab.labStep = 10;
                float timeToBeat = Time.timeSinceLevelLoad;
                Debug.Log(timeToBeat);
                if(timeToBeat < 9){
                    liquid.GetComponent<Renderer>().material = black;
                    boiledOver = true;
                }else{
                    liquid.GetComponent<Renderer>().material = white;
                }
                finishedMixing();
            }
        } else{
            goal = 5;
            float time = Time.timeSinceLevelLoad;
            if(time > 18){
                liquid.GetComponent<Renderer>().material = black;
                tooSlow = true;
                Lab.labStep = 11;
                finishedMixing();
            }
            if(count == goal){
                finishedMixing();
                Lab.labStep = 11;
                count += 1;
                liquid.GetComponent<Renderer>().material = clear;
            }
            if(count == 1){
                liquid.GetComponent<Renderer>().material = blue;
            }
            if(count == 2){
                liquid.GetComponent<Renderer>().material = green;
            }
            if(count == 4){
                liquid.GetComponent<Renderer>().material = orange_yellow;
            }
        }
    }

    void OnMouseDown(){
        animation_controller.SetTrigger("Active");
    }

    void OnEndOfAnimation(){
        count += 1;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        SceneManager.LoadScene("Laboratory Scene");
    }

    public void finishedMixing(){
        float timeToBeat = Time.timeSinceLevelLoad;
        if (timeToBeat < (10/3 * goal)){
            mixingGrade = "A";
            Lab.labGrade += 3;
        } else if(timeToBeat < (15/3 * goal)){
            mixingGrade = "B";
            Lab.labGrade += 2;
        } else if (timeToBeat < (20/3 * goal)){
            mixingGrade = "C";
            Lab.labGrade += 1;
        } else {
            mixingGrade = "F";
        }
        if(boiledOver || tooSlow){
            mixingGrade = "F";
        }
        gradeUI.text = "Grade: " + mixingGrade;
        StartCoroutine(Wait());
    }
}