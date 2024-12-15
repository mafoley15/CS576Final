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
    public Text gradeUI;
    private string mixingGrade;
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
    }

    // Update is called once per frame
    void Update()
    {
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
            particles.SetActive(true);
            if(z <= -0.65f || z >= 0.65f){
                if((z <= -0.65f && z>= -0.77) || (z <= 0.65f && z>= 0.77)){
                    // fill beaker
                    shifted_scale += 0.01f;
                    beaker_liquid.transform.localScale = new Vector3(8.0f, shifted_scale, 8.0f);
                    is_pouring = false;
                }
            }
            is_pouring = false;
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
        // float timeToBeat = Time.timeSinceLevelLoad;
        // if (timeToBeat < (10/3 * goal)){
        //     mixingGrade = "A";
        // } else if(timeToBeat < (15/3 * goal)){
        //     mixingGrade = "B";
        // } else if (timeToBeat < (20/3 * goal)){
        //     mixingGrade = "C";
        // } else {
        //     mixingGrade = "F";
        // }
        // gradeUI.text = "Grade: " + mixingGrade;
    }
}