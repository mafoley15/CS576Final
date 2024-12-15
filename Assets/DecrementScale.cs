using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrementScale : MonoBehaviour
{
    public GameObject test_tube;
    private Scale scale_script;
    // Start is called before the first frame update
    void Start()
    {
        scale_script = test_tube.GetComponent<Scale>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(scale_script.unit_in_grams){
            scale_script.mass -= 1.0f;
        }else{
            scale_script.mass -= 0.001f;
        }
    }
}