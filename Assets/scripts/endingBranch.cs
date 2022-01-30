using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingBranch : MonoBehaviour
{
    [Range(0,10)]
    public int sonPointPass;//if greater than sonPointPass you are good mom
    [Range(0,10)]
    public int evilPointPass;//if greater than evilPointPass you are a$$hole
    
    public GameFlagManager gameFlagManager;
    public string sonText;
    public string neighborText;
    struct EndingBranch {
        public string gameFlag;
        public string branchBody;
        public bool isFromSon;
        public string evilText;
        public string goodText;

        public EndingBranch(string gameFlag, string evilText, string goodText, bool isFromSon) {
            this.gameFlag = gameFlag;
            this.goodText = goodText;
            this.evilText = evilText;
            branchBody = "";
            this.isFromSon = isFromSon;
        }

        public void setBranchBody(string text)
        {
            this.branchBody = text;
        }
    };

    private List<EndingBranch> endingBranches = new List<EndingBranch>();
    private void Start() {
        endingBranches.Add(new EndingBranch("isSteal","","She stole food from our...",false));
        endingBranches.Add(new EndingBranch("isColdKid","","I remember the lady as...",false));
        endingBranches.Add(new EndingBranch("isHelpHobo","","I appreciate...",false));
        endingBranches.Add(new EndingBranch("isHospital","","We worked on the surgery whilst...",false));
        endingBranches.Add(new EndingBranch("isSon","","Mom's a hero...",false));
        endingBranches.Add(new EndingBranch("isMom","","I had to...",false));
        endingBranches.Add(new EndingBranch("isEeyore","","carrot",false));
        endingBranches.Add(new EndingBranch("isJoinWar","","She decided to join the war...",false));
        gameFlagManager = this.GetComponent<GameFlagManager>();
        foreach (EndingBranch endingBranch in endingBranches) {
            if (endingBranch.isFromSon) {            
                endingBranch.setBranchBody(gameFlagManager.sonPoint > sonPointPass ? endingBranch.goodText : endingBranch.evilText);
            }
            else {
                endingBranch.setBranchBody(gameFlagManager.evilPoint > evilPointPass ? endingBranch.evilText : endingBranch.goodText);
            }
        }
    }

    public string getSonText() {
        string _sonText = "";
        foreach (var branch in endingBranches) {
            if (branch.isFromSon == true) {
                if (gameFlagManager.hasFlag(branch.gameFlag)) {
                    _sonText += branch.branchBody;
                    _sonText += "\n";
                }
            }
        }
        
        sonText = _sonText;
        return _sonText;
    }

    public string getNeighborText()
    {        
        string _neighborText = "";
        foreach (var branch in endingBranches) {
            if (branch.isFromSon == false) {
                if (gameFlagManager.hasFlag(branch.gameFlag)) {
                    _neighborText += branch.branchBody;
                    _neighborText += "\n";
                }
            }
        }
        
        neighborText = _neighborText;
        return _neighborText;
    }
}
