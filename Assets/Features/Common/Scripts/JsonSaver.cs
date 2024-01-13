using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver
{
    public static void SaveToJsonFile<T>(T data, string filePath)
    {
        try
        {
            // Convert the data to JSON format
            string jsonData = JsonUtility.ToJson(data, true);

            // Write that JSON to a file
            File.WriteAllText(filePath, jsonData);

            Debug.Log("Data saved to " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving data to JSON: " + ex.Message);
        }
    }
}


