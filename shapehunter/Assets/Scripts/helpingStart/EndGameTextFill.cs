using UnityEngine;
using System.Collections;

public class EndGameTextFill : MonoBehaviour {

    [SerializeField]
    GameObject rope;

    string getFinishMessage()
    {
        var info = Game.Instance.PlayerInfo;
        if (info.money > 5)
        {
            rope.SetActive(true);
            return "You are rich and your enemies are scared of your name. By killing monsters you hope to defeat true monster inside you. However, each day, becoming stronger, you feel how the bludlust burns you inside out, leaving nothing human behind. Unable to fight more, you decide to put an end to it.";
        } else if (info.money > 2)
        {
            return "Your career is over. You earned enough money for comfortable existence. \n Now you have a hope to find a long-awaited peace. \n And only at the full moon bloodlust again and again will wake up. \n This is your fate.Your battle. Until the end of your days. \n This might not be the maximum you could achieve.";
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
