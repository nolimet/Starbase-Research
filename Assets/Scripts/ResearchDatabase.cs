using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Net;
using UnityEngine.Networking;

public class ResearchDatabase : MonoBehaviour
{
    private const string filename = "researchdata.json";

    public static readonly string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, filename);

    public ReasearchData reasearchData;

    public async void LoadFromDisk()
    {
        using (var webrequest = UnityWebRequest.Get(filePath))
        {
            await webrequest.SendWebRequest();
            if (webrequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(webrequest.error);
            }
            else
            {
                researchData = JsonConvert.DeserializeObject(webrequest.downloadHandler.text);
            }
        }
    }
}