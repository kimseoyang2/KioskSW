using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;

public class Flask_WW : MonoBehaviour
{
    public int i = 0;



    // Start is called before the first frame update
    void Start()
    {
        
        //StartCoroutine(GetRequest("http://127.0.0.1:5000/test"));
    }

    //IEnumerator GetRequest(string url)
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
    //    {
    //        yield return webRequest.SendWebRequest();
    //        if (webRequest.isNetworkError)
    //        {
    //            Debug.Log("Error:" + webRequest.error);

    //        }
    //        else
    //        {
    //            Debug.Log(webRequest.downloadHandler.text);
    //        }
    //    }
    //}

    public void Clicked()
    {
        StartCoroutine(RegistrationCoroutine("http://127.0.0.1:5000/test"));
        //StartCoroutine(RegistrationCoroutine("http://127.0.0.1:5000/"));
    }
    IEnumerator RegistrationCoroutine(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("fromUnity" ,"200", Encoding.UTF8);// ㅅㅓㅂㅓㅇㅔ ㅈㅓㄴㅅㅗㅇ
        


        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
            
        }
        else
        {
            Debug.Log("POST" + ":" + webRequest.downloadHandler.text);

        }
    }
}





