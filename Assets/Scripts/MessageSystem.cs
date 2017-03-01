using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
/// <summary>
/// This is an observer pattern!
/// Only use it if you are confident about it.
/// HealthUIScript is using it!
/// </summary>
public class MessageSystem : MonoBehaviour {

    private Dictionary<string, UnityEvent> eventsDictionary;    // This acts like a hashmap! Right now its values shall be the function calls!
    private static MessageSystem cantTouchThisSystem;   // the static variable which everyone can't touch it!

    public static MessageSystem instance    // A getter to access the singleton's static variable
    {
        get
        {
            if (!cantTouchThisSystem)
            {
                cantTouchThisSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<MessageSystem>();
                if (!cantTouchThisSystem)   // If can't find the message system. return null instead
                    return null;
                cantTouchThisSystem.Start();
            }
            return cantTouchThisSystem;
        }
    }

	// Use this for initialization
	void Start () {
	    if (eventsDictionary == null)   // Checks whether it has been intialized
            eventsDictionary = new Dictionary<string,UnityEvent>();
	}
	
    public bool setListener(string zeKey, UnityAction zeFunctionCall)
    {
        UnityEvent zeEventCall = null;
        if (eventsDictionary.TryGetValue(zeKey, out zeEventCall))    // if there is already a key, just add the function into it!
        {
            zeEventCall.AddListener(zeFunctionCall);
        }
        else
        {
            // Add new keys and values if can't find the keys
            zeEventCall = new UnityEvent();
            zeEventCall.AddListener(zeFunctionCall);
            eventsDictionary.Add(zeKey, zeEventCall);
        }
        return true;
    }

    public bool removeListener(string zeKey, UnityAction zeFunctionCall)
    {
        UnityEvent zeEventCall = null;
        if (eventsDictionary.TryGetValue(zeKey, out zeEventCall))   // If can find the key, then remove the listener
        {
            zeEventCall.RemoveListener(zeFunctionCall);
            return true;
        }
        return false;
    }
    
    public bool triggerEventCall(string zeEventName)
    {
        UnityEvent zeEventCall = null;
        if (eventsDictionary.TryGetValue(zeEventName, out zeEventCall))
        {
            zeEventCall.Invoke();
            return true;
        }
        return false;
    }
}
