using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeasurePour : MonoBehaviour
{
    private bool is_pouring;
    private GameObject beaker_liquid;
    private float shifted_scale;
    private GameObject particles;
    public Text gradeUI;
    private string mixingGrade;

    public float waterAmount;
    public Text amount;

    public float goal;

    public Button doneButton;
    public string scaleGrade;
    public int labStep;
    // Start is called before the first frame update
    void Start()
    {
        is_pouring = false;
        beaker_liquid = GameObject.Find("Beakerwater");
        beaker_liquid.transform.localScale = new Vector3(0, 0, 0);
        shifted_scale = 0.0f;
        waterAmount = 0.0f;
        particles = GameObject.Find("Particle System");
        particles.SetActive(false);
        doneButton.onClick.AddListener(() =>  {
            finishedMeasuring();
         });
        labStep = Lab.labStep;
        if(labStep == 4){
            goal = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(true){
            if(Input.GetKey(KeyCode.RightArrow)){
                transform.Rotate(new Vector3(0, 0, -30 * Time.deltaTime), Space.Self);
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                transform.Rotate(new Vector3(0, 0, 30 * Time.deltaTime), Space.Self);
            }

        }

        float z = GetComponent<Transform>().rotation.z;
        if(z <= -0.7f){
            is_pouring = true;
        } else{
            is_pouring = false;
        }

        if(is_pouring){
            particles.SetActive(true);
            if((z <= -0.65f && z>= -0.77)){
                    // fill beaker
                    shifted_scale += 0.01f;
                    waterAmount = shifted_scale * 100;
                    amount.text = "Amount in Beaker: " + Mathf.Min(800, waterAmount) + " ml";
                    beaker_liquid.transform.localScale = new Vector3(8.0f, shifted_scale, 8.0f);
            }
        } else{
           particles.SetActive(false);
        }

        if(beaker_liquid.transform.localScale.y >= 8.0f){
            beaker_liquid.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
        }


    }
    private IEnumerator Wait()
    {
        // Wait for 1 second


        yield return new WaitForSeconds(2f); // Wait for 3 seconds
        SceneManager.LoadScene("Laboratory Scene");
    }
    public void finishedMeasuring(){
        
        float difference = Mathf.Abs(waterAmount - goal);
        if(difference < 10){
            scaleGrade = "A";
        } else if(difference < 25){
            scaleGrade = "B";
        } else if (difference < 75){
            scaleGrade = "C";
        } else {
            scaleGrade = "F";
        }
        gradeUI.text = "Grade: " + scaleGrade;
        Lab.labStep = labStep + 1;
        StartCoroutine(Wait());
    }
}