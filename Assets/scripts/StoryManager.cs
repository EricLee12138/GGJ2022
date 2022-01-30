using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CameraMovement))]
[RequireComponent(typeof(GameFlagManager))]
public class StoryManager : MonoBehaviour
{
    [SerializeField]
    GameObject NewspaperMask;
    [SerializeField]
    GameObject NewspaperBg;
    [SerializeField]    
    GameObject NewspaperClip;

    [SerializeField]
    TMP_Text DateHolder;
    [SerializeField]
    TMP_Text MemoTextHolder;
    [SerializeField]
    TMP_Text LetterTextHolder;
    [SerializeField]
    TMP_Text ChoiceATextHolder;
    [SerializeField]
    TMP_Text ChoiceBTextHolder;
    [SerializeField]
    TMP_Text RadioTextHolder;

    [SerializeField]
    GameObject RadioTextBg;
    [SerializeField]
    GameObject Letter;

    public int DayPassed = -1;

    public int EvilPoint = 0;
    public int SonPoint = 100;

    public int[] Yes;
    public int[] No;

    public bool SpecialEventHappened = false;
    public bool SpecialEventDone = false;
    
    public string DecisionSource = "Radio"; // Radio, Letter, SpecialEvent
    public string Decision = "Should I do A or do B";
    public string ChoiceA = "A";
    public string ChoiceB = "B";
    
    public string RadioText = "It is announced that";
    public string LetterText = "Dear mom";
    public string EventText = "Today I met...";

    CameraMovement cameraMovement;
    GameFlagManager gameFlagManager;
    
    void Start()
    {
        cameraMovement = GetComponent<CameraMovement>();
        gameFlagManager = GetComponent<GameFlagManager>();
        Debug.Assert(MemoTextHolder != null);
        Debug.Assert(LetterTextHolder != null);
        EndToday();
    }

    public void EndToday()
    {
        // if (SpecialEventHappened && !SpecialEventDone) // Special event not done, can't end today
        // {
        //     DeclineEndToday();
        //     return;
        // }

        // End today
        cameraMovement.DisableMovement();
        gameFlagManager.dayPass();
        NewspaperMask.SetActive(true);
        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperClip.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void StartToday()
    {
        // Read data from GameFlagManager
        DayPassed = gameFlagManager.day; // D
        EvilPoint = gameFlagManager.evilPoint;
        SonPoint = gameFlagManager.sonPoint;
        Yes = gameFlagManager.yes;
        No = gameFlagManager.no;
        SpecialEventHappened = gameFlagManager.specialEventBody.Length > 0;
        SpecialEventDone = false;
        RadioText = gameFlagManager.radioText;
        LetterText = gameFlagManager.letterText;
        EventText = gameFlagManager.specialEventBody;
        ChoiceA = "A";      
        ChoiceB = "B";
        DecisionSource = gameFlagManager.decisionFrom;
        Decision = gameFlagManager.gameText;

        DateHolder.text = (7 + DayPassed).ToString();
        MemoTextHolder.text = Decision;
        LetterTextHolder.text = LetterText;
        ChoiceATextHolder.text = ChoiceA;
        ChoiceBTextHolder.text = ChoiceB;
        RadioTextHolder.text =  RadioText;

        if (DecisionSource == "letter" || DecisionSource == "radio")
        {
            MemoTextHolder.gameObject.SetActive(false);
            ChoiceATextHolder.gameObject.SetActive(false);
            ChoiceBTextHolder.gameObject.SetActive(false);
        } else if (DecisionSource == "special")
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
        } else {
            Debug.LogError("Something is definitely wrong");
        }

        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperClip.GetComponent<Animator>().SetTrigger("FadeOut");

        StartCoroutine(WaitUntilAnimationDone(NewspaperMask));
    }

    IEnumerator WaitUntilAnimationDone(GameObject obj)
    {
        yield return new WaitUntil(() => obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        obj.SetActive(false);
        cameraMovement.EnableMovement();
    }

    public void ListenToRadio()
    {
        if (RadioText.Length <= 0) return;
        print("radio");
        RadioTextBg.SetActive(true);
        RadioTextBg.GetComponent<Animator>().SetTrigger("FadeIn");
        RadioTextHolder.GetComponent<Animator>().SetTrigger("FadeIn");
        StartCoroutine(WaitForSeconds(5f));
        if (DecisionSource == "radio")
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        RadioTextBg.GetComponent<Animator>().SetTrigger("FadeOut");
        RadioTextHolder.GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine(WaitUntilAnimationDone(RadioTextBg));
    }

    public void ReadLetter()
    {
        if (LetterText.Length <= 0) return;
        print("letter");
        Letter.SetActive(true);
        if (DecisionSource == "letter")
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
        }
    }

    void DeclineEndToday()
    {
        Debug.LogFormat("You still haven't made your decision");
    }

}
