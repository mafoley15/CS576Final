using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public Text current_mass;
    public float mass;
    // Start is called before the first frame update
    void Start()
    {
        current_mass.text = "Mass: 0.015 g";
    }

    // Update is called once per frame
    void Update()
    {
        //maybe grade can be based on both correctness and timing?
    }

    void buttonClicked(){
        // set display to 0
        current_mass.text = "Mass: 0.000 g";
    }
}
