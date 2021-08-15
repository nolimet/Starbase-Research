using System;
using System.Collections.Generic;
using System.Linq;

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
    public readonly string Name;
    public readonly int Cube, Power, Shield, Gear;
    public readonly Material[] Materials;

    public ResearchObject(string name, int cube, int power, int shield, int gear, Material[] materials)
    {
        this.Name = name;
        Cube = cube;
        Power = power;
        Shield = shield;
        Gear = gear;
        this.Materials = materials;
    }

    public override bool Equals(object obj)
    {
        if (obj != null && obj is ResearchObject r)
        {
            return GetHashCode() == r.GetHashCode();
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hashCode = 885680131;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + Cube.GetHashCode();
        hashCode = hashCode * -1521134295 + Power.GetHashCode();
        hashCode = hashCode * -1521134295 + Shield.GetHashCode();
        hashCode = hashCode * -1521134295 + Gear.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Material[]>.Default.GetHashCode(Materials);
        return hashCode;
    }

    public static bool operator ==(ResearchObject r1, ResearchObject r2)
    {
        return r1.Equals(r2);
    }

    public static bool operator !=(ResearchObject r1, ResearchObject r2)
    {
        return !r1.Equals(r2);
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

    public override bool Equals(object obj)
    {
        if (obj != null && obj is Material m)
        {
            return GetHashCode() == m.GetHashCode();
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hashCode = -1636817442;
        hashCode = hashCode * -1521134295 + Type.GetHashCode();
        hashCode = hashCode * -1521134295 + Amount.GetHashCode();
        return hashCode;
    }

    public static bool operator ==(Material m1, Material m2)
    {
        return m1.Equals(m2);
    }

    public static bool operator !=(Material m1, Material m2)
    {
        return !m1.Equals(m2);
    }
}