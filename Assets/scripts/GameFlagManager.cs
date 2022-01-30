using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class GameData {
    public string radioText;
    public string letterText;
    public int[] yes;
    public int[] no;
    public string conditionSpecialEvent;
    public string conditionRadio;
    public string specialEventFlag;//special event flag
    public string specialEventBody;
}

[System.Serializable]
public class DataList {
    public GameData[] GameData = {};
}

public class GameFlagManager : MonoBehaviour {
    private void Start() {
        ///initialize
        readGameData(0);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed primary button.");
            makeDecision(true);
        }
    }

    private void OnDrawGizmos() {
        ///debug
        readGameData(0);
    }

    private void readGameData (int day){
        //read and parse json data according to date(as row)
        string path;
        string jsonString;
        path = Application.dataPath + "/scripts/gameData.json";
        jsonString = File.ReadAllText (path); 
        Debug.Log("Game data path :" + path);
        DataList gameDatas = JsonUtility.FromJson<DataList> (jsonString);
        print(gameDatas.GameData.Length);
        GameData current = gameDatas.GameData[day];
        this.letterText = current.letterText;
        this.yes = current.yes;
        this.no = current.no;
        
        if (current.conditionSpecialEvent != "") {
            if (hasFlag(current.conditionSpecialEvent)) {
                if (current.specialEventFlag != null) {
                    this.specialEventFlag = current.specialEventFlag;
                }
                this.specialEventBody = current.specialEventBody;
            }
        }
        else if (current.specialEventBody != "") {
            if (current.specialEventFlag != null) {
                this.specialEventFlag = current.specialEventFlag;
            }            
            this.specialEventBody = current.specialEventBody;
        }

        if (current.conditionRadio != "") {
            if (hasFlag(current.conditionRadio)) {
                this.radioText = current.radioText;
            }
        }
        else if (current.radioText != "") {
            this.radioText = current.radioText;
        }

        this.debug();
    }
    public void makeDecision(bool decision){
        ///Input: current date
        ///Edit evilPoint & sonPoint base on decision, add gameFlag if applicable
        this.evilPoint = this.evilPoint + (this.decision ? this.yes[0] : this.no[0]);
        this.sonPoint = this.sonPoint + (this.decision ? this.yes[1] : this.no[1]);
        if (this.decision) {
            if(specialEventFlag!=null){
                this.gameFlags.Add(this.specialEventFlag);
            }
        }
    }

    public bool hasFlag(string gameflag) {
        ///look for a gameflag
        foreach (string flag in this.gameFlags) {
            if (gameflag == flag)
                return true;
        }
        return false;
    }

    //constructor
    public GameFlagManager(int day, int evilPoint, int sonPoint, int[] yes, int[] no, string radioText, string letterText, string gameText, List<string> gameFlags, string specialEventFlag,string specialEventBody, bool decision) {
        this.day = day;
        this.evilPoint = evilPoint;
        this.sonPoint = sonPoint;
        this.yes = yes;
        this.no = no;
        this.radioText = radioText;
        this.letterText = letterText;
        this.gameText = gameText;
        this.gameFlags = gameFlags;
        this.specialEventFlag = specialEventFlag;
        this.specialEventBody = specialEventBody;
        this.decision = decision;
    }

    public void debug() {
        ///Print level info in a json format
        string log = JsonUtility.ToJson(this);
        Debug.Log(log);
    }
    public void dayPass() {
        ///One day passed, update level info
        this.day++;
        readGameData(day);
    }

    //init
    public int day = 0;//Player needs to make decision to pass a day
    public int evilPoint = 0;
    public int sonPoint = 0;
    public int[] yes = null;
    public int[] no = null;
    public string radioText = "";//First Priority, then play random songs
    public string letterText = "";
    public string gameText = "";//Show on notebook, player make decision
    public List<string> gameFlags = null;//A list of game flags
    public string specialEventFlag = "";//Flag that will add to gameFlags
    public string specialEventBody = "";//Event text
    public bool decision = false;
}
