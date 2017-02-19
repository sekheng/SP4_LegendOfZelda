using UnityEngine;
using System.Collections;

/// <summary>
/// Will help to fix the mobile build of particle system
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ParticleScript : MonoBehaviour {
    private ParticleSystem toRenderParticleEffects;
    // Used to debuggin test in android! Will remove soon!
    //private TextMesh debugginMesh;
#if UNITY_ANDROID
    //[Tooltip("The time to kill all the effects")]
    //public float m_time = 0.4f;

    //// Time counter
    //private float m_timeCounter = 0;

	// Use this for initialization
	void Start () {
        //if (debugginMesh == null)
        //    debugginMesh = GameObject.Find("Remove TODO").GetComponent<TextMesh>();
        //debugginMesh.text = "Activating blood effects particle";
        toRenderParticleEffects = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (gameObject.activeSelf)
        //{
        //    m_timeCounter += Time.deltaTime;
        //    if (m_timeCounter > m_time)
        //    {
        //        Debug.Log("Set to inactive");
        //        toRenderParticleEffects.Stop();
        //        toRenderParticleEffects.Clear();
        //        gameObject.SetActive(false);
        //    }
        //}
    }
#else
    void Start()
    {
        toRenderParticleEffects = GetComponent<ParticleSystem>();
    }
#endif

    public void playEffect()
    {
        //if (debugginMesh == null)
        //    debugginMesh = GameObject.Find("Remove TODO").GetComponent<TextMesh>();
        debugginMesh.text = "Position of particle: " + transform.position;
        toRenderParticleEffects.Play();
    }
}
