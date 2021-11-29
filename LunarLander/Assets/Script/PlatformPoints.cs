using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class PlatformPoints : MonoBehaviour
{
    [SerializeField] private Point point;
    public GameObject Text;
    private int MoltiplicationPoint;
    private int PointBase = 50;
    private string[] PointText = { "x2", "x3", "x5" };
    public enum Point
    {
        Pointx2,
        Pointx3,
        Pointx4,
        Pointx5,
    }

    // Start is called before the first frame update
    void Start()
    {
        point = (Point)UnityEngine.Random.Range(0, 3);
        switch (point)
        {
            case Point.Pointx2:
                if (Text) Text.GetComponent<TextMeshProUGUI>().text = "x2";
                MoltiplicationPoint = 2;
                break;
            case Point.Pointx3:
                if (Text) Text.GetComponent<TextMeshProUGUI>().text = "x3";
                MoltiplicationPoint = 3;
                break;
            case Point.Pointx4:
                if (Text) Text.GetComponent<TextMeshProUGUI>().text = "x4";
                MoltiplicationPoint = 4;
                break;
            case Point.Pointx5:
                if (Text) Text.GetComponent<TextMeshProUGUI>().text = "x5";
                MoltiplicationPoint = 5;
                break;

        }
    }

    public void AddScore()
    {
        PointBase = PointBase * MoltiplicationPoint;
        ManagerGame.managerGame.DisplayScore(PointBase);
        ManagerGame.managerGame.Victory(PointBase);
    }

}
