using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Camera menuCam, playerCam;

    public GameObject mainMenu;

    public GameObject instructions;

    public Button enter;

    public uint t = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PauseManager.Instance.main)
        {
            Begin();
            return;
        }
        Time.timeScale = 0;
        playerCam.gameObject.SetActive(false);
        menuCam.gameObject.SetActive(true);
        PauseManager.Instance.main = false;
        mainMenu.SetActive(true);
        instructions.SetActive(false);
        enter.onClick.AddListener(delegate {Begin();});
    }

    void Begin()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        menuCam.gameObject.SetActive(false);
        instructions.SetActive(true);
        playerCam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeSelf)
        {
            t += 1;
            if (t % 5 == 0) menuCam.gameObject.transform.Rotate(0, 0.25f, 0);
        }
    }
}
