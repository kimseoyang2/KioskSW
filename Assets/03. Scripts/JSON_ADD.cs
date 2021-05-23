using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using UnityEngine.Networking;

[System.Serializable]
public class User1
{
    public string Name;
    public string ID;

}

[System.Serializable]
public class Users2
{
    public List<User1> users = new List<User1>();
}

public class JSON_ADD : MonoBehaviour
{
    void Start()
    {
        //Test용 User create
        User1 user1 = new User1
        {
            Name = "Kim",
            ID = "qq"
        };

        User1 user2 = new User1
        {
            Name = "Lee",
            ID = "tt"
        };

        Users2 user_arr = new Users2();

        //Add user
        user_arr.users.Add(user1);
        user_arr.users.Add(user2);

        //Convert JsonString
        string json = JsonUtility.ToJson(user_arr);

        //request Post
        StartCoroutine(Upload("http://127.0.0.1:5000/test", json));
    }

    IEnumerator Upload(string URL, string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }

        }
    }
}