using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class GameData
{
    public string newsText;//everyday newspaper, do not contain decision
    public string newsTitle;//everyday newspaper, do not contain decision
    public string gameText;
    public string radioText;
    public string letterText;
    public int[] yes;
    public int[] no;
    public string conditionSpecialEvent;
    public string conditionRadio;
    public string specialEventFlag; //special event flag
    public string specialEventBody;
    public string decisionFrom;//letter, radio, special
    public string choiceA;
    public string choiceB;
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
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Pressed primary button.");
            dayPass();
        }
    }

    private void OnDrawGizmos() {
        ///debug
        // readGameData(0);
    }

    private void readGameData (int day){
        //read and parse json data according to date(as row)
        string path;
        string jsonString;
        path = Application.streamingAssetsPath + "/gameData.json";
        jsonString = File.ReadAllText (path); 
        // Debug.Log("Game data path :" + path);
        DataList gameDatas = JsonUtility.FromJson<DataList> (jsonString);
        // print(gameDatas.GameData.Length);
        GameData current = gameDatas.GameData[day];
        this.letterText = current.letterText;
        this.yes = current.yes;
        this.no = current.no;
        this.decisionFrom = current.decisionFrom;
        this.gameText = current.gameText;
        this.newsTitle = current.newsTitle;
        this.newsText = current.newsText;
        this.choiceA = current.choiceA;
        this.choiceB = current.choiceB;
        
        if (current.conditionSpecialEvent != "") {
            if (hasFlag(current.conditionSpecialEvent)) {
                if (current.specialEventFlag != "") {
                    this.specialEventFlag = current.specialEventFlag;
                    this.specialEventBody = current.specialEventBody;
                }
                else {
                    this.specialEventFlag = "";
                    this.specialEventBody = "";
                }
            }
        }
        else{
            this.specialEventFlag = current.specialEventFlag;
            this.specialEventBody = current.specialEventBody;
        }

        if (current.conditionRadio != "") {
            if (hasFlag(current.conditionRadio)) {
                this.radioText = current.radioText;
            }
            else {
                this.radioText = "";
            }
        }
        else {
            this.radioText = current.radioText;
        }

        this.debug();
    }
    public void makeDecision(bool decision){
        ///Input: current date
        ///Edit evilPoint & sonPoint base on decision, add gameFlag if applicable
        this.evilPoint = this.evilPoint + (decision ? this.yes[0] : this.no[0]);
        this.sonPoint = this.sonPoint + (decision ? this.yes[1] : this.no[1]);
        if (decision) {
            if(this.specialEventFlag!=null){
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
    public GameFlagManager(int day, int evilPoint, int sonPoint, int[] yes, int[] no, string radioText, string letterText, string gameText, List<string> gameFlags, string specialEventFlag,string specialEventBody, bool decision, string decisionFrom, string newsText) {
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
        this.decisionFrom = decisionFrom;
        this.newsText = newsText;
    }

    public void debug() {
        ///Print level info in a json format
        string log = JsonUtility.ToJson(this);
        // Debug.Log(log);
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
    public string decisionFrom = "special";
    public string newsText = "";
    public string newsTitle;//everyday newspaper, do not contain decision
    public string choiceA = "";
    public string choiceB = "";
}
