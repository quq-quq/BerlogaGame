using System.Collections;
using System.Collections.Generic;
using Save_files.Scripts;
using UnityEngine;

public class testSaver : MonoBehaviour
{
    void Start()
    {
        Saver.Load();
        print(Saver.Data.Levels[0].Parts[0].SceneName);
        Saver.Data.Levels[0].Parts[0].SceneName = "я гей";
        Saver.Save();
    }
}
