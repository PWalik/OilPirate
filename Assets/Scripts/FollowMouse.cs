using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector3 mousePosition;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float distance;
    [SerializeField]
    float minDistance;
    [SerializeField]
    float critDistance;
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    LineController line;
    RaycastHit hit;
    Ray ray;
    Vector3 tar;
    float y;
    GoOnCd cd;

    private void Start()
    {
        y = transform.position.y;
        cd = GetComponent<GoOnCd>();
    }

    void Update()
    {
        if (!cd.isOnCD)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float dis = Vector3.Distance(hit.point, playerTransform.position);

                if (dis < critDistance)
                {
                    line.DisableLine();
                    cd.SetDisabled(true);
                    return;
                }
                if(cd.IsDisabled)
                cd.SetDisabled(false);

                if (dis <= distance && dis >= minDistance)
                {
                    tar = hit.point;
                }
                else if(dis > distance)
                {
                    float xm, ym;
                    if (hit.point.x - playerTransform.position.x <= 0)
                        xm = -1;
                    else xm = 1;
                    if (hit.point.z - playerTransform.position.z <= 0)
                        ym = -1;
                    else ym = 1;

                    float x = Mathf.Abs(hit.point.x - playerTransform.position.x);
                    float y = Mathf.Abs(hit.point.z - playerTransform.position.z);
                    float newX = playerTransform.position.x + (x / dis) * distance * xm;
                    float newZ = playerTransform.position.z + y / dis * distance * ym;
                    tar = new Vector3(newX, 0, newZ);

                }
                else
                {
                    float x = hit.point.x - playerTransform.position.x;
                    float z = hit.point.z - playerTransform.position.z;
                    float r = Mathf.Sqrt(x * x + z * z);
                    float ratio = minDistance/r;
                    tar = new Vector3(hit.point.x * ratio, 0, hit.point.z * ratio);
                }
                line.RecalculateLine(tar);

                transform.position = new Vector3(tar.x, y, tar.z);
            }
        }
        else line.DisableLine();
    }

}
