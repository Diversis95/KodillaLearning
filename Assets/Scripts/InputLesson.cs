using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLesson : MonoBehaviour
{
    Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
    }

    private void OnMouseDrag()
    {
        m_rigidbody.simulated = false;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
    }
}
