using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class itemInformation : MonoBehaviour {
    public string item_type;
    public string item_name;
    public string item_effect;
    public int item_count;
    public Text display_stats;

    public void updateStatDisplay()
    {
        StringBuilder str = new StringBuilder("Name:");
        str.Append(item_name + ", ");
        str.Append("Type:" + item_type + ", ");
        str.Append("Effect:" + item_effect + ", ");
        

        display_stats.text = str.ToString();
    }
}
