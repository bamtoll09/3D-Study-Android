using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dpad : MonoBehaviour
{
    public GameObject leftPadImage;
    public GameObject upPadImage;
    public GameObject downPadImage;
    public GameObject rightPadImage;
    public PlayerMovement playerMovement;

    Canvas m_Canvas;
    GraphicRaycaster m_GraphicRaycaster;
    PointerEventData m_PointerEventData;
    bool m_Inputting;

    // Start is called before the first frame update
    void Start()
    {
        m_Canvas = GetComponent<Canvas>();
        m_GraphicRaycaster = GetComponent<GraphicRaycaster>();
        m_PointerEventData = new PointerEventData(null);
    }

    // Update is called once per frame
    void Update()
    {
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_GraphicRaycaster.Raycast(m_PointerEventData, results);
        m_Inputting = false;

        if (results.Count > 0)
        {
            if (results[0].gameObject == leftPadImage)
            {
                playerMovement.Goto(MovementDirection.LEFT);
                m_Inputting = true;
            }
            else if (results[0].gameObject == upPadImage)
            {
                playerMovement.Goto(MovementDirection.UP);
                m_Inputting = true;
            }
            else if (results[0].gameObject == downPadImage)
            {
                playerMovement.Goto(MovementDirection.DOWN);
                m_Inputting = true;
            }
            else if (results[0].gameObject == rightPadImage)
            {
                playerMovement.Goto(MovementDirection.RIGHT);
                m_Inputting = true;
            }
        }
        if (!m_Inputting)
        {
            playerMovement.Stop();
        }
    }
}
