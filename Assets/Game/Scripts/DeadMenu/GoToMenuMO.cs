using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuMO : MenuOption
{
    public override void enterOption()
    {
        SceneManager.LoadScene("Menu");
    }
}
