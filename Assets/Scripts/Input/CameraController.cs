using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour 
{
    [SerializeField]
    private float scrollStrength = 1f;
    [SerializeField]
    private float scrollDuration = 1f;

    [SerializeField]
    private bool viewportBordersEnabled = true;
    [SerializeField]
    private float viewportBorderTreshold = 0.01f;

    private bool rightClick = false;
    private Vector2 lastRightClickPos;

	// Update is called once per frame
	void Update () 
    {

        if (Input.GetMouseButton(1))
        {
            if (!rightClick)
            {
                rightClick = true;
            }
            else
            {
                MoveBy((lastRightClickPos - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }

            lastRightClickPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //MoveTo(new Vector3(mouseWorldPos.x, mouseWorldPos.y, this.transform.localPosition.z));
        }
        else
        {
            rightClick = false;

            Vector2 mouseViewportPos = viewportBordersEnabled ? 
                (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition) : new Vector2(0.5f, 0.5f);

            bool left = Input.GetKey(KeyCode.LeftArrow) || mouseViewportPos.x < viewportBorderTreshold;
            bool right = Input.GetKey(KeyCode.RightArrow) || mouseViewportPos.x > 1f - viewportBorderTreshold;
            bool up = Input.GetKey(KeyCode.UpArrow) || mouseViewportPos.y > 1f - viewportBorderTreshold;
            bool down = Input.GetKey(KeyCode.DownArrow) || mouseViewportPos.y < viewportBorderTreshold;

            if (left || right || up || down)
            {
                MoveBy(new Vector2(left ? -scrollStrength : (right ? scrollStrength : 0f), 
                        up ? scrollStrength : (down ? -scrollStrength : 0f)));
            }
        }
	}

    void MoveBy(Vector2 shift)
    {
        MoveTo(this.gameObject.transform.localPosition + (Vector3)shift);
    }

    void MoveTo(Vector3 target)
    {
        DOTween.Kill("CameraTween");
        this.gameObject.transform
            .DOLocalMove(target, scrollDuration)
            .SetId("CameraTween");
    }

}
