using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    /* OBJECT OF REGISTER AND LOGIN */
    [SerializeField] private GameObject m_registerUI    = null;
    [SerializeField] private GameObject m_loginUI       = null;

    /* FIELDS TO REGISTER */
    [Header("Register")]
    [SerializeField] private InputField m_userNameInput         = null;
    [SerializeField] private InputField m_emailInput            = null;
    [SerializeField] private InputField m_passwordInput         = null;
    [SerializeField] private InputField m_reEnterPasswordInput  = null;
    [SerializeField] private Text m_messageLabel                = null;
    
    /* FIELDS OF LOGIN */
    [Header("Login")]
    [SerializeField] private InputField mL_emailInput            = null;
    [SerializeField] private InputField mL_passwordInput         = null;
    [SerializeField] private Text mL_messageLabel                = null;

    
    public PlayerUser user;
    public Form formIssues;
    public string userString;


    public void SubmitLogin(){
        if(mL_emailInput.text == "" || mL_passwordInput.text == ""){
            m_messageLabel.text = "the username or password field is empty";
            return;
        }

        CheckUser(mL_emailInput.text, mL_passwordInput.text, delegate(Response response){
            if(response.error == ""){
                userString = response.player.Replace("'","\"");
                
                user = JsonUtility.FromJson<PlayerUser>(userString);

                PlayerPrefs.SetInt("userID", user.id);
                PlayerPrefs.SetString("userName", user.user);
                PlayerPrefs.SetString("userToken", user.token);


                SceneManager.LoadScene("PressKey");
            }
            mL_messageLabel.text = response.message;
        });
    }

    public void SubmitRegister(){
        if(m_userNameInput.text == "" || m_emailInput.text == "" || m_passwordInput.text == "" || m_reEnterPasswordInput.text == ""){
            m_messageLabel.text = "Please fill in all the fields";
            return;
        }

        if(m_passwordInput.text == m_reEnterPasswordInput.text){
            CreateUser(m_userNameInput.text, m_emailInput.text, m_passwordInput.text, delegate(Response response){
                if(response.message == "Sign in sucessfully"){
                   ShowLogin();
                }
                m_messageLabel.text = "Error: " + response.error;
            });
        }else{
            m_messageLabel.text = "Different passwords";
            return;
        }
    }


    /* HIDDEN REIGSTER OR LOGIN */
    public void ShowLogin(){
        m_registerUI.SetActive(false);
        m_loginUI.SetActive(true);
    }

    public void ShowRegister(){
        m_registerUI.SetActive(true);
        m_loginUI.SetActive(false);
    }

    /* REGISTER USER */
    public void CreateUser(string userName, string email, string password, Action<Response> response){
        StartCoroutine( CO_CreateUser( userName, email, password, response ) );
    }

    public IEnumerator CO_CreateUser(string userName, string email, string password, Action<Response> response){
        WWWForm form = new WWWForm();
        // form.AddField("id",10);
        form.AddField("user",userName);
        form.AddField("email",email);
        form.AddField("password",password);
        form.AddField("token",password);

        UnityWebRequest web = UnityWebRequest.Post("http://localhost:8000/user/",form);

        yield return web.SendWebRequest();

        
        string res = "{\"message\": \"Sign in sucessfully\",\"error\": \"\"}";

        if(web.result == UnityWebRequest.Result.ConnectionError || web.result == UnityWebRequest.Result.ProtocolError){
            formIssues = JsonUtility.FromJson<Form>(web.downloadHandler.text);
            res = "{\"message\": \"Sign in Failed\",\"error\": \""+formIssues.email[0]+"\"}";
        }

        response(JsonUtility.FromJson<Response>(res));
        

    }

    /* LOGIN USER */
    public void CheckUser(string email, string password, Action<Response> response){
        StartCoroutine( CO_CheckUser( email, password, response ) );
    }

    public IEnumerator CO_CheckUser(string email, string password, Action<Response> response){
        WWWForm form = new WWWForm();
        form.AddField("email",email);
        form.AddField("password",password);

        UnityWebRequest web = UnityWebRequest.Post("http://localhost:8000/user/login",form);

        yield return web.SendWebRequest();
        
        response(JsonUtility.FromJson<Response>(web.downloadHandler.text));
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public class Form{
        public List<string> id;
        public List<string> user;
        public List<string> email;
        public List<string> password;
        public List<string> token;
    }

    public class PlayerUser{
        public int id = 0;
        public string user = "";
        public string email = "";
        public string password = "";
        public string token = "";
        public DateTime updateAt ;
        public DateTime createdAt ;
    }


    [Serializable]
    public class Response{
        public string player;
        public string message = "";
        public string error   = "";
        
    }

}