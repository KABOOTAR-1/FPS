using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ClientAPI : MonoBehaviour
{
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

    public static IEnumerator UserAction(string url, string username, string password, bool isLogin,Action<bool> onResult)
    {
        PlayerData playerData = new PlayerData();
        playerData.user_name = username;
        playerData.password = password;
        var jsonData = JsonUtility.ToJson(playerData);

        using (UnityWebRequest clientRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

            using (var uploadHandler = new UploadHandlerRaw(bodyRaw))
            {
                clientRequest.uploadHandler = uploadHandler;
                clientRequest.downloadHandler = new DownloadHandlerBuffer();
                clientRequest.SetRequestHeader("Content-Type", "application/json");


                yield return clientRequest.SendWebRequest();
                if (clientRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(clientRequest.error);
                }
                else
                {
                    if (clientRequest.isDone)
                    {
                        string result = System.Text.Encoding.UTF8.GetString(clientRequest.downloadHandler.data);
                        Debug.Log(result);
                        bool success = false;
                        try
                        {
                            var re = JsonUtility.FromJson<tp>(result);
                            if (!string.IsNullOrEmpty(re.token))
                            {
                                FileHandling.SaveToken(re.token);
                            }
                            success = true;
                        }
                        catch (Exception err)
                        {
                            if (result == "400")
                                Debug.Log("Invalid Credentials");
                            if (result == "404" && isLogin)
                                Debug.Log("No such UserName exists");
                            if (result == "409" && !isLogin)
                                Debug.Log("Such UserName already exists");
                        }
                        onResult?.Invoke(success);
                    }
                    else
                    {
                        Debug.Log("Error! Data couldn't be retrieved.");
                    }
                }
                uploadHandler.Dispose();
                clientRequest.uploadHandler.Dispose();
                clientRequest.downloadHandler.Dispose();
            }
            clientRequest.Dispose();
        }
    }

    public static IEnumerator Get(string url, bool sendToken = true)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            if (sendToken)
            {
                string token = FileHandling.LoadToken();
                if (!string.IsNullOrEmpty(token))
                {
                    request.SetRequestHeader("Authorization", "Bearer " + token);
                }
            }

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
                        Debug.Log("You are not authorized " + err.Message);
                    }
                }
                else
                {
                    Debug.Log("Couldn't get the data.");
                }
            }

            request.Dispose();
        }
    }
    [System.Serializable]
    class PlayerChange
    {
       public string user_name;
       public int score;
    }
    public static IEnumerator Put(string url, string user_name, int score)
    {
        PlayerChange player = new PlayerChange
        {
            user_name = user_name,
            score = score
        };
        string jsonData = JsonUtility.ToJson(player);

        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            string token = FileHandling.LoadToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

            using (var uploadHandler = new UploadHandlerRaw(bodyRaw))
            {
                request.uploadHandler = uploadHandler;
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError("Connection error: " + request.error);
                }
                else if (request.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                    Debug.Log("Request successful!");
                    Debug.Log(result);
                }
                else
                {
                    Debug.LogError("Error! Data couldn't be received.");
                }
            }
        }
    }

}
