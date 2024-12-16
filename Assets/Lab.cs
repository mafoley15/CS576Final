using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lab : MonoBehaviour
{
    // Start is called before the first frame update

    public int labNum; // lab 1 = color chameleon / lab 2 = flame tests & atomic spectra
    public static int labStep; //counter for what step of procedure user is on
    public static int newLabStep;
    public Text instructions;

    public GameObject exclamation; //moving exclamation to help user navigate lab

    public GameObject player;
    public GameObject glassCabinet;

    public GameObject chemicalCabinet;

    public GameObject measuringStation;

    public GameObject labObject;

    public GameObject dissolvingTable;

    public GameObject cleaningStation;



    
    void Start()
    {
        if(labStep == 0){
            labNum = 1; //will change to set according to which lab user selects on main menu
            labStep = 1;
            exclamation.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(labStep == 1){
            Collider playerCollider = player.GetComponent<Collider>();
            Collider glassCabinetCollider = glassCabinet.GetComponent<Collider>();
            instructions.text = "Instructions: Welcome to the lab. To start this lab, collect some beakers from the glass cabinet";
            exclamation.SetActive(true);
            exclamation.transform.position = new Vector3(-1.494f,3.361f, 15.135f);
            if (playerCollider.bounds.Intersects(glassCabinetCollider.bounds))
            {
               instructions.text = "Press E to collect necessary glassware";
                if(Input.GetKey(KeyCode.E)){
                    labStep = 2;
                }
            }
        } else if(labStep == 2){
            instructions.text = "Instructions: Collect the necessary chemicals from the chemical cabinet";
            Collider playerCollider = player.GetComponent<Collider>();
            Collider chemicalCabinetCollider = chemicalCabinet.GetComponent<Collider>();
            exclamation.transform.position = new Vector3(-9.712f,3.361f, 14.124f);
            exclamation.transform.eulerAngles = new Vector3(0f, 90f, 0f);
            if (playerCollider.bounds.Intersects(chemicalCabinetCollider.bounds))
            {
               instructions.text = "Press E to collect necessary chemicals";
                if(Input.GetKey(KeyCode.E)){
                    labStep = 3;
                }
            } 
        } else if (labStep == 3){
            instructions.text = "Instructions: Head to measuring station to measure out necessary amounts of chemicals";
            exclamation.transform.position = new Vector3(-10.018f,1.916f, 9.269f);
            Collider playerCollider = player.GetComponent<Collider>();
            Collider measuringStationCollider = measuringStation.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(measuringStationCollider.bounds))
            {
                instructions.text = "Press E to begin measuring";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Scale");
                }
            }
        } else if (labStep == 4){
            instructions.text = "Instructions: Head to measuring station to measure out necessary amounts of water";
            exclamation.transform.position = new Vector3(-10.018f,1.916f, 9.269f);
            Collider playerCollider = player.GetComponent<Collider>();
            Collider measuringStationCollider = measuringStation.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(measuringStationCollider.bounds))
            {
                instructions.text = "Press E to begin measuring";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Measure");
                }
            }
        } else if (labStep == 5){
            instructions.text = "Instructions: Head to table to dissolve the potassium permanganate into water";
            exclamation.transform.position = new Vector3(-1.896f,1.916f, 7.741f);
            Collider playerCollider = player.GetComponent<Collider>();
            exclamation.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Collider dissolvingStationCollider = dissolvingTable.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(dissolvingStationCollider.bounds))
            {
                instructions.text = "Press E to begin dissolving";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Pour");
                }
            }
        } else if (labStep == 6){
            instructions.text = "Instructions: Head to measuring station to measure out necessary amounts of chemicals";
            exclamation.transform.position = new Vector3(-10.018f,1.916f, 9.269f);
            Collider playerCollider = player.GetComponent<Collider>();
            Collider measuringStationCollider = measuringStation.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(measuringStationCollider.bounds))
            {
                instructions.text = "Press E to begin measuring";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Scale");
                }
            }
        } else if (labStep == 7){
            instructions.text = "Instructions: Head to measuring station to measure out necessary amounts of water";
            exclamation.transform.position = new Vector3(-10.018f,1.916f, 9.269f);
            Collider playerCollider = player.GetComponent<Collider>();
            Collider measuringStationCollider = measuringStation.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(measuringStationCollider.bounds))
            {
                instructions.text = "Press E to begin measuring";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Measure");
                }
            }
        } else if (labStep == 8){
            instructions.text = "Instructions: Head to table to dissolve the compounds into water";
            exclamation.transform.position = new Vector3(-1.896f,1.916f, 7.741f);
            Collider playerCollider = player.GetComponent<Collider>();
            exclamation.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Collider dissolvingStationCollider = dissolvingTable.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(dissolvingStationCollider.bounds))
            {
                instructions.text = "Press E to begin dissolving";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Pour");
                }
            }
        } else if (labStep == 10){
            instructions.text = "Instructions: Head to table to mix two solutions together";
            exclamation.transform.position = new Vector3(-1.896f,1.916f, 7.741f);
            Collider playerCollider = player.GetComponent<Collider>();
            exclamation.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Collider dissolvingStationCollider = dissolvingTable.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(dissolvingStationCollider.bounds))
            {
                instructions.text = "Press E to begin final mixing process";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Mix");
                }
            }
        } else if (labStep == 11){
            instructions.text = "Instructions: Head to sink to clean lab equipment";
            exclamation.transform.position = new Vector3(3.291f,1.916f, 5.481f);
            Collider playerCollider = player.GetComponent<Collider>();
            exclamation.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Collider cleaningStationCollider = cleaningStation.GetComponent<Collider>();
            if (playerCollider.bounds.Intersects(cleaningStationCollider.bounds))
            {
                instructions.text = "Press E to clean lab equipment";
                if(Input.GetKey(KeyCode.E)){
                    SceneManager.LoadScene("Cleaning");
                }
            }
        }
        
    }
}
