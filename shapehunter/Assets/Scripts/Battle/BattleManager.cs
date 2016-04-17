using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

    public delegate void FightEnded();
    public event FightEnded onFightEnded;

    [SerializeField]
    UITweener[] fightWin;
    [SerializeField]
    UITweener[] fightLose;
    [SerializeField]
    UITweener[] iRun;
    [SerializeField]
    UITweener[] enemyRun;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void RunTweens(UITweener[] tweens)
    {
        foreach (var tween in tweens)
        {
            tween.PlayForward();
        }
    }

    public void StartFight(BattleLevelManager.BattleResult result)
    {
        if (result == BattleLevelManager.BattleResult.FightWon)
        {
            RunTweens(fightWin);
        } else if (result == BattleLevelManager.BattleResult.FightLostDeath)
        {
            RunTweens(fightLose);
        } else if (result == BattleLevelManager.BattleResult.EnemyRan)
        {
            RunTweens(enemyRun);
        } else if (result == BattleLevelManager.BattleResult.YouRan)
        {
            RunTweens(iRun);
        }
    }
}
