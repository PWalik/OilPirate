using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;
    [SerializeField]
    int howManyVertices;
    PlayerShoot shoot;

    private void Start()
    {
        shoot = GetComponent<PlayerShoot>();
    }

    public void RecalculateLine(Vector3 target)
    {
        if (!line.gameObject.activeSelf)
            EnableLine();
        List<Vector3> vertices = new List<Vector3>();
        Vector3 newVert;
        float y;
        Vector3 add = (target - transform.position) / howManyVertices;
        Vector3 current = transform.position;
        float height = shoot.ThrowHeight;
        float part = Mathf.Sqrt(add.x * add.x + add.z * add.z);
        float full = part * howManyVertices;
        line.positionCount = howManyVertices + 1;
        vertices.Add(transform.position);
        for (int i = 1; i <= howManyVertices; i++)
        {
            y = transform.position.y + height * Mathf.Sin(Mathf.PI * (part * i) / full);
            newVert = new Vector3(current.x + add.x, y, current.z + add.z);
            current.x += add.x;
            current.z += add.z;
            vertices.Add(newVert);
        }
        line.SetPositions(vertices.ToArray());
    }

    public void DisableLine()
    {
        if(line.gameObject.activeSelf)
        line.gameObject.SetActive(false);
    }

    public void EnableLine()
    {
        line.gameObject.SetActive(true);
    }

}
