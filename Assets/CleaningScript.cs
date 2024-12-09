using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleaningScript : MonoBehaviour
{
    public GameObject beakerToClean;

    public GameObject cylinder1;
    public GameObject cylinder2;
    public Animator beakerAnimator;
    public Animator cylinder1Animator;
    public Animator cylinder2Animator;

    private Camera camera;
    public Material cleanGlass;

    private int timeCleanedBeaker;
    private int timeCleanedCylinder1;
    private int timeCleanedCylinder2;

    private bool cleaningBeaker;
    private bool cleaningCylinder1;
    private bool cleaningCylinder2;

    private string cleaningGrade;
    public Texture2D cursorTexture;


    public Image progressBarFill; // Drag the ProgressBarFill image here in the Inspector
    public float maxValue = 100f; // Maximum value of the progress
    public float currentValue = 0f; // Current value of the progress

    public Text gradeUI;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        timeCleanedBeaker = 0;
        cleaningBeaker = true;
        cleaningCylinder1 = false;
        cleaningCylinder2 = false;
        timeCleanedCylinder1 = 0;
        timeCleanedCylinder2 = 0;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(0)){
            DetectObjectWithRaycast();
        } 
        currentValue = (timeCleanedBeaker + timeCleanedCylinder1 + timeCleanedCylinder2) / 3;

        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        progressBarFill.fillAmount = currentValue / maxValue;

        Debug.Log(Time.timeSinceLevelLoad);
        if(cleaningBeaker && timeCleanedBeaker >= 100){
            beakerAnimator.SetBool("moveDown", true);
            beakerToClean.GetComponent<MeshRenderer> ().material = cleanGlass;
            cylinder1Animator.SetBool("moveUp", true);
            cleaningBeaker = false;
            cleaningCylinder1 = true;
        } else if(cleaningCylinder1 && timeCleanedCylinder1 >= 100){
            cylinder1Animator.SetBool("moveDown", true);
            cylinder1.GetComponent<MeshRenderer> ().material = cleanGlass;
            cylinder2Animator.SetBool("moveUp", true);
            cleaningCylinder1 = false;
            cleaningCylinder2 = true;
        } else if(cleaningCylinder2 && timeCleanedCylinder2 >= 100){
            cylinder2Animator.SetBool("moveDown", true);
            cylinder2.GetComponent<MeshRenderer> ().material = cleanGlass;
            cleaningCylinder2 = false;
            finishedCleaning();
        }

       
    }

    public void DetectObjectWithRaycast()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = camera.ScreenPointToRay(mousePosition);
        RaycastHit raycastHit;
        bool hit = Physics.Raycast(ray, out raycastHit);
        if(hit){
            if(cleaningBeaker && raycastHit.transform.name == "BeakerToClean"){
                timeCleanedBeaker ++;
            }

            if(cleaningCylinder1 && raycastHit.transform.name == "CylinderToClean1"){
                timeCleanedCylinder1 ++;
            }

            if(cleaningCylinder2 && raycastHit.transform.name == "CylinderToClean2"){
                timeCleanedCylinder2 ++;
            }
        } else{
            Debug.Log("nothing hit");
        }
    }

    public void finishedCleaning(){
        float timeToBeat = Time.timeSinceLevelLoad;
        if (timeToBeat < 10){
            cleaningGrade = "A";
        } else if(timeToBeat < 15){
            cleaningGrade = "B";
        } else if (timeToBeat < 20){
            cleaningGrade = "C";
        } else {
            cleaningGrade = "F";
        }

        gradeUI.text = "Grade: " + cleaningGrade;
        //Debug.Log(timeToBeat);
    }

}
