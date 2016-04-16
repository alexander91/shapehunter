using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
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
