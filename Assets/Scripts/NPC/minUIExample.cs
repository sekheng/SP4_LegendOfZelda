using UnityEngine;
using System.Collections;

public class minUIExample : MonoBehaviour {

    [System.NonSerialized]
    public VIDE_Data dialogue;
#if UNITY_ANDROID
    void Start()
    {
        dialogue = gameObject.AddComponent<VIDE_Data>();
    }

    void OnGUI () {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - (Screen.width / 6), Screen.height / 2, Screen.width / 3, Screen.height / 3));
	    if (dialogue.isLoaded)
        {
            var data = dialogue.nodeData; //Quick reference
            if (data.currentIsPlayer) // If it's a player node, let's show all of the available options as buttons
            {
                for (int i = 0; i < data.playerComments.Length; i++)
                {
                    if (GUILayout.Button(data.playerComments[i])) //When pressed, set the selected option and call Next()
                    {
                        data.selectedOption = i;
                        dialogue.Next();
                    }
                }
            } else //if it's a NPC node, Let's show the comment and add a button to continue
            {
                GUILayout.Label(data.npcComment[data.npcCommentIndex]);

                if (GUILayout.Button(">"))
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
