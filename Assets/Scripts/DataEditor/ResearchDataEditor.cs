using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class ResearchDataEditor : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputName, cubeAmount, powerAmount, shieldAmount, gearAmount;

    [SerializeField]
    private Transform materialContainer;

    [Inject]
    private MaterialDataEditor.Factory materialFactory;

    private readonly List<MaterialDataEditor> materialDataEditors = new List<MaterialDataEditor>();

    private readonly List<ResearchObject> researchObjects = new List<ResearchObject>();

    public void AddMaterial()
    {
        materialFactory.Create(materialContainer, RemoveMaterialCallback);
    }

    public void Submit()
    {
        researchObjects.Add(new ResearchObject
            (
               name: inputName.text,
               cube: int.TryParse(cubeAmount.text, out var cube) ? cube : 0,
               power: int.TryParse(powerAmount.text, out var power) ? power : 0,
               shield: int.TryParse(shieldAmount.text, out var shield) ? shield : 0,
               gear: int.TryParse(gearAmount.text, out var gear) ? gear : 0,
               materials: materialDataEditors.Select(x => x.Data).ToArray()
            ));
    }

    private void RemoveMaterialCallback(MaterialDataEditor material)
    {
        materialDataEditors.Remove(material);
        Destroy(material.gameObject);
    }
}