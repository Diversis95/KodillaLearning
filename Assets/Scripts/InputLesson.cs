using UnityEngine;

public class InputLesson : Singleton<GameplayManager>
{
    Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameplayManager.Instance.pause)
            m_rigidbody.simulated = false;
        else
            m_rigidbody.simulated = true;
    }

    void OnMouseUp()
    {
        m_rigidbody.simulated = true;
    }
     
    void OnMouseDrag()
    {
        m_rigidbody.simulated = false;

        if (GameplayManager.Instance.pause)
            return;
        else
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        } 
    }
}
