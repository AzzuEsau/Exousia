using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SendStats : MonoBehaviour
{
    KillsManager killsManager;
    MoneyManager moneyManager;
    DecisionController decisionController;
    KarmaBar karmaBar;

    private int kills;
    private int money;
    private string decisions;
    private float karma;

    private int userID;
    private string userName;
    private string userToken;

    private Form formIssues;
    private string userString;
    private StatsPlayer statsPlayer;


    
    // Start is called before the first frame update
    void Start()
    {
        // killsManager = FindObjectOfType<KillsManager>();
        // moneyManager = FindObjectOfType<MoneyManager>();
        // decisionController = FindObjectOfType<DecisionController>();
        // karmaBar = FindObjectOfType<KarmaBar>();

        // kills = killsManager.GetKill();
        // money = moneyManager.GetMoney();
        // decisions = decisionController.GetJsonFormat();
        // karma = karmaBar.getKarma();

        kills = 0;
        money = 0;
        decisions = "hola";
        karma = 10.0f;

        int userID = PlayerPrefs.GetInt("userID");
        string userName = PlayerPrefs.GetString("userName");
        string userToken = PlayerPrefs.GetString("userToken");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    

    public void CreateStats(){
        CreateStatsDatabase( delegate(Response response){
            Debug.Log(response.message);
            if(response.message != "Send stats Succesfully"){
                // m_messageLabel.text = "Error: " + response.error;
                Debug.Log("Error: " + response.error);
            }
            Debug.Log("Registro Exitoso");
        });
    }
    public void CreateStatsDatabase( Action<Response> response){
        StartCoroutine( CO_CreateStatsDatabase( response ) );
    }

    public IEnumerator CO_CreateStatsDatabase( Action<Response> response){
        WWWForm form = new WWWForm();
        form.AddField("karma",0);
        form.AddField("kills",0);
        form.AddField("decisions","asd");
        form.AddField("money",0);
        form.AddField("idUser", 1);

        UnityWebRequest web = UnityWebRequest.Post("http://localhost:8000/score/",form);

        yield return web.SendWebRequest();
       
        string res = "{\"message\": \"Send stats Succesfully\",\"error\": \"\"}";

        if(web.result == UnityWebRequest.Result.ConnectionError || web.result == UnityWebRequest.Result.ProtocolError){
            formIssues = JsonUtility.FromJson<Form>(web.downloadHandler.text);
            res = "{\"message\": \"Sign in Failed\",\"error\": \""+
                (formIssues.idUser.Count > 0       ? formIssues.idUser[0] : null)+ ", " +
                (formIssues.karma.Count > 0         ? formIssues.karma[0] : null)+ ", " +
                (formIssues.kills.Count > 0         ? formIssues.kills[0] : null)+ ", " +
                (formIssues.decisions.Count > 0     ? formIssues.decisions[0] : null)+ ", " +
                (formIssues.money.Count > 0         ? formIssues.money[0] : null)+ " " +
            "\"}";
            Debug.Log(res);
        }

        response(JsonUtility.FromJson<Response>(res));
        

    }



    /* Update Data */


    public void SendStatsToDataBase(){
        UpdateStats(kills, money, decisions, karma, delegate(Response response){
            Debug.Log(response.message);
            if(response.message != "Send stats Succesfully"){
                // m_messageLabel.text = "Error: " + response.error;
                Debug.Log("Error: " + response.error);
            }
            Debug.Log("Registro Exitoso");
        });
    }
    public void UpdateStats(int kills, int money, string decisions, float karma, Action<Response> response){
        StartCoroutine( CO_UpdateStats( kills, money, decisions, karma, response ) );
    }

    public IEnumerator CO_UpdateStats(int kills, int money, string decisions, float karma, Action<Response> response){
        statsPlayer.idUser = 1;
        statsPlayer.karma = karma;
        statsPlayer.kills = kills;
        statsPlayer.decisions = decisions;
        statsPlayer.money = money;
        string form = JsonUtility.ToJson(statsPlayer);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(form);

        UnityWebRequest web = UnityWebRequest.Put("http://localhost:8000/score/",myData);

        
        web.SetRequestHeader ("Content-Type", "application/json");


        yield return web.SendWebRequest();

        
        string res = "{\"message\": \"Send stats Succesfully\",\"error\": \"\"}";

        if(web.result == UnityWebRequest.Result.ConnectionError || web.result == UnityWebRequest.Result.ProtocolError){
            formIssues = JsonUtility.FromJson<Form>(web.downloadHandler.text);
            res = "{\"message\": \"Sign in Failed\",\"error\": \""+
                (formIssues.idUser != null       ? formIssues.idUser[0] : null)+ ", " +
                (formIssues.karma != null        ? formIssues.karma[0] : null)+ ", " +
                (formIssues.kills != null        ? formIssues.kills[0] : null)+ ", " +
                (formIssues.decisions != null    ? formIssues.decisions[0] : null)+ ", " +
                (formIssues.money != null        ? formIssues.money[0] : null)+ ", " +
            "\"}";
        }

        response(JsonUtility.FromJson<Response>(res));
        

    }





    /* GET DATA STATS */

    public void GetStats(){

        TryGetStats(delegate(Response response){
            if(response.error == ""){
                userString = response.player.Replace("'","\"");

                Debug.Log(userString);
                statsPlayer = JsonUtility.FromJson<StatsPlayer>(userString);
            }
            
            Debug.Log(response.message);
        });
    }

    public void TryGetStats(Action<Response> response){
        StartCoroutine( CO_TryGetStats( response ) );
    }

    public IEnumerator CO_TryGetStats( Action<Response> response){

        UnityWebRequest web = UnityWebRequest.Get("http://localhost:8000/score/" + this.userID);

        yield return web.SendWebRequest();
        
        response(JsonUtility.FromJson<Response>(web.downloadHandler.text));
    }






    public class Form{
        public List<string> idUser;
        public List<string> karma;
        public List<string> kills;
        public List<string> decisions;
        public List<string> money;
    }

    public class StatsPlayer{
        public int idUser;
        public int kills;
        public int money;
        public string decisions;
        public float karma;
    }



    public class Response{
        public string player;
        public string message = "";
        public string error   = "";
        
    }

}