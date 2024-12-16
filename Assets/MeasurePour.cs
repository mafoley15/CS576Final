using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasurePour : MonoBehaviour
{
    private bool is_pouring;
    private bool beaker_filled;
    private GameObject beaker_liquid;
    private float shifted_scale;
    private GameObject particles;
    public Text amountText;
    public Text gradeUI;
    private string mixingGrade;
    public GameObject game;
    public GameObject grade;
    private int amount;
    private int count;
    public int goal;
    // Start is called before the first frame update
    void Start()
    {
        is_pouring = false;
        beaker_filled = false;
        beaker_liquid = GameObject.Find("Beakerwater");
        beaker_liquid.transform.localScale = new Vector3(0, 0, 0);
        shifted_scale = 0.0f;
        particles = GameObject.Find("Particle System");
        particles.SetActive(false);
        transform.Rotate(new Vector3(0, 0, Random.Range(0.0f, 360.0f)), Space.Self);
        grade.SetActive(false);
        game.SetActive(true);
        amount = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count % 50 == 0){
            amount = count / 5;
            amountText.text = "Amount in Beaker: " + amount.ToString() + " ml";
        }
        if(!is_pouring){
            if(Input.GetKey(KeyCode.RightArrow)){
                transform.Rotate(new Vector3(0, 0, -30 * Time.deltaTime), Space.Self);
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                transform.Rotate(new Vector3(0, 0, 30 * Time.deltaTime), Space.Self);
            }
            if(Input.GetKey(KeyCode.Space)){
                is_pouring = true;
            }
        }
        if(is_pouring && !beaker_filled){
            float z = GetComponent<Transform>().rotation.z;
            //Debug.Log(z);
            if((z <= -0.65f && z>= -0.80) || (z >= 0.65f && z<= 0.80)){
                // fill beaker
                particles.SetActive(true);
                shifted_scale += 0.001f;
                beaker_liquid.transform.localScale = new Vector3(8.0f, shifted_scale, 8.0f);
                is_pouring = false;
                count += 1;
            }
            is_pouring = false;
            particles.SetActive(false);
        }
        if(beaker_liquid.transform.localScale.y >= 8.0f){
            particles.SetActive(false);
            beaker_liquid.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
        }
        if(beaker_liquid.transform.localScale.y == 8.0f){
            beaker_filled = true;
        }
    }

    public void finishedMeasuring(){
        beaker_filled = true;
        if (amount == goal){
            mixingGrade = "A";
        } else if((amount >= (goal-(.2*goal))) && (amount <= (goal+(.2*goal)))){
            mixingGrade = "B";
        } else if ((amount >= (goal-(.4*goal))) && (amount <= (goal+(.4*goal)))){
            mixingGrade = "C";
        } else {
            mixingGrade = "F";
        }
        gradeUI.text = "Grade: " + mixingGrade;
        grade.SetActive(true);
        game.SetActive(false);
    }
}