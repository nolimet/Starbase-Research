using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MaterialItem : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI resourceName;

    [SerializeField]
    private TextMeshProUGUI resourceAmount;

    public class Factory : PlaceholderFactory<MaterialItem>
    {
        [Inject]
        private MaterialIconsConfiguration materialIcons;

        public MaterialItem Create(Transform parent, Material material)
        {
            var newMaterial = base.Create();
            newMaterial.transform.SetParent(parent, false);
            newMaterial.icon.sprite = materialIcons.GetIconForMaterial(material.Type);
            newMaterial.resourceName.text = material.Type.ToString();
            newMaterial.resourceAmount.text = string.Format("{0:#.##}kv", material.Amount);

            return newMaterial;
        }
    }
}