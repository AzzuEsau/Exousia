using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecisionList
{
    public string name;
    public int time;
    public string response;
    public string parent;

    public DecisionList()
    {
        name = null;
        time = -1;
        response = null;
        parent = null;
    }
}

 
[System.Serializable]
public class DecisionListGroup
{
    public DecisionList[] decisionList;

    public DecisionListGroup(DecisionList[] Decisions)
    {
        decisionList = Decisions;
    }
}