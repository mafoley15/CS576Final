using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pour : MonoBehaviour
{
    bool beaker_filled;
    GameObject graduated_cylinder;

    GameObject graduated_cylinder_solution;
    GameObject beaker;
    GameObject beaker_liquid;
    float shifted_scale;
    bool beaker_placed;
    Vector3 beaker_position;
    int num_goals;
    public Text gradeUI;
    string pouringGrade;

    public Material beakerSolutionPurple;
    public Material beakerSolutionWhite;

    private GameObject particles;

    public Text directionsText;
    private string directions;
    // Start is called before the first frame update
    void Start()
    {
        graduated_cylinder = GameObject.Find("Graduated_Cylinder");
        graduated_cylinder_solution = GameObject.Find("Graduated_Cylinder_water");
        beaker = GameObject.Find("Beaker");
        beaker_liquid = GameObject.Find("BeakerSolution");
        shifted_scale = 0.0f;
        beaker_liquid.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        beaker_placed = false;
        beaker_filled = false;
        num_goals = 3;
        particles = GameObject.Find("Particle System");
        if(Lab.labStep == 5){
            beaker_liquid.GetComponent<Renderer>().material = beakerSolutionPurple;
            particles.GetComponent<Renderer>().material = beakerSolutionPurple;
            graduated_cylinder_solution.GetComponent<Renderer>().material = beakerSolutionPurple;
            directions = "Dissolve a small amount of potassium permanganate into water. Use the arrow keys to move the flask to pour into the beaker.";
        } else{
            beaker_liquid.GetComponent<Renderer>().material = beakerSolutionWhite;
            particles.GetComponent<Renderer>().material = beakerSolutionWhite;
            graduated_cylinder_solution.GetComponent<Renderer>().material = beakerSolutionWhite;
            directions = "Dissolve the sugar and sodium hydroxide in the water. Use the arrow keys to move the flask to pour into the beaker.";
        }
        directionsText.text = directions;
    }

    // Update is called once per frame
    void Update()
    {
        if(!beaker_placed){
            beaker_position = new Vector3(Random.Range(-3.0f, 3.0f), 1.95f, Random.Range(-1.0f, -2.0f));
            beaker.GetComponent<Transform>().position = beaker_position;
            beaker_placed = true;
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            Move("up");
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            Move("down");
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            Move("right");
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            Move("left");
        }
        if(LiquidReachesBeaker(beaker_position, graduated_cylinder.GetComponent<Transform>().position)){
            if(!beaker_filled){
                shifted_scale += 0.0001f;
                beaker_liquid.transform.localScale = new Vector3(1.0f, shifted_scale, 1.0f);
            }
        }
        if(beaker_liquid.transform.localScale.y >= 0.15f){
            beaker_liquid.transform.localScale = new Vector3(1.0f, 0.15f, 1.0f);
            beaker_filled = true;
            num_goals -= 1;
        }else if(beaker_liquid.transform.localScale.y >= 0.05f && num_goals == 3){
            beaker_placed = false;
            num_goals -= 1;
        }else if(beaker_liquid.transform.localScale.y >= 0.1f && num_goals == 2){
            beaker_placed = false;
            num_goals -= 1;
        }
        if(num_goals == 0){
            finishedPouring();
            num_goals -= 1;
        }
    }

    void Move(string direction){
        Vector3 position = graduated_cylinder.GetComponent<Transform>().position;
        switch (direction){
            case "up":
                position.z += 0.005f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
            case "down":
                position.z -= 0.005f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
            case "right":
                position.x += 0.007f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
            case "left":
                position.x -= 0.007f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
        }
    }

    bool LiquidReachesBeaker(Vector3 beaker_pos, Vector3 cur_pos){
        if((cur_pos.x >= beaker_pos.x-2.86f && cur_pos.x <= beaker_pos.x-2.0f) && (cur_pos.z >= beaker_pos.z) && (cur_pos.z <= beaker_pos.z + 1.0f)){
            return true;
        }
        return false;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        SceneManager.LoadScene("Mix");
    }

    public void finishedPouring(){
        particles.SetActive(false);
        float timeToBeat = Time.timeSinceLevelLoad;
        if (timeToBeat < 10){
            pouringGrade = "A";
        } else if(timeToBeat < 15){
            pouringGrade = "B";
        } else if (timeToBeat < 20){
            pouringGrade = "C";
        } else {
            pouringGrade = "F";
        }
        gradeUI.text = "Grade: " + pouringGrade;
        if(Lab.labStep == 5){
            Lab.labStep = 6;
            StartCoroutine(Wait());
        } else{
            Lab.labStep = 9;
            StartCoroutine(Wait());
        }
    }
}
