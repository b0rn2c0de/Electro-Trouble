using UnityEngine;
using System.Collections;

public class electronic : MonoBehaviour {


    public int WastageOffset;
    bool IsOn = false ;
    public Animator anim ;
    AudioSource audio;
    public AudioClip switchAudio ; 
    public int order = -1;
    float Timer = 0.0f;

	void Start () 
    {
	 anim = GetComponent<Animator>();
     audio = GetComponent<AudioSource>();
	}

	void Update () 
    {
        Timer += Time.deltaTime;
        if (IsOn && Timer >= 3.0f)
        {
            Manager.Wastage += WastageOffset;
            Timer = 0;
        }
	}

    public void ToggleOn(bool val)
    {
        IsOn = val;
        audio.clip = switchAudio;
        audio.Play();
        Debug.Log("turned on");
        anim.SetBool("IsOn", IsOn);
    }

}
