using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVent : MonoBehaviour
{
    private PlayerStateMachine playerState;
    private GameObject useBtn;
    private Transform eyes;
    private PlayerMessages msgPlayers;
    private PlayerUse pUse;
    private CharacterController cController;
    private Transform eyesMiddle;
    private List<GameObject> vents = new List<GameObject>();

    private float timeToVent = 0.75f;
    private float distanceForVent = 5;
    private float currentDistance = 5;
    private bool venting;

    private void Start()
    {
        eyes = transform.Find("Eyes");
        msgPlayers = GetComponent<PlayerMessages>();
        playerState = GetComponent<PlayerStateMachine>();
        cController = GetComponent<CharacterController>();
        eyesMiddle = transform.Find("LeanPoints").transform.Find("EyesMiddle");
        pUse = GetComponent<PlayerUse>();

        vents = GameObject.FindGameObjectsWithTag("Vents").ToList();
    }
    void Update()
    {

    }

    private IEnumerator MoveToVent(Transform vent)
    {
        float time = 0;
        Vector3 startPos = transform.position, targetPos = vent.position;

        while ((transform.position - targetPos).magnitude > 0.1)
        {
            transform.position = Vector3.Slerp(startPos, targetPos, (time += Time.deltaTime) / timeToVent);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator UseVent(Transform vent)
    {
        /*
        if (inVent)
        {
            exitingVent = true;
            StartCoroutine(MoveToVent(eyesMiddle, true));
            while (!finishedInterpolating)
                yield return new WaitForEndOfFrame();
            exitingVent = false;
            currentVentGroup = -1;
            currentVentIndexInGroup = -1;
            inVent = false;

            playerState.InputDisabled = false;
        }
        else
        {
            inVent = true;

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
        */
        yield return new WaitForEndOfFrame();
    }
}