using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI subtitles;
    [SerializeField]
    private ActorGroup[] actors;
    private AudioSource asource;
    public int nextGroup = 0;
    private Camera curCamera;

 
    void Start()
    {
        asource = this.GetComponent<AudioSource>();
        if (actors.Length > 0)
            playClip(nextGroup);
    }

    // Update is called once per frame
    void Update()
    {
        if (!asource.isPlaying )
            playClip(nextGroup);
    }

    void playClip(int groupIndex) 
    {
        if (groupIndex == -2)
        {
            LevelManager.ExitGame();
            return;
        }
        if (groupIndex == -1)
        {
            LevelManager.NextLevel();
            return;
        }

        

        ActorGroup curGroup = actors[groupIndex];
        ActorNode curNode = curGroup.node[curGroup.iterator];

        if (curNode.cutsceneEvent != null)
            curNode.cutsceneEvent.startEvent();

        if (curCamera != null)
            curCamera.enabled = false;
        curNode.camera.enabled = true;
        curCamera = curNode.camera;

        subtitles.SetText(curNode.subtext);

        asource.PlayOneShot(curNode.audioClip);
        ++curGroup.iterator;
        nextGroup = curNode.nextGroup;

        
    }
}

[Serializable]
class ActorGroup 
{
    //public AudioClip[] audioClips;
    public ActorNode[] node;
    public int iterator = 0;
}

[Serializable]
class ActorNode 
{
    public AudioClip audioClip;
    public int nextGroup;
    public Camera camera;
    public string subtext;
    public CutsceneEvent cutsceneEvent;
}
