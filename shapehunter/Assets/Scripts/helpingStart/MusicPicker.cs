using UnityEngine;
using System.Collections;

public class MusicPicker : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        var audios = GetComponents<AudioSource>();
        foreach (var a in audios)
        {
            a.Stop();
        }
        audios[Random.Range(0, audios.Length)].Play();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
