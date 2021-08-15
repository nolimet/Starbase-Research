using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Icons Configuration", menuName = "Configuration/MaterialIconConfiguration")]
public class MaterialIconsConfiguration : ScriptableObject
{
    [SerializeField]
    private Material[] materials;

    private Dictionary<ResourcesType, Sprite> lookupTable;

    public Sprite GetIconForMaterial(ResourcesType type)
    {
        if (lookupTable == null)
        {
            lookupTable = materials.ToDictionary(x => x.material, x => x.icon);
        }

        return lookupTable[type];
    }

    private void UpdateList()
    {
        if (Application.isPlaying)

            return;

        var resourceTypes = Enum.GetValues(typeof(ResourcesType)) as ResourcesType[];
        if (materials == null)
        {
            materials = new Material[0];
        }
        materials = materials.Concat
           (
               resourceTypes
               .Where
               (
                   t =>
                       t != ResourcesType.None && !materials.Any(
                           m =>
                               m.material == t
                           )
               )
               .Select(x => new Material(x))
               .OrderBy(x => x.material)
           ).ToArray();
    }

    [Serializable]
    private class Material
    {
        public Sprite icon;
        public ResourcesType material;

        public Material(ResourcesType material)
        {
            this.material = material;
        }

        public Material(Sprite icon, ResourcesType material)
        {
            this.icon = icon;
            this.material = material;
        }
    }
}