using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LetterManager : MonoBehaviour
{
    [SerializeField]
    UILabel targetName;
    [SerializeField]
    UILabel targetMainDescription;
    [SerializeField]
    UILabel targetAdditionalDescription;

    [SerializeField]
    UITexture targetPhoto;

    string target;

    internal void setTargetInfo(string target)
    {
        var tnode = Game.Instance.targetNode(target);
        targetName.text = tnode.Attributes["name"].Value;
        targetMainDescription.text = tnode.SelectSingleNode("description").InnerText;
        targetAdditionalDescription.text = tnode.SelectSingleNode("descriptionAdd").InnerText;

        this.target = target;
    }

    public void setCurrentEnemy()
    {
        Game.Instance.PlayerInfo.currentEnemy = target;
    }
}

