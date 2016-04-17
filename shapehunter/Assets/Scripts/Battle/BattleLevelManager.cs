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

    [SerializeField]
    UITweener[] changingFormTweens;

    [SerializeField]
    UITexture me;
    [SerializeField]
    UITexture enemy;

    [SerializeField]
    BattleManager battleManager;

    GameState state = new ShowingChoices();

    #region States

    abstract class GameState
    {
        public abstract void Proceed(BattleLevelManager manager);
    }

    class ShowingChoices : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            
        }
    }

    class ChangeFormStart : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            foreach (var tween in manager.changingFormTweens)
            {
                tween.PlayForward();
            }
            manager.state = new ChangeFormMiddle();
        }
    }

    class ChangeFormMiddle : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {            
            manager.me.mainTexture = Resources.Load(manager.picturePathByAnimalName(manager.myAnimal)) as Texture2D;
            manager.enemy.mainTexture = Resources.Load(manager.picturePathByAnimalName(manager.enemyAnimal)) as Texture2D;
            foreach (var tween in manager.changingFormTweens)
            {
                tween.PlayReverse();
            }

            manager.state = new ChangeFormEnd();
        }
    }

    class ChangeFormEnd : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            manager.battleManager.StartFight(manager.winDictionary[manager.myAnimal]);
            manager.state = new Battle();
        }
    }

    class Battle : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            manager.ShowFightResults(manager.myAnimal);
            manager.state = new EndScreenChoices();
        }
    }

    class EndScreenChoices : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            var info = Game.Instance.PlayerInfo;
            info.complete.Add(info.currentEnemy);
            Application.LoadLevel("tableScene");
        }
    }

    #endregion

    public void Proceed()
    {
        state.Proceed(this);
    }

    void ManageChoice(string animal)
    {
        myAnimal = animal;
        state = new ChangeFormStart();
        state.Proceed(this);
    }

    void ShowFightResults(string animal)
    {
        endText.text = winDictionary[animal].ToString();
        state = new EndScreenChoices();
    }
    
    XmlNode target;

    string picturePathByAnimalName(string animalName)
    {
        var animal = Game.Instance.animalNode(animalName);
        return "art/animals/" + animal.Attributes["picture"];
    }

    public enum BattleResult
    {
        FightWin = 1,
        YouRun = -1,
        EnemyRun = 0,
        FightLose = -2
    }

    Dictionary<string, BattleResult> winDictionary = new Dictionary<string, BattleResult>();
    string enemyAnimal;
    string myAnimal;

    // Use this for initialization
    void Start () {
        var targetName = Game.Instance.PlayerInfo.currentEnemy;
        target = Game.Instance.targetNode(targetName);

        enemyAnimal = target.Attributes["animal"].Value;

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
