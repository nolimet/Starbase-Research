using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct DisplayResearchData
{
    public readonly int ResearchdataLocation;
    public readonly DisplayData display;
}

public readonly struct DisplayData
{
    public readonly double TotalAmount;
    public readonly double TotalResearch;
    public readonly double TotalEfficiency;

    public readonly double CubeEfficiency;
    public readonly double PowerEfficiency;
    public readonly double ShieldEfficiency;
    public readonly double GearEfficiency;
}

[Serializable]
public readonly struct ReasearchData
{
    public readonly ResearchObject[] objects;

    public ReasearchData(ResearchObject[] objects)
    {
        this.objects = objects;
    }
}

[Serializable]
public readonly struct ResearchObject
{
    public readonly string name;
    public readonly int Cube, Power, Shield, Gear;
    public readonly Material[] materials;

    public ResearchObject(string name, int cube, int power, int shield, int gear, Material[] materials)
    {
        this.name = name;
        Cube = cube;
        Power = power;
        Shield = shield;
        Gear = gear;
        this.materials = materials;
    }
}

[Serializable]
public readonly struct Material
{
    public readonly ResourcesType Type;
    public readonly double Amount;

    public Material(ResourcesType type, double amount)
    {
        this.Type = type;
        this.Amount = amount;
    }
}