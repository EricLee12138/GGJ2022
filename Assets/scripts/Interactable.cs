using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnClick;

    void OnMouseDown()
    {
        Debug.LogFormat("You clicked {0}", gameObject.name);
        OnClick.Invoke();
    }
}
