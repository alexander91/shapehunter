using UnityEngine;
using System.Collections;

public class MoneyCreator : MonoBehaviour {

    public GameObject prefab;

    // Use this for initialization
    void Start () {

        var textures = GetComponentsInChildren<UITexture>();


        for (int i = Game.Instance.PlayerInfo.money; i < textures.Length; ++i)
        {
            textures[i].gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
