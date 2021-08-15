using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class MaterialDataEditor : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField amount;

    [SerializeField]
    private TMP_Dropdown materialType;

    public Material data => new Material
        (
            Enum.Parse(typeof(ResourcesType), materialType.options[materialType.value].text) as ResourcesType,
            int.TryParse(amount.text, out var result) ? result : 0
        );

    private void Awake()
    {
        var resourceTypes = Enum.GetNames(typeof(ResourcesType));

        materialType.AddOptions(resourceTypes.Select(x => CreateOption(x)).ToList());

        TMP_Dropdown.OptionData CreateOption(string x)
        {
            return new TMP_Dropdown.OptionData(x);
        }
    }
}