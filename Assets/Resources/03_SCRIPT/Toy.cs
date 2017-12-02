using UnityEngine;
using UnityEditor;

public class Toy 
{
    public string name;
    public string description;
    public string size;
    public string spriteName;

    public Toy(string p_name,string p_description, string p_size, string p_spriteName)
    {
        this.name = p_name;
        this.description = p_description;
        this.size = p_size;
        this.spriteName = p_spriteName;
    }


}