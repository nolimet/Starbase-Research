using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class MaterialDataEditor : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField amount;

    [SerializeField]
    private TMP_Dropdown materialType;

    [SerializeField]
    private TabNavigationAction nextAction;

    public TabNavigationAction NextAction => nextAction;

    private Action<MaterialDataEditor> removeAction;

    public Material Data => new Material
        (
            (ResourcesType)Enum.Parse(typeof(ResourcesType), materialType.options[materialType.value].text),
            double.TryParse(amount.text, out var result) ? result : 0
        );

    public void RemoveMaterial()
    {
        removeAction?.Invoke(this);
    }

    private void Start()
    {
        amount.SetTextWithoutNotify("0");
    }

    public class Factory : PlaceholderFactory<MaterialDataEditor>
    {
        [Inject]
        private MaterialIconsConfiguration materialIcons;

        public MaterialDataEditor Create(Transform parent, Action<MaterialDataEditor> removeAction)
        {
            var newMaterial = base.Create();
            newMaterial.transform.SetParent(parent, false);
            newMaterial.removeAction = removeAction;

            var resourceTypes = Enum.GetValues(typeof(ResourcesType)) as ResourcesType[];
            newMaterial.materialType.options = resourceTypes.Select(x => CreateOption(x)).ToList();

            return newMaterial;
            TMP_Dropdown.OptionData CreateOption(ResourcesType resource)
            {
                return new TMP_Dropdown.OptionData(resource.ToString(), materialIcons.GetIconForMaterial(resource));
            }
        }

        public MaterialDataEditor Create(Transform parent, Action<MaterialDataEditor> removeAction, Material data)
        {
            var newMaterial = Create(parent, removeAction);

            newMaterial.amount.text = data.Amount.ToString();
            string materialName = data.Type.ToString();
            newMaterial.materialType.value = newMaterial.materialType.options.FindIndex(x => x.text == materialName);

            return newMaterial;
        }
    }
}