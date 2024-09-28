/* 
 * author : jiankaiwang (2017/12)
 *          elyx (2024/09)
 * description : The script provides you with basic operations 
 *               of first personal camera look on mouse moving.
 *
 * Update: Fixed Issues. Added 'maxRotationAngle' variable.
 *
 * platform : Unity 2021.1.22f1
 * date : 2024/09
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    public GameObject character;
    private Vector2 mouseLook;
    private Vector2 smoothV;

    public float maxRotationAngle = 60.0f;

    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.x = Mathf.Clamp(mouseLook.x, -maxRotationAngle, maxRotationAngle);
        mouseLook.y = Mathf.Clamp(mouseLook.y, -89f, 89f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
