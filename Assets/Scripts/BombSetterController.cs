using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSetterController : MonoBehaviour
{
    // Start is called before the first frame update

    public PigPlayer pigPlayer;
    public GameObject bomb;
    private GameObject taskLabel;
    private const string taskLabelName = "TaskLabel";

    void Start()
    {
        taskLabel = GameObject.Find(taskLabelName);
        taskLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0) & pigPlayer.hasBombs == true)
        {
            if ((Input.mousePosition - transform.position).magnitude < 50)
            {
                Instantiate(bomb, pigPlayer.transform.position, pigPlayer.transform.rotation);
                bomb.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (pigPlayer.hasBombs == true)
        {
            taskLabel.SetActive(true);
        }
    }

}
