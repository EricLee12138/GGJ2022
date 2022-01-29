using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public int DaySurvived = 0;

    public int EvilPoint = 0;
    public int KindPoint = 100;

    public bool SpeicalEventHappened = false;
    public bool SpeicalEventDone = false;

    public string Decision = "Should I do A or do B";
    public string ChoiceA = "A";
    public string ChoiceB = "B";

    public void EndToday()
    {
        if (SpeicalEventHappened && !SpeicalEventDone) // Special event not done, can't end today
        {
            DeclineEndToday();
            return;
        }

        // End today

    }

    void DeclineEndToday()
    {
        Debug.LogFormat("You still haven't made your decision");
    }

}
