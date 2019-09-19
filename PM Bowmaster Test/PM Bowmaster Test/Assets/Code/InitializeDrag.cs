using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeDrag : MonoBehaviour
{
    #region Variables
    private Vector3 m_DragOffset;
    private Vector2 m_StartPosition;
    private Vector2 m_CurrentPosition;
    public delegate void ReleaseDragDelegate();
    public static event ReleaseDragDelegate ReleaseDragEvent;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        m_StartPosition = transform.position;
        transform.position = m_StartPosition;
    }

    private void OnMouseDown()
    {
        m_DragOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Cursor.visible = false;
    }

    private void OnMouseDrag()
    {
        var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        m_CurrentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + m_DragOffset;
        m_CurrentPosition.x = Mathf.Clamp(m_CurrentPosition.x, -3f, 3f);
        m_CurrentPosition.y = Mathf.Clamp(m_CurrentPosition.y, -3f, 3f);
        transform.position = m_CurrentPosition;
    }

    private void OnMouseUp()
    {
        Cursor.visible = true;

        if (ReleaseDragEvent != null)
        {
            ReleaseDragEvent.Invoke();
        }
        transform.position = m_StartPosition;
    }
    #endregion
}
