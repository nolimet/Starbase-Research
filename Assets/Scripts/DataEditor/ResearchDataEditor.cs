using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class ResearchDataEditor : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputName, cubeAmount, powerAmount, shieldAmount, gearAmount;

    [SerializeField]
    private MaterialDataEditor materialDataEditorPrefab;

    private readonly List<MaterialDataEditor> materialDataEditors = new List<MaterialDataEditor>();

    public void Submit()
    {
    }
}