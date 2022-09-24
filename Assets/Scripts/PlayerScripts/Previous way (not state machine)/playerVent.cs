using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerVent : NetworkBehaviour
{
    public bool movingVents;
    public bool finishedInterpolating = true;
    public float timeToVent = 0.75f;
    public bool inVent;
    private playerState playerState;
    private GameObject useBtn;
    private Transform player;
    private Transform eyes;
    private messagesBetweenPlayers msgPlayers;
    private playerUse pUse;
    private CharacterController cController;
    private Transform eyesMiddle;
    private playerLean playerLean;
    private List<Transform> vents = new List<Transform>();
    private float distanceForVent = 5;
    private float currentDistance;
    private string currentVentName;
    private int currentVentGroup = -1;
    private int currentVentIndexInGroup = -1;
    private bool exitingVent;
    private void Start()
    {
        player = transform;
        useBtn = transform.Find("PlayerCanvas/UseButton").gameObject;
        eyes = transform.Find("Eyes");
        msgPlayers = GetComponent<messagesBetweenPlayers>();
        playerState = GetComponent<playerState>();
        cController = GetComponent<CharacterController>();
        eyesMiddle = transform.Find("LeanPoints").transform.Find("EyesMiddle");
        playerLean = GetComponent<playerLean>();
        pUse = GetComponent<playerUse>();

        currentDistance = distanceForVent;
        foreach (Transform child in GameObject.FindGameObjectWithTag("Vents").transform)
        {
            vents.Add(child);
        }
    }
    void Update()
    {        
        if (inVent)
        {
            if (Input.GetKeyDown(KeyCode.D) && !exitingVent && !movingVents)
            {
                if (currentVentGroup == 1 || currentVentGroup == 4)
                {
                    if (currentVentIndexInGroup == 3)
                        currentVentIndexInGroup = 1;
                    else
                        currentVentIndexInGroup++;
                }
                else
                {
                    if (currentVentIndexInGroup == 2)
                        currentVentIndexInGroup = 1;
                    else
                        currentVentIndexInGroup++;
                }
                foreach (Transform ventToMoveTo in vents)
                    if (ventToMoveTo.name == currentVentGroup + "" + currentVentIndexInGroup)
                        StartCoroutine(moveToVent(ventToMoveTo));
            }
            else if (Input.GetKeyDown(KeyCode.A) && !exitingVent && !movingVents)
            {
                if (currentVentGroup == 1 || currentVentGroup == 4)
                {
                    if (currentVentIndexInGroup == 1)
                        currentVentIndexInGroup = 3;
                    else
                        currentVentIndexInGroup--;

                }
                else
                {
                    if (currentVentIndexInGroup == 1)
                        currentVentIndexInGroup = 2;
                    else
                        currentVentIndexInGroup--;
                }
                foreach (Transform ventToMoveTo in vents)
                    if (ventToMoveTo.name == currentVentGroup + "" + currentVentIndexInGroup)
                        StartCoroutine(moveToVent(ventToMoveTo));
            }
        }

        foreach (Transform vent in vents)
        {
            float distance = Vector3.Distance(player.position, new Vector3(vent.position.x, player.position.y, vent.position.z));
            
            if (distance < currentDistance)
            {
                useBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Buttons/ventButton");
                pUse.useButton.interactable = true;
                pUse.currentUseable = vent;
                currentVentName = vent.name;
                currentDistance = distance;
            }
            else if (distance > distanceForVent && currentVentName == vent.name)
            {
                useBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Buttons/useButton");
                pUse.useButton.interactable = false;
                pUse.currentUseable = null;
                currentDistance = distanceForVent;
            }
        }
    }
    IEnumerator moveToVent(Transform ventMoveTo)
    {
        movingVents = true;
        GameObject blackTransitionIn = Instantiate(Resources.Load<GameObject>("VentTransition/blackCanvas"));
        ventTransition ventTransitionIn = blackTransitionIn.transform.GetChild(0).GetComponent<ventTransition>();
        Animator animIn = blackTransitionIn.transform.GetChild(0).GetComponent<Animator>();
        ventTransitionIn.fadeOut = false;
        animIn.enabled = true;
        while (!ventTransitionIn.finished)
            yield return new WaitForEndOfFrame();

        cController.enabled = false;
        player.position = new Vector3(ventMoveTo.position.x, player.position.y, ventMoveTo.position.z);
        cController.enabled = true;
        eyes.position = new Vector3(ventMoveTo.position.x, eyes.position.y, ventMoveTo.position.z);

        Destroy(blackTransitionIn);
        GameObject blackTransitionOut = Instantiate(Resources.Load<GameObject>("VentTransition/blackCanvas"));
        ventTransition ventTransitionOut = blackTransitionOut.transform.GetChild(0).GetComponent<ventTransition>();
        Animator animOut = blackTransitionOut.transform.GetChild(0).GetComponent<Animator>();
        ventTransitionOut.fadeOut = true;
        animOut.enabled = true;
        while (!ventTransitionOut.finished)
            yield return new WaitForEndOfFrame();

        Destroy(blackTransitionOut);
        movingVents = false;
    }
    public IEnumerator useVent(Transform vent)
    {
        if (inVent)
        {
            exitingVent = true;
            StartCoroutine(moveToPosSpherically(eyesMiddle, true));
            while (!finishedInterpolating)
                yield return new WaitForEndOfFrame();
            exitingVent = false;
            currentVentGroup = -1;
            currentVentIndexInGroup = -1;
            inVent = false;
            playerLean.inVent = false;

            playerState.inputDisabled = false;
        }
        else
        {
            inVent = true;
            playerLean.inVent = true;

            StartCoroutine(moveToPosSpherically(vent.GetChild(0), false));
            while (!finishedInterpolating)
                yield return new WaitForEndOfFrame();
            
            cController.enabled = false;
            player.position = new Vector3(vent.position.x, player.position.y, vent.position.z);
            eyes.position = vent.GetChild(0).position;
            cController.enabled = true;
            
            currentVentGroup = int.Parse(vent.name.Substring(0,1));
            currentVentIndexInGroup = int.Parse(vent.name.Substring(1,1));
        }
    }
    IEnumerator moveToPosSpherically(Transform posTo, bool visible)
    {
        finishedInterpolating = false;
        if (visible)
        {
            msgPlayers.skinnedMeshRendererEnable(true);
            msgPlayers.TextMeshProEnable(true);
        }
        Vector3 posFrom = eyes.position;
        if (!visible)
            playerState.ChangeState(playerState.runNoTransitionState);

        float CurrentVX = 0, CurrentVZ = 0;
        float AvgAccelerationX = 2 * (posTo.position.x - transform.position.x) / Mathf.Pow(timeToVent, 2);
        float AvgAccelerationZ = 2 * (posTo.position.z - transform.position.z) / Mathf.Pow(timeToVent, 2);
        for (float time = 0; time <= timeToVent; time += Time.deltaTime)
        {
            eyes.position = Vector3.Slerp(posFrom, posTo.position, time / timeToVent);
            cController.Move(new Vector3(CurrentVX * Time.deltaTime, 0, CurrentVZ * Time.deltaTime));
            if (time <= timeToVent / 2)
            {
                CurrentVX += 2 * AvgAccelerationX * Time.deltaTime;
                CurrentVZ += 2 * AvgAccelerationZ * Time.deltaTime;
            }
            else
            {
                CurrentVX -= 2 * AvgAccelerationX * Time.deltaTime;
                CurrentVZ -= 2 * AvgAccelerationZ * Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }

        playerState.ChangeState(playerState.idleState);

        if (!visible)
        {
            msgPlayers.skinnedMeshRendererEnable(false);
            msgPlayers.TextMeshProEnable(false);
        }
        finishedInterpolating = true;
    }
}