using UnityEngine;
using UnityEngine.AI;

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
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                squad.UpgadeTarget(m_HitInfo.point);
        }
    }
}
