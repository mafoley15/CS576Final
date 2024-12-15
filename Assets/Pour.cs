using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pour : MonoBehaviour
{
    bool beaker_filled;
    GameObject graduated_cylinder;
    GameObject beaker;
    GameObject beaker_liquid;
    float shifted_scale;
    bool beaker_placed;
    Vector3 beaker_position;
    int num_goals;
    public Text gradeUI;
    string pouringGrade;
    // Start is called before the first frame update
    void Start()
    {
        graduated_cylinder = GameObject.Find("Graduated_Cylinder");
        beaker = GameObject.Find("Beaker");
        beaker_liquid = GameObject.Find("Beakerwater");
        shifted_scale = 0.0f;
        beaker_liquid.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        beaker_placed = false;
        beaker_filled = false;
        num_goals = 3;
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
                shifted_scale += 0.001f;
                beaker_liquid.transform.localScale = new Vector3(1.0f, shifted_scale, 1.0f);
            }
        }
        if(beaker_liquid.transform.localScale.y >= 1.0f){
            beaker_liquid.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            beaker_filled = true;
            num_goals -= 1;
        }else if(beaker_liquid.transform.localScale.y >= 0.3f && num_goals == 3){
            beaker_placed = false;
            num_goals -= 1;
        }else if(beaker_liquid.transform.localScale.y >= 0.6f && num_goals == 2){
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
                position.z += 0.001f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
            case "down":
                position.z -= 0.001f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
            case "right":
                position.x += 0.001f;
                graduated_cylinder.GetComponent<Transform>().position = position;
                break;
            case "left":
                position.x -= 0.001f;
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

    public void finishedPouring(){
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
    }
}
