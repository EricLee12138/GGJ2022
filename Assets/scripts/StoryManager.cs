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
    TMP_Text NewspaperContinue;
    [SerializeField]
    TMP_Text NewspaperText;
    [SerializeField]
    TMP_Text NewspaperTitle;

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
    GameObject Package;

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
    TMP_Text EndingLetterText;
    [SerializeField]
    TMP_Text EndingCommentText;
    [SerializeField]
    GameObject Credits;

    [SerializeField]
    throwCoin coin;

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
    
    public string NewsText = "Today I met...";
    public string NewsTitle = "Today I met...";

    public GameObject radio;
    private soundManager _soundManager;

    CameraMovement cameraMovement;
    GameFlagManager gameFlagManager;
    
    void Start()
    {
        Cursor.visible = false;
        _soundManager = radio.GetComponent<soundManager>();
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
        if (DayPassed != -1 && DayPassed != 7)
        {
            _soundManager.playNewsSound();
            gameFlagManager.dayPass();
            NewsTitle = gameFlagManager.newsTitle;
            NewsText = gameFlagManager.newsText;

            
        } else if (DayPassed == 7)
        {
            NewsTitle = "End of War in Sight";
            NewsText = "The war may soon come to an end as talks of peace treaties are being held between our leaders. Even though we lost many of our soldiers, we hope for the safe return of our fearless troops on the front lines. Well done boys! ";
        } else
        {
            NewsTitle = "War Looms as Tensions Rise";
            NewsText = "War has broken out on the peninsula! The government has ordered all men of age to aid in our war against tyranny and cruelty. We are sending our boys to war but they will come back as heroes. ";
        }
        
        NewspaperText.text = NewsText;
        NewspaperTitle.text = NewsTitle;

        NewspaperMask.SetActive(true);
        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperContinue.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperText.GetComponent<Animator>().SetTrigger("FadeIn");
        NewspaperTitle.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void EndGame()
    {
        cameraMovement.DisableMovement();
        EndingObj.SetActive(true);
        EndingMask.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingEnvelope.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingEnvelopeOpen.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingLetter.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingComment.GetComponent<Animator>().SetTrigger("FadeIn");
        Credits.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingLetterText.GetComponent<Animator>().SetTrigger("FadeIn");
        EndingCommentText.GetComponent<Animator>().SetTrigger("FadeIn");

        EndingLetterText.text = GetComponent<endingBranch>().getSonText();
        print(GetComponent<endingBranch>().getSonText());
        EndingCommentText.text = GetComponent<endingBranch>().getNeighborText();
    }

    public void StartToday()
    {
        if (DayPassed == 7)
        {
            print("end");
            // Ending
            NewspaperMask.GetComponent<Animator>().SetTrigger("FadeOut");
            NewspaperBg.GetComponent<Animator>().SetTrigger("FadeOut");
            NewspaperContinue.GetComponent<Animator>().SetTrigger("FadeOut");
            NewspaperText.GetComponent<Animator>().SetTrigger("FadeOut");
            NewspaperTitle.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(WaitUntilAnimationIsName(NewspaperMask, "Idle"));
            _soundManager.playEnding();
            EndGame();
            return;
        }

        EnvelopeA.gameObject.SetActive(false);
        EnvelopeB.gameObject.SetActive(false);
        EnvelopeOpen.gameObject.SetActive(false);
        Package.SetActive(false);
        Letter.SetActive(false);
        LetterTextHolder.gameObject.SetActive(false);
        coin.transform.position = new Vector3(1000f, 1000f, 0f);
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
        ChoiceA = gameFlagManager.choiceA;      
        ChoiceB = gameFlagManager.choiceB;     
        DecisionSource = gameFlagManager.decisionFrom;
        Decision = gameFlagManager.gameText;

        DateHolder.text = (7 + DayPassed).ToString();
        MemoTextHolder.text = EventText;
        LetterTextHolder.text = LetterText;
        ChoiceATextHolder.text = ChoiceA;
        ChoiceBTextHolder.text = ChoiceB;
        RadioTextHolder.text =  RadioText;

        if (LetterText.Length > 0)
        {
            EnvelopeA.gameObject.SetActive(true);
            EnvelopeB.gameObject.SetActive(true);
        }

        if (EventText.Length > 0)
        {
            MemoTextHolder.gameObject.SetActive(true);
        }

        if (DecisionSource == "letter")
        {
            ChoiceATextHolder.gameObject.SetActive(false);
            ChoiceBTextHolder.gameObject.SetActive(false);
        } else if(DecisionSource == "radio")
        {
            ChoiceATextHolder.gameObject.SetActive(false);
            ChoiceBTextHolder.gameObject.SetActive(false);
        } else if (DecisionSource == "special")
        {
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
        } else {
            Debug.LogError("Something is definitely wrong");
        }

        NewspaperMask.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperBg.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperContinue.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperText.GetComponent<Animator>().SetTrigger("FadeOut");
        NewspaperTitle.GetComponent<Animator>().SetTrigger("FadeOut");
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
        if (DecisionSource == "radio" && !SpecialEventHappened)
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);

            // Append to memo
            MemoTextHolder.text +=  EventText.Length > 0 ? '\n' + Decision : Decision;
            SpecialEventHappened = true;
        }

        StartCoroutine(WaitUntilAnimationIsName(RadioTextBg, "Idle"));
    }

    public void ReadLetter()
    {
        if (LetterText.Length <= 0) return;
        print("letter");
        EnvelopeB.SetActive(false);
        EnvelopeOpen.SetActive(true);
        Letter.SetActive(true);
        LetterTextHolder.gameObject.SetActive(true);
        if (DecisionSource == "letter" && !SpecialEventHappened)
        {
            MemoTextHolder.gameObject.SetActive(true);
            ChoiceATextHolder.gameObject.SetActive(true);
            ChoiceBTextHolder.gameObject.SetActive(true);
            
            // Append to memo
            MemoTextHolder.text +=  EventText.Length > 0 ? '\n' + Decision : Decision;
            SpecialEventHappened = true;
        }
    }

    public void Choose(bool yes) {
        if (yes)
        {
            MemoTextHolder.text += "\n\n" + ChoiceA;
            Package.SetActive(true);
        } else
        {
            MemoTextHolder.text += "\n\n" + ChoiceB;
        }
        ChoiceATextHolder.gameObject.SetActive(false);
        ChoiceBTextHolder.gameObject.SetActive(false);
        SpecialEventDone = true;
        coin.Decide(yes);
        gameFlagManager.makeDecision(yes);
    }

    public void ShowRadioHelp()
    {
        HelpTextHolder.text = "This is my son's old radio. I'm glad it still works...";
    }

    public void ShowPackageHelp()
    {
        HelpTextHolder.text = "Knowing my son will have it is indeed a relief...";
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

    public void ShowLetterHelp()
    {
        HelpTextHolder.text = "It's nice to have something to read during the war...";
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
