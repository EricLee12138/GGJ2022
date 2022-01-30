using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController : MonoBehaviour
{
   public GameObject hand;

   private void Update() {//hand at cursor
      Vector3 mousePos;
      mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      {
         Debug.Log(mousePos.x);
         Debug.Log(mousePos.y);
      }
      mousePos.z += 10;
      this.transform.position = mousePos;
   }
}
