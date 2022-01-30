using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Calendar : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] StoryManager storyManager;

    TMP_Text text;
    string tmp;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        tmp = text.text;
        text.text = "Next Day";
    }

    public void OnPointerExit(PointerEventData data)
    {
        text.text = tmp;
    }
}
