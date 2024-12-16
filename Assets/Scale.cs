using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public GameObject game;
    public GameObject grade;
    public Text current_mass;
    public Text measurement;
    public float mass;
    public bool unit_in_grams;
    public Text gradeUI;
    private string scaleGrade;
    // goal needs to be set depending on procedure step
    public float goal;
    // Start is called before the first frame update
    void Start()
    {
        game.SetActive(true);
        grade.SetActive(false);
        mass = 0.0f;
        current_mass.text = "Mass: " + mass.ToString("#.000") + " g";
        unit_in_grams = false;
        measurement.text = "milligrams";
    }

    // Update is called once per frame
    void Update()
    {
        current_mass.text = "Mass: " + mass.ToString("#.000") + " g";
        if(unit_in_grams){
            measurement.text = "grams";
        }else{
            measurement.text = "milligrams";
        }
        if(mass < 0.0f){
            mass = 0.0f;
        }
    }

    public void buttonClicked(){
        if(unit_in_grams){
            unit_in_grams = false;
        }else{
            unit_in_grams = true;
        }
    }

    void OnMouseDown(){
        if(unit_in_grams){
            mass += 1.0f;
        }else{
            mass += 0.001f;
        }
    }

    public void buttonDoneClicked(){
        // give grade
        grade.SetActive(true);
        game.SetActive(false);
        finishedScale();
    }

    public void finishedScale(){
        if(mass == goal){
            scaleGrade = "A";
        } else if((mass >= (goal-(.1*goal))) && (mass <= (goal+(.1*goal)))){
            scaleGrade = "B";
        } else if ((mass >= (goal-(.2*goal))) && (mass <= (goal+(.2*goal)))){
            scaleGrade = "C";
        } else {
            scaleGrade = "F";
        }
        gradeUI.text = "Grade: " + scaleGrade;
    }
}
