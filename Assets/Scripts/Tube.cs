using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] GameObject tubeBallsHolder;
    [SerializeField] Transform tubeStartPos;

    [SerializeField] GameObject selectedHolder;

    [HideInInspector] public bool isSelectedThis;

    bool isCompleted;
    

    // Start is called before the first frame update
    void Start()
    {
        isCompleted = false;
        isSelectedThis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isSelected)
        {
            if (isSelectedThis)
            {
                if (Vector3.Distance(selectedHolder.transform.GetChild(0).gameObject.transform.position, tubeStartPos.transform.position) >= 0.1f)
                {
                    Vector3 newPos = tubeStartPos.transform.position - selectedHolder.transform.GetChild(0).transform.position;
                    selectedHolder.transform.GetChild(0).transform.Translate(newPos * 10 * Time.deltaTime, Space.World);
                }
            }
        }
    }

    void CheckTube()
    {
        if(tubeBallsHolder.transform.childCount == 4)
        {
            if(tubeBallsHolder.transform.GetChild(0).gameObject.tag == tubeBallsHolder.transform.GetChild(1).gameObject.tag && 
                tubeBallsHolder.transform.GetChild(0).gameObject.tag == tubeBallsHolder.transform.GetChild(2).gameObject.tag &&
                tubeBallsHolder.transform.GetChild(0).gameObject.tag == tubeBallsHolder.transform.GetChild(3).gameObject.tag)
            {
                if (!isCompleted)
                {
                    isCompleted = true;
                    for(int i = 0; i < GameManager.instance.levelTubesCompleted.Length; i++)
                    {
                        if (GameManager.instance.levelTubesCompleted[i] == false)
                        {
                            GameManager.instance.levelTubesCompleted[i] = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isCompleted)
        {
            if (!GameManager.instance.isSelected)
            {
                if (!isSelectedThis)
                {
                    isSelectedThis = true;
                    Debug.Log(transform.parent.gameObject.name.ToString() + "Clicked");
                    Debug.Log(tubeBallsHolder.transform.GetChild(tubeBallsHolder.transform.childCount - 1).gameObject.name.ToString() + "Selected");
                    tubeBallsHolder.transform.GetChild(tubeBallsHolder.transform.childCount - 1).gameObject.transform.parent = selectedHolder.transform;
                    selectedHolder.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = false;
                    GameManager.instance.isSelected = true;
                }

            }
            else if (GameManager.instance.isSelected)
            {
                if (isSelectedThis)
                {
                    isSelectedThis = false;

                    GameManager.instance.isSelected = false;
                    selectedHolder.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                    selectedHolder.transform.GetChild(0).gameObject.transform.parent = tubeBallsHolder.transform;
                }
                else
                {
                    if (tubeBallsHolder.transform.childCount < 4)
                    {
                        GameManager.instance.isSelected = false;
                        selectedHolder.transform.GetChild(0).gameObject.transform.position = tubeStartPos.position;
                        selectedHolder.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                        selectedHolder.transform.GetChild(0).gameObject.transform.parent = tubeBallsHolder.transform;
                        GameManager.instance.ResetSelected();
                    }
                    else
                    {
                        Debug.Log("Tube is Full");
                    }
                }
                CheckTube();
                GameManager.instance.CheckGameComplete();
            }
        }
        else
        {
            Debug.Log("Tube is completed");
        }
    }
}
