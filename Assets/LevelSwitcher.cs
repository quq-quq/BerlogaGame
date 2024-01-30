using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public static void SwitchScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
