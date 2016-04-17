using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CabinetLevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject withLetter;

    [SerializeField]
    GameObject withoutLetter;

    [SerializeField]
    LetterManager letter;

    [SerializeField]
    UILabel gameFinishText;

    enum Status
    {
        PlainMenu,
        PickingLetter
    }

    Status nowStatus = Status.PlainMenu;

    public void nextAction()
    {
        if (nowStatus == Status.PlainMenu)
        {
            pickTarget();
            nowStatus = Status.PickingLetter;
        }
        else
        {
            letter.setCurrentEnemy();
            Application.LoadLevel("battle");
        }
    }

    public void pickTarget()
    {
        var info = Game.Instance.PlayerInfo;
        if (info.targetsIds.Count == info.complete.Count)
        {
            Application.LoadLevel("finishScreen"); 
            return;
        }
        var target = info.targetsIds[info.complete.Count];
        Debug.Log("current target is " + target);

        withLetter.SetActive(true);
        withoutLetter.SetActive(false);

        letter.setTargetInfo(target);
    }

    // Use this for initialization
    void Start()
    {
        Game.Instance.Manager = this;
    }

    // Update is called once per frame
    void Update()
    {


    }



}
