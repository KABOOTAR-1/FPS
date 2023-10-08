using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class ClientAPI : MonoBehaviour
{
    string url = "http://localhost:4000/player/";
    private void Start()
    {
        StartCoroutine(Get(url));
    }
    [System.Serializable]
    class PlayerData
    {
        public string user_name;
        public string password;
    }

    [System.Serializable]
    class tp
    {
        public string user_name;
        public int score;
        public string token;

    }
    public IEnumerator Post(string url)
    {
        PlayerData playerData = new PlayerData();
        playerData.user_name = "Arvind";
        playerData.password = "This is it";
        var jsonData = JsonUtility.ToJson(playerData);
 

        using (UnityWebRequest ClientCreate = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            
            using (var uploadHandler = new UploadHandlerRaw(bodyRaw)) // Use 'using' to dispose of the UploadHandlerRaw
            {
                ClientCreate.uploadHandler = uploadHandler;
                ClientCreate.downloadHandler = new DownloadHandlerBuffer();
                ClientCreate.SetRequestHeader("Content-Type", "application/json");

                yield return ClientCreate.SendWebRequest();

                if (ClientCreate.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(ClientCreate.error);
                }
                else
                {
                    if (ClientCreate.isDone)
                    {
                        // handle the result
                        string result = System.Text.Encoding.UTF8.GetString(ClientCreate.downloadHandler.data);
                        Debug.Log(result);
                        try
                        {
                            var re = JsonUtility.FromJson<tp>(result);
                            FileHandling.SaveToken(re.token);
                            Debug.Log(re.user_name);
                        }
                        catch (Exception err)
                        {
                            Debug.Log("This username already exsists");
                        };


                    }
                    else
                    {
                        Debug.Log("Error! Data couldn't get.");
                    }
                }
                uploadHandler.Dispose();
            }
            ClientCreate.Dispose();
        }
    }

    public IEnumerator Get(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url)) {

            string token = FileHandling.LoadToken();
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                if (request.isDone)
                {
                    try{
                        Debug.Log("Response: " + request.downloadHandler.text); 
                    }
                    catch(Exception err)
                    {
                        Debug.Log("You are not authorised "+ err.Message);
                    }
                }
                else
                {
                    Debug.Log("Couldnt get the data");
                }
            }
            request.Dispose();
        } 
    }
} 
