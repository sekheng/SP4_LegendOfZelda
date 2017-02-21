using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class itemInformation {
    public string item_type;
    public string item_name;
    public string item_effect;
    public int item_count;
    //public Text display_stats;

    public itemInformation()
    {
        item_count = 0;
    }
    public itemInformation(string zeName, string zeType, string zeEffect)
    {
        item_name = zeName;
        item_type = zeType;
        item_effect = zeEffect;
    }
    public itemInformation(itemInformation zeOther)
    {
        item_name = zeOther.item_name;
        item_effect = zeOther.item_effect;
        item_type = zeOther.item_type;
        item_count = zeOther.item_count;
    }

    //public void updateStatDisplay()
    //{
    //    StringBuilder str = new StringBuilder("Name:");
    //    str.Append(item_name + ", ");
    //    str.Append("Type:" + item_type + ", ");
    //    str.Append("Effect:" + item_effect + ", ");
        

    //    //display_stats.text = str.ToString();
    //}
}
