using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public TMP_Text textArea;
    public GameObject[] levels;
    public SceneManager[] scenes;
    public CooldownComponent canSwitch;
    public Material outlinedMaterial;
    public Material defaultMaterial;
    private int currentPos;
    private int lastPos;

    void Start()
    {
        //the Knight's Chapter is default selection
        textArea.text = levels[0].name;
        currentPos = 0;
        lastPos = currentPos;
        levels[0].GetComponent<SpriteRenderer>().material = outlinedMaterial;
        for(int i = 1; i < levels.Length; i++)
        {
            levels[i].GetComponent<SpriteRenderer>().material = defaultMaterial;
        }
    }

    void Update()
    {
        if (Input.GetButton("Up") && currentPos != 0 && canSwitch)
        {
            lastPos = currentPos;
            currentPos--;
            Select();
        }
        if(Input.GetButton("Down") && currentPos != levels.Length - 1 && canSwitch)
        {
            lastPos = currentPos;
            currentPos++;
            Select();
        }
        if (Input.GetButton("Interact"))
        {
            SceneManager.LoadScene(levels[currentPos].name);
        }

    }

    private void Select()
    {
        textArea.text = levels[currentPos].name;
        canSwitch.StartCooldownTimer();
        //outline chosen area and set back original material for the previous
        levels[currentPos].GetComponent<SpriteRenderer>().material = outlinedMaterial;
        levels[lastPos].GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
}
