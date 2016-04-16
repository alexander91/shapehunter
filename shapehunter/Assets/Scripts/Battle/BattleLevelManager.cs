using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;

public class BattleLevelManager : MonoBehaviour {

    [SerializeField]
    ChoiceManager choiceManager;

    [SerializeField]
    UILabel endText;




    abstract class GameState
    {
        public abstract void Proceed(BattleLevelManager manager);
    }

    class ShowingChoices : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            throw new NotImplementedException();
        }
    }

    void ManageChoice(string animal)
    {
        endText.text = winDictionary[animal].ToString();
    }
    
    XmlNode target;

    string picturePathByAnimalName(string animalName)
    {
        var animal = Game.Instance.animalNode(animalName);
        return "art/animals/" + animal.Attributes["picture"];
    }

    enum BattleResult
    {
        Win = 1,
        Lose = -1,
        Draw = 0
    }

    Dictionary<string, BattleResult> winDictionary = new Dictionary<string, BattleResult>();

    // Use this for initialization
    void Start () {
        var targetName = Game.Instance.PlayerInfo.currentEnemy;
        target = Game.Instance.targetNode(targetName);
        List<string> animals = new List<string>();
        var options = target.SelectSingleNode("options").SelectNodes("option");
        foreach (XmlNode animalChoice in options)
        {
            string animalName = animalChoice.Attributes["name"].Value;
            animals.Add(animalName);
            winDictionary[animalName] = (BattleResult)(int.Parse(animalChoice.Attributes["result"].Value));
        }
        choiceManager.activateWithChoices(animals.ToArray());
        choiceManager.onAnimalChoiceMade += ManageChoice;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
