using UnityEngine;
using System.Collections;

public class ChoiceManager : MonoBehaviour {

    [SerializeField]
    GameObject grid;

    [SerializeField]
    UITexture zeroChoice;

    public void pickZero()
    {

    }

    public void pickOne()
    {

    }

    public void pickTwo()
    {

    }

    public void pickThree()
    {

    }

    string[] choices;

    public void activateWithChoices(string[] choices)
    {
        this.choices = choices;

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
