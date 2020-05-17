using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMO : MenuOption
{
    // Start is called before the first frame update
    public override void enterOption()
    {
        //We load the level scene for now
        SceneManager.LoadScene("Level1");
    }
}
