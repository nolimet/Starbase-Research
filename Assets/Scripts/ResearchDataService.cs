﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ResearchDataService
{
    private const string filename = "researchdata.json";
    private readonly string filePath = Path.Combine(Application.streamingAssetsPath, filename);

    private readonly List<ResearchObject> researchObjects;
    public IReadOnlyList<ResearchObject> ResearchObjects => researchObjects;

    public void AddNewResearchObject(ResearchObject researchObject)
    {
        researchObjects.Add(researchObject);
    }

    public async void LoadData()
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
                var researchData = JsonConvert.DeserializeObject<ReasearchData>(webrequest.downloadHandler.text);
                researchObjects.Clear();
                researchObjects.AddRange(researchData.objects);
            }
        }
    }

    public async void SaveData()
    {
        var file = new FileInfo(filePath);
        if (!file.Directory.Exists)
        {
            file.Directory.Create();
        }

        string json = JsonConvert.SerializeObject(new ReasearchData(researchObjects.OrderBy(x => x.name).ToArray()), Formatting.Indented);
        using (var textWriter = file.CreateText())
        {
            await textWriter.WriteAsync(json);
        }
    }
}