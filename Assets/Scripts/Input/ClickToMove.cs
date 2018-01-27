using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(PlayerSquad))]
public class ClickToMove : MonoBehaviour
{
    PlayerSquad squad;
    RaycastHit m_HitInfo = new RaycastHit();

    void Start()
    {
        squad = GetComponent<PlayerSquad>();
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                squad.UpdateTarget(m_HitInfo.point);
        }
    }
}
