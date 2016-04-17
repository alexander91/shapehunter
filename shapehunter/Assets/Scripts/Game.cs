using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Game
{
    Player player = new Player();

    CabinetLevelManager manager;

    public CabinetLevelManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    private static Game instance;
    private Game()
    { }
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }
    }

    public Player PlayerInfo
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    public XmlNode animalNode(string animal)
    {
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.

        //xmlDoc.Load("Assets/data/buildings.xml"); // load the file.
        TextAsset PrnFile;

        PrnFile = Resources.Load("animals") as TextAsset;


        xmlDoc.LoadXml(PrnFile.text); // load the file.


        XmlNode doc = xmlDoc.GetElementsByTagName("animals")[0];
        XmlNode node = doc.SelectSingleNode(animal);

        return node;
    }

    public XmlNode targetNode(string target)
    {
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.

        //xmlDoc.Load("Assets/data/buildings.xml"); // load the file.
        TextAsset PrnFile;

        PrnFile = Resources.Load("targets") as TextAsset;


        xmlDoc.LoadXml(PrnFile.text); // load the file.

        XmlNode doc = xmlDoc.GetElementsByTagName("targets")[0];
        XmlNode node = doc.SelectSingleNode(target);

        return node;
    }

    internal void ResetPlayer()
    {
        player = new Player();
    }
}
