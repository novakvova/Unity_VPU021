using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]

    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedmult = 0.5f;

    private GameObject[] cloudeInstances;

    private void Awake()
    {
        // ������� ������ ��� �������� ���� ����������� �������
        cloudeInstances = new GameObject[numClouds];
        // ����� ������������ ������� ������ CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        // ������� � ����� �������� ���������� �������
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // ������� ��������� cloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefab);
            // ������� �������������� ��� ������
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMin.y);
            cPos.y = Random.Range(cloudPosMax.x, cloudPosMax.y);
            // �������������� ������
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // ������� ������ (� ������� ��������� scaleU) ������ ���� �����
            // � �����
            cPos.y = Mathf.Lerp(cloudPosMax.y, cPos.y, scaleU);
            // ������� ������ ������ ���� ������
            cPos.z = 100 - 90 * scaleU;
            // ��������� ���������� �������� ��������� � �������� � ������
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // ������� ������ �������� �� ��������� � anchor
            cloud.transform.SetParent(anchor.transform);
            // �������� ������ � ������
            cloudeInstances[i] = cloud;

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cloud in cloudeInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedmult;

            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }

            cloud.transform.position = cPos;
        }
    }
}
