using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMO : MenuOption
{


    public override void enterOption()
    {
        Application.Quit();
    }
}
