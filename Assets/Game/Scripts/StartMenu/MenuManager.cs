using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public AudioClip changeOptionSound;
    public AudioClip enterOptionSound;
    public AudioClip verticalScrollSound;

    public List<MenuOption> menuOptions = new List<MenuOption>();
    MenuOption currentMenuOption;
    public int menuOptionIndex;

    //Learn sounds stuff
    public int verticalOptionIndex = 0;
    public LearnSoundsMO learnSounds;

    //Menu controlls
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKet = KeyCode.D;

    public KeyCode enterKey = KeyCode.Return;

    public KeyCode repeatKey = KeyCode.R;

    //For the singleton stuff
    public static MenuManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        currentMenuOption = menuOptions[menuOptionIndex];
        currentMenuOption.playDescription();
    }

    public bool learnMode = false;

    // Update is called once per frame
    void Update()
    {
        if (!learnMode)
        {
            if (Input.GetKeyDown(leftKey)) moveLeft();
            if (Input.GetKeyDown(rightKet)) moveRight();
            if (Input.GetKeyDown(enterKey)) enterOption();
            if (Input.GetKeyDown(repeatKey)) currentMenuOption.playDescription();
        }
        else
        {
            //Learn mode activated so we iterate vertically throught the options
            if (Input.GetKeyDown(upKey)) moveUp();
            if (Input.GetKeyDown(downKey)) moveDown();
            if (Input.GetKeyDown(enterKey)) enterLearnOption();
            if (Input.GetKeyDown(repeatKey)) learnSounds.playOption(verticalOptionIndex);
        }



    }

    public void moveUp()
    {
        if (verticalOptionIndex < learnSounds.soundDescriptions.Count - 1)
        {
            verticalOptionIndex++;
            learnSounds.playSound(verticalScrollSound);
            learnSounds.playOption(verticalOptionIndex);
        }

    }

    public void moveDown()
    {
        if (verticalOptionIndex > 0)
        {
            verticalOptionIndex--;
            learnSounds.playSound(verticalScrollSound);
            learnSounds.playOption(verticalOptionIndex);
        }
    }

    public void moveLeft()
    {
        if (menuOptionIndex > 0)
        {
            menuOptionIndex--;
            currentMenuOption = menuOptions[menuOptionIndex];
            currentMenuOption.playSound(changeOptionSound);
            currentMenuOption.playDescription();
        }
    }

    public void enterLearnOption()
    {
        learnSounds.enterLearnOption(verticalOptionIndex);
    }
    public void exitLearnOption()
    {
        learnMode = false;
        currentMenuOption.playDescription();
    }

    public void moveRight()
    {
        if (menuOptionIndex < menuOptions.Count - 1)
        {
            menuOptionIndex++;
            currentMenuOption = menuOptions[menuOptionIndex];
            currentMenuOption.playSound(changeOptionSound);
            currentMenuOption.playDescription();
        }

    }

    public void enterOption()
    {
        currentMenuOption.playSound(enterOptionSound);
        currentMenuOption.enterOption();
    }

    public void learnOptionMenu()
    {
        learnMode = true;

    }

}
