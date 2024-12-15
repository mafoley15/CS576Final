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

    public void OnClick(){
        //scale_script.current_mass;
    }
}
