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

    [SerializeField]
    GameObject endRound;

    [SerializeField]
    UIButton panel;

    [SerializeField]
    UITexture blood;

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
            manager.panel.onClick.Add(new EventDelegate(manager.Proceed));
        }
    }

    class ChangeFormStart : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            manager.panel.onClick.Clear();
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
            manager.me.mainTexture = Resources.Load(manager.choiceManager.picturePathByAnimalName(manager.myAnimal)) as Texture2D;
            manager.enemy.mainTexture = Resources.Load(manager.choiceManager.picturePathByAnimalName(manager.enemyAnimal)) as Texture2D;
            manager.me.width = manager.me.height;
            manager.enemy.width = manager.enemy.height;
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
            manager.panel.onClick.Add(new EventDelegate(manager.Proceed));
            manager.endRound.SetActive(true);
            manager.ShowFightResults(manager.myAnimal);
            manager.state = new EndScreenChoices();
            BattleResult result = manager.winDictionary[manager.myAnimal];
            if ((result == BattleResult.FightWon)||(result == BattleResult.FightLostDeath))
            {
                manager.blood.gameObject.SetActive(true);
            }
        }
    }

    class EndScreenChoices : GameState
    {
        public override void Proceed(BattleLevelManager manager)
        {
            manager.ProcessResult();
            var info = Game.Instance.PlayerInfo;
            Application.LoadLevel("tableScene");
        }
    }

    #endregion

    private void ProcessResult()
    {
        var info = Game.Instance.PlayerInfo;
        info.complete.Add(info.currentEnemy);

        var result = winDictionary[myAnimal];

        if (result == BattleResult.FightWon)
        {
            info.money += 1;
        }
        else if (result == BattleResult.FightLostDeath)
        {
            Game.Instance.ResetPlayer();
        }
        else if (result == BattleResult.EnemyRan)
        {
        }
        else if (result == BattleResult.YouRan)
        {
        }
    }    

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
        endText.text = resultText[winDictionary[animal]];
        state = new EndScreenChoices();
    }
    
    XmlNode target;

    public enum BattleResult
    {
        FightWon = 1,
        YouRan = -1,
        EnemyRan = 0,
        FightLostDeath = -2
    }
    Dictionary<BattleResult, string> resultText = new Dictionary<BattleResult, string>()
    { {BattleResult.FightWon, "You made a kill and your job is done. Perfect, as usual." },
      {BattleResult.FightLostDeath, "Life plays jokes sometimes. Prey and predator changed roles. You are dead." },
      {BattleResult.EnemyRan, "You couldn't catch this sly bastard! Your prey ran away and you lost your chance to yearn some money." },
      {BattleResult.YouRan, "Given the circumstances you did not have any choice but to save yourself. Pride is good but life is priceless. At least yours is ;)" } };

    Dictionary<string, BattleResult> winDictionary = new Dictionary<string, BattleResult>();
    string enemyAnimal;
    string myAnimal;

    public string silhouettePath(string name)
    {
        return "art/silhouettes/" + name;
    }

    // Use this for initialization
    void Start () {
        var targetName = Game.Instance.PlayerInfo.currentEnemy;
        target = Game.Instance.targetNode(targetName);

        enemyAnimal = target.Attributes["animal"].Value;
        enemy.mainTexture = Resources.Load(silhouettePath(target.Attributes["silhouette"].Value)) as Texture2D;

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
