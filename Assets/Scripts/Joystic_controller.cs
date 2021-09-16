using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystic_controller : MonoBehaviour
{

    public PigPlayer pigPlayer;

    private GameObject touchMarker;

    private Vector3 targetVector;
    private Vector3 firstTouchPosition;
    private bool isClicked;

    

    // Start is called before the first frame update
    void Start()
    {
        touchMarker = transform.GetChild(0).gameObject;
        touchMarker.transform.position = transform.position;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if((Input.mousePosition - transform.position).magnitude < 90)
            {
                isClicked = true;
            }
        }
        if(Input.GetMouseButton(0) & isClicked)
        {
            Vector3 clampedPosition = Vector3.ClampMagnitude(Input.mousePosition - transform.position, 40);
            touchMarker.transform.position = transform.position - Vector3.ClampMagnitude(transform.position - Input.mousePosition, 90);
            pigPlayer.MooveThisPig(clampedPosition);
        }    
        if(Input.GetMouseButtonUp(0))
        {
            isClicked = false;
            touchMarker.transform.position = transform.position;
        }
    }
}
