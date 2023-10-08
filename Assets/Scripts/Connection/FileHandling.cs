using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileHandling 
{
    public static string filePath = Path.Combine(Application.persistentDataPath, "AuthToken.txt");

    public static void SaveToken(string token)
    {
        try
        {
            File.WriteAllText(filePath, token);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving token: " + e.Message);
        }
    }

    public static string LoadToken()
    {
        try
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading token: " + e.Message);
        }
        return null;
    }
}
