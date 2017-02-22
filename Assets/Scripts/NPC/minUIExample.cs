using UnityEngine;
using System.Collections;

public class minUIExample : MonoBehaviour {

    [System.NonSerialized]
    public VIDE_Data dialogue;
    private GUIStyle style, style2;

    // TODO: Will remove once finished debugging GUI Text
    //private TextMesh debugAlignment;

#if UNITY_ANDROID
    void Start()
    {
        dialogue = gameObject.AddComponent<VIDE_Data>();
        
    }

    void OnGUI () {
        //if (debugAlignment == null)
        //{
        //    debugAlignment = GameObject.Find("DebuggingText").GetComponent<TextMesh>();
        //}

        GUILayout.BeginArea(new Rect(Screen.width / 2 - (Screen.width / 4), Screen.height / 2, Screen.width / 2, Screen.height));

        //debugAlignment.text = "Screen Height: " + Screen.height + ", Screen Width: " + Screen.width; 

        style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        style.wordWrap = true;
        style.fontSize = Screen.width / 32;

        style2 = new GUIStyle(GUI.skin.box);
        style2.normal.textColor = Color.white;
        style2.wordWrap = true;
        style2.fontSize = Screen.height / 18;

	    if (dialogue.isLoaded)
        {
            var data = dialogue.nodeData; //Quick reference
            if (data.currentIsPlayer) // If it's a player node, let's show all of the available options as buttons
            {
                for (int i = 0; i < data.playerComments.Length; i++)
                {
                    if (GUILayout.Button(data.playerComments[i], style)) //When pressed, set the selected option and call Next()
                    {
                        data.selectedOption = i;
                        dialogue.Next();
                    }
                }
            } else //if it's a NPC node, Let's show the comment and add a button to continue
            {
                GUILayout.Label(data.npcComment[data.npcCommentIndex], style2);

                if (GUILayout.Button(">", style))
                {
                    dialogue.Next();
                }
            }
			if (data.isEnd) // If it's the end, let's just call EndDialogue
            {
                if(dialogue.assigned.dialogueName.Equals("Dragon") && !LocalDataSingleton.instance.talkedToDragon)
                {
                    LocalDataSingleton.instance.talkedToDragon = true;
                    dialogue.assigned.overrideStartNode = 30;
                }
                LocalDataSingleton.instance.talking = false;
                dialogue.EndDialogue();
            }
        }
        GUILayout.EndArea();
	}
#endif
}
