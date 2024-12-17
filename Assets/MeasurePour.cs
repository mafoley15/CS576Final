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
    public Text amountText;
    public Text gradeUI;
    private string mixingGrade;

    public float waterAmount;
    public Text amount;

    public float goal;

    public Button doneButton;
    public string scaleGrade;
    public int labStep;

    public Text directionsText;
    private string directions;

    public AudioSource audioSource;
    public AudioClip pouringSound;
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
            directions = "Measure out 500 ml of distilled water. Tip over the flask with arrow keys and fill the beaker the correct amount. Raise the flask to stop pouring. When finished press the done button.";
        } else{
            goal = 750;
            directions = "Measure out 750 ml distilled water. Tip over the flask with arrow keys and fill the beaker the correct amount. Raise the flask to stop pouring. When finished press the done button.";
        }
        directionsText.text = directions;
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
            particles.SetActive(false);
        }

        if(is_pouring){
            particles.SetActive(true);
            if((z <= -0.65f && z>= -0.77)){
                    // fill beaker
                if(!audioSource.isPlaying){
                    audioSource.PlayOneShot(pouringSound);
                }
                    shifted_scale += 0.001f;
                    waterAmount = (int)(shifted_scale * 100);
                    amount.text = "Amount in Beaker: " + Mathf.Min(800, waterAmount) + " ml";
                    beaker_liquid.transform.localScale = new Vector3(8.0f, shifted_scale, 8.0f);
            }
        } else{
           particles.SetActive(false);
           audioSource.Stop();
        }

        if(beaker_liquid.transform.localScale.y >= 8.0f){
            beaker_liquid.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
        }


    }
    private IEnumerator Wait()
    {


        yield return new WaitForSeconds(2f); // Wait for 3 seconds
        SceneManager.LoadScene("Laboratory Scene");
    }
    public void finishedMeasuring(){
        
        float difference = Mathf.Abs(waterAmount - goal);
        if(difference < 10){
            scaleGrade = "A";
            Lab.labGrade += 3;
        } else if(difference < 25){
            scaleGrade = "B";
            Lab.labGrade += 2;
        } else if (difference < 75){
            scaleGrade = "C";
            Lab.labGrade += 1;
        } else {
            scaleGrade = "F";
        }
        gradeUI.text = "Grade: " + scaleGrade;
        Lab.labStep = labStep + 1;
        StartCoroutine(Wait());
    }
}