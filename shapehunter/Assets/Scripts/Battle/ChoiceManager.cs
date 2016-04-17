using UnityEngine;
using System.Collections;

public class ChoiceManager : MonoBehaviour {

    public delegate void AnimalChosen(string animal);
    public event AnimalChosen onAnimalChoiceMade; 

    [SerializeField]
    GameObject grid;

    [SerializeField]
    UITexture zeroChoice;
    [SerializeField]
    UITexture firstChoice;
    [SerializeField]
    UITexture secondChoice;
    [SerializeField]
    UITexture thirdChoice;

    UITexture[] textureArr = new UITexture[4];

    public void pickZero()
    {
        makeChoice(0);
    }
    public void pickOne()
    {
        makeChoice(1);
    }
    public void pickTwo()
    {
        makeChoice(2);
    }
    public void pickThree()
    {
        makeChoice(3);
    }

    void makeChoice(int i)
    {
        grid.SetActive(false);

        if (onAnimalChoiceMade != null)
        {
            onAnimalChoiceMade(animalChoices[i]);
        } else
        {
            Debug.Log("no one listens to me");
        }
    }

    public string picturePathByAnimalName(string animalName)
    {
        var animal = Game.Instance.animalNode(animalName);
        return "art/animals/" + animal.Attributes["picture"].Value;
    }

    string[] animalChoices;

    public void activateWithChoices(string[] choices)
    {
        animalChoices = choices;

        for (int i = 0; i < choices.Length; ++i)
        {
            textureArr[i].mainTexture = Resources.Load(picturePathByAnimalName(choices[i])) as Texture2D;
        }
    }

    // Use this for initialization
    void Start () {
        textureArr[0] = zeroChoice;
        textureArr[1] = firstChoice;
        textureArr[2] = secondChoice;
        textureArr[3] = thirdChoice;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
