using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlameManager : MonoBehaviour
{
    private string salt;
    public Rigidbody wire;
    public GameObject fire;
    public List<string> salts;
    public TMP_Text instructions;

    public Button done, exit;

    private bool playing;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        exit.gameObject.SetActive(false);
        salts = new List<string>();
        salt = "None";
        done.onClick.AddListener(delegate {
            playing = false;
            string grade = "Grade: ";
            if (salts.Count == 4)
                grade += "A";
            else if (salts.Count == 3)
                grade += "B";
            else if (salts.Count == 2)
                grade += "C";
            else if (salts.Count == 1)
                grade += "D";
            else
                grade += "F";

            instructions.text = grade;
            exit.gameObject.SetActive(true);
            Lab.labStep = 13;
        });

        exit.onClick.AddListener(delegate {SceneManager.LoadScene("Laboratory Scene");});
    }

    // Update is called once per frame
    void Update()
    {
        if (playing) {
        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is pressed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create ray from mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Check if ray hits anything
            {
                GameObject clickedObject = hit.collider.gameObject; // Get the clicked object

                string n = clickedObject.name;

                if (n == "CaCl" || n == "FeCl3" || n == "CuCl2" || n == "KCl") 
                {
                    if (salts.Contains(n)) 
                    {
                        instructions.text = "You already tested that salt!";
                        return;
                    }
                    salt = n;
                    instructions.text = "Bring the wire to the flame to test the color!";
                }

                Debug.Log(salt);
            }
        }

        if (salt != "None")
        {
            wire.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, .9f));
        }
        else
        {
            wire.position = new Vector3(-4.2f, 1f, 11.5f);
        }
        }
    }

    void OnTriggerEnter(Collider col)
        {
            Debug.Log(salt);
            if (col.gameObject.name == "Fire" && playing)
            {
                switch (salt)
                {
                        case "CaCl":
                            fire.GetComponent<ParticleSystem>().startColor = new Color(.84f, .33f, .1f);
                            fire.GetComponent<Renderer>().material.color = new Color(.84f, .33f, .1f);
                            break;

                        case "FeCl3":
                            fire.GetComponent<ParticleSystem>().startColor = new Color(.84f, .63f, .3f);
                            fire.GetComponent<Renderer>().material.color = new Color(.84f, .63f, .3f);
                            break;

                        case "CuCl2":
                            fire.GetComponent<ParticleSystem>().startColor = new Color(.2f, .72f, .63f);
                            fire.GetComponent<Renderer>().material.color = new Color(.2f, .72f, .63f);
                            break;

                        case "KCl":
                            fire.GetComponent<ParticleSystem>().startColor = new Color(.8f, .4f, .86f);
                            fire.GetComponent<Renderer>().material.color = new Color(.8f, .4f, .86f);
                            break;
                }
                if (!salts.Contains(salt)) salts.Add(salt);
                if (salts.Count != 4)
                    instructions.text = "Now try a different salt!";
                else
                    instructions.text = "Well done! You are a master of colorful flames!";
            }
        }
}
