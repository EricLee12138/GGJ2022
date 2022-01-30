using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CameraMovement))]
[RequireComponent(typeof(GameFlagManager))]
public class StoryManager : MonoBehaviour
{
    [SerializeField]
    GameObject FirstPlayMask;
    [SerializeField]
    GameObject NewspaperMask;
    [SerializeField]
    GameObject NewspaperBg;

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
    TMP_Text HelpTextHolder;

    [SerializeField]
    GameObject RadioTextBg;
    [SerializeField]
    GameObject Letter;
    [SerializeField]
    GameObject EnvelopeA;
    [SerializeField]
    GameObject EnvelopeB;
    [SerializeField]
    GameObject EnvelopeOpen;

    [SerializeField]
    GameObject EndingObj;
    [SerializeField]
    GameObject EndingMask;
    [SerializeField]
    GameObject EndingEnvelope;
    [SerializeField]
    GameObject EndingEnvelopeOpen;
    [SerializeField]
    GameObject EndingLetter;
    [SerializeField]
    GameObject EndingComment;
    [SerializeField]
    GameObject EndingLetterText;
    [SerializeField]
    GameObject EndingCommentText;

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
        Cursor.visible = false;
        cameraMovement = GetComponent<CameraMovement>();
        gameFlagManager = GetComponent<GameFlagManager>();
        Debug.Assert(MemoTextHolder != null);
        Debug.Assert(LetterTextHolder != null);
        DayPassed = -1;
        SpecialEventHappened = true;
        SpecialEventDone = true;
        EndToday();
    }

    public void EndToday()
    {
        if (!SpecialEventHappened || !SpecialEventDone) // Special event not done, can't end today
        {
            DeclineEndToday();
            return;
        }


        // End today
        cameraMovement.DisableMovement();
        if (DayPassed != -1)
        {
            gameFlagManager.dayPass();
        } 
        
        if (DayPassed == 8)
        {
            // Ending
            EndGame();
            return;
        }

        NewspaperMask.SetActive(true);
        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void EndGame()
    {
        EndingObj.SetActive(true);
        EndingMask.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingEnvelope.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingEnvelopeOpen.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingLetter.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingComment.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingLetterText.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingCommentText.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void StartToday()
    {
        EnvelopeA.gameObject.SetActive(false);
        EnvelopeB.gameObject.SetActive(false);
        EnvelopeOpen.gameObject.SetActive(false);
        Letter.SetActive(false);
        LetterTextHolder.gameObject.SetActive(false);
        ShowNothingHelp();

        if (DayPassed == -1)
        {
            FirstPlayMask.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(WaitUntilAnimationIsName(FirstPlayMask, "FadeOut"));
        }

        // Read data from GameFlagManager
        DayPassed = gameFlagManager.day; // D
        EvilPoint = gameFlagManager.evilPoint;
        SonPoint = gameFlagManager.sonPoint;
        Yes = gameFlagManager.yes;
        No = gameFlagManager.no;
        SpecialEventHappened = gameFlagManager.decisionFrom == "special";
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

        if (DecisionSource == "letter")
        {
            EnvelopeA.gameObject.SetActive(true);
            EnvelopeB.gameObject.SetActive(true);
            MemoTextHolder.gameObject.SetActive(false);
            ChoiceATextHolder.gameObject.SetActive(false);
            ChoiceBTextHolder.gameObject.SetActive(false);
        } else if(DecisionSource == "radio")
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

        StartCoroutine(WaitUntilAnimationIsName(NewspaperMask, "Idle"));
    }

    IEnumerator WaitUntilAnimationIsName(GameObject obj, string name)
    {
        yield return new WaitUntil(() => obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(name));
        obj.SetActive(false);
        cameraMovement.EnableMovement();
    }

    public void ListenToRadio()
    {
        if (RadioText.Length <= 0) 
        {
            ShowRadioHelp();
            return;
        }
        print("radio");
        SpecialEventHappened = true;
        RadioTextBg.SetActive(true);
        RadioTextBg.GetComponent<Animator>().SetTrigger("FadeIn");
        RadioTextHolder.GetComponent<Animator>().SetTrigger("FadeIn");
        StartCoroutine(WaitForSeconds(5f));
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        RadioTextBg.GetComponent<Animator>().SetTrigger("FadeOut");
        RadioTextHolder.GetComponent<Animator>().SetTrigger("FadeOut");
        if (DecisionSource == "radio")
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
        }

        StartCoroutine(WaitUntilAnimationIsName(RadioTextBg, "Idle"));
    }

    public void ReadLetter()
    {
        if (LetterText.Length <= 0) return;
        print("letter");
        SpecialEventHappened = true;
        EnvelopeB.SetActive(false);
        EnvelopeOpen.SetActive(true);
        Letter.SetActive(true);
        LetterTextHolder.gameObject.SetActive(true);
        if (DecisionSource == "letter")
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
        }
    }

    public void Choose(bool yes) {
        if (yes)
        {
            Decision += '\n' + ChoiceA;
        } else
        {
            Decision += '\n' + ChoiceB;
        }
        MemoTextHolder.text = Decision;
        ChoiceATextHolder.gameObject.SetActive(false);
        ChoiceBTextHolder.gameObject.SetActive(false);
        SpecialEventDone = true;
    }

    public void ShowRadioHelp()
    {
        HelpTextHolder.text = "This is my son's old radio. I'm glad it still works...";
    }

    public void ShowPackageHelp()
    {
        HelpTextHolder.text = "Should I just send it...";
    }

    public void ShowDeclineHelp()
    {
        if (!SpecialEventHappened)
        {
            HelpTextHolder.text = "I'm worried about him...";
        } else
        {
            HelpTextHolder.text = "It's tough but I need to decide...";
        }
    }

    public void ShowNothingHelp()
    {
        HelpTextHolder.text = "...";
    }

    void DeclineEndToday()
    {
        ShowDeclineHelp();
        Debug.LogFormat("You still haven't made your decision");
    }

}
