using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonTemp : MonoBehaviour
{
    [SerializeField] LevelChanger levelChanger;
    public void backToMenu()
    {
        levelChanger.changeLevel("MainMenu");
    }
}
