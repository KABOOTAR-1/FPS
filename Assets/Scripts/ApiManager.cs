using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiManager
{
    private const string BaseApiUrl = "http://localhost:4000/player/";

    public static string MakeApiCall(string endpoint)
    {
        string apiUrl = BaseApiUrl + endpoint;
        return apiUrl;
    }
}

