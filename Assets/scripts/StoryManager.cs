using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class StoryManager : MonoBehaviour
{
    [SerializeField]
    GameObject NewspaperMask;
    [SerializeField]
    GameObject NewspaperBg;
    [SerializeField]    
    GameObject NewspaperClip;

    public int DayPassed = 0;

    public int EvilPoint = 0;
    public int KindPoint = 100;

    public bool SpeicalEventHappened = false;
    public bool SpeicalEventDone = false;

    public string Decision = "Should I do A or do B";
    public string ChoiceA = "A";
    public string ChoiceB = "B";

    CameraMovement cameraMovement;
    
    void Start()
    {
        cameraMovement = GetComponent<CameraMovement>();
    }

    public void EndToday()
    {
        if (SpeicalEventHappened && !SpeicalEventDone) // Special event not done, can't end today
        {
            DeclineEndToday();
            return;
        }

        // End today
        cameraMovement.DisableMovement();
        DayPassed++;
        NewspaperMask.SetActive(true);
        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperClip.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void StartToday()
    {
        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeOut");

        NewspaperClip.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    void DeclineEndToday()
    {
        Debug.LogFormat("You still haven't made your decision");
    }

}
