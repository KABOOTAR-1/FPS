using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class ClientAPI : MonoBehaviour
{
    private void Start()
    {

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
    public static IEnumerator LoginUser(string url, string username, string password)
    {
        PlayerData playerData = new PlayerData();
        playerData.user_name = username;
        playerData.password = password;
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
                        }
                        catch (Exception err)
                        {
                            if (result == "400")
                                Debug.Log("Invalid Credentials");
                            if (result == "404")
                                Debug.Log("No such UserName exsits");
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

    public static IEnumerator AddUser(string url, string username, string password)
    {
        PlayerData playerData = new PlayerData();
        playerData.user_name = username;
        playerData.password = password;
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
                            var re = JsonUtility.FromJson<Player>(result);
                        }
                        catch (Exception err)
                        {
                                Debug.Log("Such UserName already exists");
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

    public static IEnumerator Get(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {

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
                    try
                    {
                        Debug.Log("Response: " + request.downloadHandler.text);
                    }
                    catch (Exception err)
                    {
                        Debug.Log("You are not authorised " + err.Message);
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
