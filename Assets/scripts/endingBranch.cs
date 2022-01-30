using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingBranch : MonoBehaviour
{
    public GameFlagManager gameFlagManager;
    public string sonText;
    public string neighborText;
    struct EndingBranch
    {
        public string gameFlag;
        public string branchBoby;
        public bool isFromSon;

        public EndingBranch(string gameFlag, string branchBoby, bool isFromSon)
        {
            this.gameFlag = gameFlag;
            this.branchBoby = branchBoby;
            this.isFromSon = isFromSon;
        }
    };

    private List<EndingBranch> endingBranches;
    private void Start() {
        endingBranches.Add(new EndingBranch("isSteal","She stole food from our...",false));
        endingBranches.Add(new EndingBranch("isColdKid","I remember the lady as...",false));
        endingBranches.Add(new EndingBranch("isHelpHobo","I appreciate...",false));
        endingBranches.Add(new EndingBranch("isHospital","We worked on the surgery whilst...",false));
        endingBranches.Add(new EndingBranch("isSon","Mom's a hero...",false));
        endingBranches.Add(new EndingBranch("isMom","I had to...",false));
        endingBranches.Add(new EndingBranch("isEeyore","carrot",false));
        endingBranches.Add(new EndingBranch("isJoinWar","She decided to join the war...",false));
        gameFlagManager = this.GetComponent<GameFlagManager>();
    }

    public string getSonText() {
        string _sonText = "";
        foreach (var branch in endingBranches) {
            if (branch.isFromSon == true) {
                if (gameFlagManager.hasFlag(branch.gameFlag))
                {
                    _sonText += branch.branchBoby;
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
                if (gameFlagManager.hasFlag(branch.gameFlag))
                {
                    _neighborText += branch.branchBoby;
                }
            }
        }

        neighborText = _neighborText;
        return _neighborText;
        
    }

}
