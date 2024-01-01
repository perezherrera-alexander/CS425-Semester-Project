using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public PlayerData playerData;
    public ValidWorlds validWorlds;

    public void StartWorld1 ()
    {
        string WorldName = "World1";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld2 ()
    {
        string WorldName = "World2";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld3 ()
    {
        string WorldName = "World3";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld4 ()
    {
        string WorldName = "World4";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld5 ()
    {
        string WorldName = "World5";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld6 ()
    {
        string WorldName = "World6";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld7 ()
    {
        string WorldName = "World7";
        validWorlds.SelectingValidWorlds(WorldName);
    }

    public void StartWorld8 ()
    {
        string WorldName = "World8";
        validWorlds.SelectingValidWorlds(WorldName);
    }
}
