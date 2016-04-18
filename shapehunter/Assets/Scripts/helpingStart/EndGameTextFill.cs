using UnityEngine;
using System.Collections;

public class EndGameTextFill : MonoBehaviour {

    string getFinishMessage()
    {
        var info = Game.Instance.PlayerInfo;
        if (info.money > 1)
        {
            return "You are rich and your enemies are scared of your name. But killing people you lost hope in life. You failed.";
        } else
        {
            return "Enough! You decide that you have had enough of this, time to make your life peaceful! You can say you won.";
        }
    }

	// Use this for initialization
	void Start () {
        GetComponent<UILabel>().text = getFinishMessage();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Restart()
    {
        Game.Instance.ResetPlayer();
        Application.LoadLevel("tableScene");
    }
}
