using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_Login : MonoBehaviour
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
    [SerializeField] private InputField mL_userNameInput         = null;
    [SerializeField] private InputField mL_passwordInput         = null;
    [SerializeField] private Text mL_messageLabel                = null;

    // private voide Awake(){
    //     m_networkManager = GameObject.findObjectOfType<NetworkManager>();
    // }

    public void SubmitLogin(){
        if(mL_userNameInput.text == "" || mL_passwordInput.text == ""){
            m_messageLabel.text = "El usuario o contraseña estan vacios";
            return;
        }

        CheckUser(mL_userNameInput.text, mL_passwordInput.text, delegate(Response response){
            if(response.message == "valid"){
                SceneManager.LoadScene("Intro");
            }
            mL_messageLabel.text = response.message;
        });
    }

    public void SubmitRegister(){
        if(m_userNameInput.text == "" || m_emailInput.text == "" || m_passwordInput.text == "" || m_reEnterPasswordInput.text == ""){
            m_messageLabel.text = "Porfavor llena todos los campos";
            return;
        }

        if(m_passwordInput.text == m_reEnterPasswordInput.text){
            CreateUser(m_userNameInput.text, m_emailInput.text, m_passwordInput.text, delegate(Response response){
                m_messageLabel.text = response.message;
            });
        }else{
            m_messageLabel.text = "Contraseñas diferentes";
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
        SecureForm form = new SecureForm();
        form.secureForm.AddField("userName",userName);
        form.secureForm.AddField("email",email);
        form.secureForm.AddField("password",password);

        WWW w = new WWW("http://localhost/Exousia_DB/SignIn.php",form.secureForm);

        yield return w;

        response(JsonUtility.FromJson<Response>(w.text));
    }

    /* LOGIN USER */
    public void CheckUser(string userName, string password, Action<Response> response){
        StartCoroutine( CO_CheckUser( userName, password, response ) );
    }

    public IEnumerator CO_CheckUser(string userName, string password, Action<Response> response){
        SecureForm form = new SecureForm();
        form.secureForm.AddField("userName",userName);
        form.secureForm.AddField("password",password);

        WWW w = new WWW("http://localhost/Exousia_DB/Login.php",form.secureForm);

        yield return w;

        response(JsonUtility.FromJson<Response>(w.text));
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class Response{
    public bool   done    = false;
    public string message = "" ;

    
}
