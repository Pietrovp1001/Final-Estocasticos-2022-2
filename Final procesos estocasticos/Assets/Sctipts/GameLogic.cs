using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameLogic : MonoBehaviour
{
    public IList<int> simonList, userList;
    public Animator[] animators;
    public TextMeshProUGUI roundText, loserText, retryButton, simontext, usertext;
    public Image retryImage, sadFace;
    public GameObject win;
    public Button retry;
    public int round = 1;
    public List<GameObject> buttons = new List<GameObject>();
    private Array arraysimon, arrayuser;
    private string stringsimon, stringuser;
    
   
    
    
    private int i, randomNum, interval = 1, count = 0, y;
    private bool simonIsPlaying = true, checkValues = false;
    



    void Start()
    {
        StartCoroutine("newRound");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            userList.Add(0);
            action(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            userList.Add(1);
            action(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            userList.Add(2);
            action(2);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            userList.Add(3);
            action(3);
        }


        if (userList.Count > round)
        {
            Debug.Log("Perdiste!");
            retryImage.enabled = true;
            retryButton.enabled = true;
            retry.enabled = true;
            Destroy(buttons[0]);
            Destroy(buttons[1]);
            Destroy(buttons[2]);
            Destroy(buttons[3]);
            sadFace.enabled = true;
        }
        
        if (userList.Count == simonList.Count)
        {
            StartCoroutine(nameof(CheckLists));

            if (checkValues)
            {
                for (int y = 0; y < simonList.Count; y++)
                {
                    if(simonList[y] != userList[y])
                    {
                        count++;
                    }
                    else if (userList[y] == simonList[y])
                    {
                        Debug.Log("Correct");
                    }
                }

                if (count == 0)
                {
                    Debug.Log("Proximo!");
                    win.SetActive(true);
                    stringsimon = simonList.ToString();
                    stringuser = userList.ToString();
                    simontext.text = stringsimon;
                    usertext.text = stringuser;
                    Debug.Log(stringsimon);
                    Debug.Log(stringuser);

                }
                else if (count > 0 )
                {
                    Debug.Log("Perdiste!");
                    retryImage.enabled = true;
                    retryButton.enabled = true;
                    retry.enabled = true;
                    Destroy(buttons[0]);
                    Destroy(buttons[1]);
                    Destroy(buttons[2]);
                    Destroy(buttons[3]);
                    sadFace.enabled = true;
                }
            }
        }
    }

    public void nextround()
    {
        win.SetActive(false);
        roundText.text = round.ToString();
        simonIsPlaying = true;
        StartCoroutine("newRound");
    }
    void action(int id)
    {
        animators[id].SetBool("KeyPress", true);
        StartCoroutine("ChangeBool", id);
    }

    public IEnumerator newRound()
    {
        if (simonIsPlaying)
        {
            simonList = new List<int>();
            userList = new List<int>();

            for (int i = 0; i < round; i++)
            {
                randomNum = Random.Range(0, 4);
                simonList.Add(randomNum);
                action(randomNum);

                yield return new WaitForSeconds(interval);
            }

            round++;
            simonIsPlaying = false;
        }
    }

    IEnumerator ChangeBool(int x)
    {
        yield return new WaitForSeconds(1);
        animators[x].SetBool("KeyPress", false);
    }

    private IEnumerator CheckLists()
    {
        yield return new WaitForSeconds(1);
        checkValues = true;
        yield return new WaitForSeconds(1);
        checkValues = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
