using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLesson : Singleton<GameplayManager>
{
    Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnMouseUp()
    {
        PauseOnMouseUp();
    }
     
    void OnMouseDrag()
    {
        PauseOnMouseDrag(); 
    }

    private void PauseOnMouseUp()
    {
        m_rigidbody.simulated = true;
    }

    private void PauseOnMouseDrag()
    {
        m_rigidbody.simulated = false;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }
}
