using UnityEngine;
using System.Collections;

public class PrehistoryLabelBeh : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
    public void shutDownPrehistory()
    {
        Game.Instance.PlayerInfo.additionalProgress.showedPrehistory = true;
    }

	// Update is called once per frame
	void Update () {
        if (Game.Instance.PlayerInfo.additionalProgress.showedPrehistory)
            gameObject.SetActive(false);
    }
}
