using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

    public EventTrigger boostButtonTrigger;
    public EventTrigger decelerateButtonTrigger;
    public EventTrigger shootButtonTrigger;


	// Use this for initialization
	void Start ()
    {

        EventTrigger.Entry boostDown = new EventTrigger.Entry();
        boostDown.eventID = EventTriggerType.PointerDown;
        boostDown.callback.AddListener((eventData) => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().StartBoost(); });
        boostButtonTrigger.triggers.Add(boostDown);

        EventTrigger.Entry boostUp = new EventTrigger.Entry();
        boostUp.eventID = EventTriggerType.PointerUp;
        boostUp.callback.AddListener((eventData) => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().StopBoost(); });
        boostButtonTrigger.triggers.Add(boostUp);

        EventTrigger.Entry decelerateDown = new EventTrigger.Entry();
        decelerateDown.eventID = EventTriggerType.PointerDown;
        decelerateDown.callback.AddListener((eventData) => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().StartDeceleration(); });
        decelerateButtonTrigger.triggers.Add(decelerateDown);

        EventTrigger.Entry decelerateUp = new EventTrigger.Entry();
        decelerateUp.eventID = EventTriggerType.PointerUp;
        decelerateUp.callback.AddListener((eventData) => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().StopDeceleration(); });
        decelerateButtonTrigger.triggers.Add(decelerateUp);


        EventTrigger.Entry shootDown = new EventTrigger.Entry();
        shootDown.eventID = EventTriggerType.PointerDown;
        shootDown.callback.AddListener((eventData) => { GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().Shoot(); });
        shootButtonTrigger.triggers.Add(shootDown);

        EventTrigger.Entry shootUp = new EventTrigger.Entry();
        shootUp.eventID = EventTriggerType.PointerUp;
        shootUp.callback.AddListener((eventData) => {  });
        shootButtonTrigger.triggers.Add(shootUp);



    }


}
