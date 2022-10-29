using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionController : MonoBehaviour
{
    [SerializeField]
    private DecisionList[] Decisions;

    private int _numDecisions = 10;
    private string _name = "";
    private int _time = -1;
    private string _parent = null;
    private string _response = null;

    private void Start()
    {
        Decisions = new DecisionList[_numDecisions];

        for (int i = 0; i < _numDecisions; i++)
        {
            Decisions[i] = new DecisionList();
        }
    }

    public void FakeD1Initialize()
    {
        Decisions[0].name = "Poseidon keep trident";
        Decisions[0].time = 4000;
        Decisions[0].response = "yes";
        Decisions[0].response = null;
    }

    public void SetDecision(int decisionIndex, string name, int time, string response, string parent)
    {
        decisionIndex = decisionIndex - 1;
        Decisions[decisionIndex].name = name;
        Decisions[decisionIndex].time = time;
        Decisions[decisionIndex].response = response;
        Decisions[decisionIndex].parent = parent;
    }

    public string GetDecisionName(int decision)
    {
        decision = decision - 1;
        _name = Decisions[decision].name;
        return _name;
    }

    public int GetDecisionTime(int decision)
    {
        decision = decision - 1;
        _time = Decisions[decision].time;
        return _time;
    }

    public string GetDecisionResponse(int decision)
    {
        decision = decision - 1;
        _response = Decisions[decision].response;
        return _response;
    }

    public string GetDecisionParent(int decision)
    {
        decision = decision - 1;
        _parent = Decisions[decision].parent;
        return _parent;
    }

    public string GetDecisionJsonData(int decision)
    {
        decision = decision - 1;
        return GetJsonData(Decisions[decision]);
    }

    public void SetDecisionJsonData(string json,int decision)
    {
        decision = decision - 1;
        Decisions[decision] = JsonUtility.FromJson<DecisionList>(json);
    }

    public string GetJsonData(DecisionList decision)
    {
        return JsonUtility.ToJson(decision);
    }

    public string GetJsonFormat()
    {
        DecisionListGroup decisionListGroup = new DecisionListGroup(Decisions);
 
        return JsonUtility.ToJson(decisionListGroup);
    }

    public void SetJsonFormat(string json)
    {
        DecisionListGroup decisionListGroup = JsonUtility.FromJson<DecisionListGroup>(json);
        Decisions = decisionListGroup.decisionList;
    }

}
