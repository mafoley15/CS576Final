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
        GameObject liquid;
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        count = 0;
        goal = 5;
        liquid = GameObject.Find("Erlenmeyer_flask3");
        liquid.GetComponent<Renderer>().material = blue;
        Lab.labStep = 6; //remove later
    }

    // Update is called once per frame
    void Update()
    {
        if(Lab.labStep == 6){
            goal = 3;
            if(count == goal){
                finishedMixing();
                count += 1;
                liquid.GetComponent<Renderer>().material = purple;
            }
        }
        //Debug.Log(count);
        // if(count == goal){
        //     finishedMixing();
        //     count += 1;
        //     liquid.GetComponent<Renderer>().material = clear;
        // }
        // if(count == 1){
        //     liquid.GetComponent<Renderer>().material = blue;
        // }
        // if(count == 2){
        //     liquid.GetComponent<Renderer>().material = green;
        // }
        // if(count == 4){
        //     liquid.GetComponent<Renderer>().material = orange_yellow;
        // }
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
        } else if(timeToBeat < (15/3 * goal)){
            mixingGrade = "B";
        } else if (timeToBeat < (20/3 * goal)){
            mixingGrade = "C";
        } else {
            mixingGrade = "F";
        }
        gradeUI.text = "Grade: " + mixingGrade;
        if(Lab.labStep == 6){
            StartCoroutine(Wait());
        }
    }
}