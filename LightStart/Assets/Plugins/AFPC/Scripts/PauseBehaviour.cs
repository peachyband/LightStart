using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PauseBehaviour : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool canPause = true;

    [SerializeField]
    private Volume vol;
    private DepthOfField dof;

    [SerializeField]
    private AudioSource bgm;

    [SerializeField]
    private RectTransform pauseMenu;


    private void Awake()
    {
        canPause = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (vol != null)
            vol.profile.TryGet<DepthOfField>(out dof);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        dof.active = !dof.active;
        
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            if (bgm != null)
                bgm.Pause();
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            if(bgm != null)
                bgm.UnPause();
        }

        if (pauseMenu != null)
            pauseMenu.gameObject.SetActive(isPaused);


    }

    public void SetCanPause(System.Boolean val) 
    {
        canPause = val;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}


