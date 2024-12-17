using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseMenu;

    public Button resume, exit;

    public static PauseManager Instance;

    public bool main = true;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(delegate {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();});
        resume.onClick.AddListener(delegate {Resume();});
    }

    void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
