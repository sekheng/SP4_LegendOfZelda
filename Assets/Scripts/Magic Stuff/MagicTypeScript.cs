using UnityEngine;
using System.Collections;

/// <summary>
/// Very hardcoded way to determine magic
/// TODO: Make it less hardcoded when there is time!
/// </summary>
public class MagicTypeScript : MonoBehaviour {
    public enum MAGIC_TYPE
    {
        NORMAL_TYPE,
        FIRE_TYPE,
        WATER_TYPE,
        EARTH_TYPE,
        ICE_TYPE,
        TOTAL_MAGIC_TYPE,
    };
    [Tooltip("Change the magic type of the player")]
    public MAGIC_TYPE currentMagicType = MAGIC_TYPE.TOTAL_MAGIC_TYPE;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Returns the effectiveness! Have to hardcode this because no time to code!
    public float CompareEffectiveOfThisToOtherMagic(MagicTypeScript other)
    {
        switch (currentMagicType)
        {
            case MAGIC_TYPE.FIRE_TYPE:
                switch(other.currentMagicType)
                {
                    case MAGIC_TYPE.WATER_TYPE:
                        return 0.75f;
                    case MAGIC_TYPE.ICE_TYPE:
                        return 1.25f;
                    default: 
                        break;
                }
                break;
            case MAGIC_TYPE.WATER_TYPE:
                switch(other.currentMagicType)
                {
                    case MAGIC_TYPE.EARTH_TYPE:
                        return 0.75f;
                    case MAGIC_TYPE.FIRE_TYPE:
                        return 1.25f;
                    default: 
                        break;
                }
                break;
            case MAGIC_TYPE.EARTH_TYPE:
                switch(other.currentMagicType)
                {
                    case MAGIC_TYPE.ICE_TYPE:
                        return 0.75f;
                    case MAGIC_TYPE.WATER_TYPE:
                        return 1.25f;
                    default: 
                        break;
                }
                break;
            case MAGIC_TYPE.ICE_TYPE:
                          switch(other.currentMagicType)
                {
                    case MAGIC_TYPE.FIRE_TYPE:
                        return 0.75f;
                    case MAGIC_TYPE.EARTH_TYPE:
                        return 1.25f;
                    default: 
                        break;
                }
            break;
            default: 
                break;
        }
        return 1.0f;
    }
}
