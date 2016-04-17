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
        if (result == BattleLevelManager.BattleResult.FightWin)
        {
            RunTweens(fightWin);
        } else if (result == BattleLevelManager.BattleResult.FightLose)
        {
            RunTweens(fightLose);
        } else if (result == BattleLevelManager.BattleResult.EnemyRun)
        {
            RunTweens(enemyRun);
        } else if (result == BattleLevelManager.BattleResult.YouRun)
        {
            RunTweens(iRun);
        }
    }
}
