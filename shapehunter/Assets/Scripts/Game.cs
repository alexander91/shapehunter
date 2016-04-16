using UnityEngine;
using System.Collections;

public class Game
{

    GameManager manager;

    public GameManager Manager
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

}
