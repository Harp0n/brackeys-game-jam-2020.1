using Assets.Logics;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Path : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Text lengthText;
    private Text difficultyText;
    private GameObject pathInfo;

    public Edge Edge { get; set; }

    void Start()
    {
        pathInfo = GameObject.FindGameObjectWithTag("pathInfo");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //pathInfo.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f);
        pathInfo.transform.position = Input.mousePosition;
        foreach (Transform transform in pathInfo.transform)
        {
            transform.gameObject.SetActive(true);
        }
        lengthText = pathInfo.transform.Find("length").GetComponent<Text>();
        difficultyText = pathInfo.transform.Find("difficulty").GetComponent<Text>();
        lengthText.text = Edge.Route.HowLong.ToString();
        difficultyText.text = Edge.Route.HowHard.ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Transform transform in pathInfo.transform)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameObject.FindObjectOfType<MapSelection>().Select(Edge);
    }

}
