using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    public RectTransform boxVisual;
    Rect selectionbox;

    Vector2 start, end;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        start = Vector2.zero;
        end = Vector2.zero;
        //Start and end vectors are 0. Calling for not appearing box
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        //When mouse is clicked pin as start
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            start = Input.mousePosition;
            selectionbox = new Rect();
        }
        //When dragging keep updating end position
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            end = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
        //When releasing click buton
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            SelectUnits();
            start = Vector2.zero;
            end = Vector2.zero;
            DrawVisual();
        }
        
    }
    void DrawVisual()
    {
        Vector2 start = this.start;
        Vector2 end = this.end;

        Vector2 center = (this.start + this.end) / 2;
        boxVisual.position = center;

        boxVisual.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
        
    }
    void DrawSelection() 
    {
        //Calculation for X 
        if (Input.mousePosition.x < start.x)
        {
            //Dragging Left
            selectionbox.xMin = Input.mousePosition.x;
            selectionbox.xMax = start.x;
        }
        else
        {
            //Dragging Right
            selectionbox.xMin = start.x;
            selectionbox.xMax = Input.mousePosition.x;

        }

        //Calculation for X 
        if (Input.mousePosition.y < start.y)
        {
            //Dragging Down
            selectionbox.yMin = Input.mousePosition.y;
            selectionbox.yMax = start.y;
        }
        else
        {
            //Dragging Up
            selectionbox.yMin = start.y;
            selectionbox.yMax = Input.mousePosition.y;
        }
    }
    void SelectUnits() 
    { 
        foreach( var unit in UnitSelections.Instance.unitList)
        {
            //If unit is within the draw rectangle 
            if (selectionbox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    
    }
}
