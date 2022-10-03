using UnityEngine;

public class SecureForm 
{
   private WWWForm m_secureForm = null;

   private const string   CONNECTION_PASSWORD = "Q[>b+1%+%bGNjcok.l,|%Zpm$&XZWc";
   private const string   ANDROID_PASSWORD = "{A;jVY<1ta>yegogdJf[fD}uLh~:GQ";
   private const string   IOS_PASSWORD = "k4c'dr%Gx3|.F.;JwhiQFY1Xp3^/_z";

   public WWWForm secureForm { get { return m_secureForm;  } }


   public SecureForm(){
       m_secureForm = new WWWForm();

       m_secureForm.AddField("connectionPass", CONNECTION_PASSWORD);
       
       #if UNITY_ANDROID
            m_secureForm.AddField("os","android");
            m_secureForm.AddField("platformPass",ANDROID_PASSWORD);
       #endif

       #if UNITY_IOS || UNITY_EDITOR || UNITY_STANDALONE
            m_secureForm.AddField("os","ios");
            m_secureForm.AddField("platformPass",IOS_PASSWORD);
       #endif
   }



}
