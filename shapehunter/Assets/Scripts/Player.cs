using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Player
{
    public class CompletedStuff
    {
        public bool showedPrehistory;
    }

    public CompletedStuff additionalProgress = new CompletedStuff();

    public List<string> targetsIds = new List<string> { "bear" };
    public List<string> complete = new List<string>();

    public string currentEnemy;

}

