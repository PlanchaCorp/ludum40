using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Toy
{
    public Dictionary<int, string> sizeLabel = new Dictionary<int, string>()
    {
        {1,"Small" },
        {2,"Medium" },
        {3,"Large" },
    };
    
    public string name;
    public string description;
    public int size;
    public string spriteName;
    public bool broken;

    public Toy() { }
    public Toy(string p_name,string p_description, int p_size, string p_spriteName, bool p_broken)
    {
        this.name = p_name;
        this.description = p_description;
        this.size = p_size;
        this.spriteName = p_spriteName;
        this.broken = p_broken;
    }

   public string GetLabelSize()
    {
        return sizeLabel[this.size];
    }


}