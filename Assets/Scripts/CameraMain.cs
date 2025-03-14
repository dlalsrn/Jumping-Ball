using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offSet; // 카메라와 Player 사이의 방향 백터

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offSet = playerTransform.position - transform.position;
    }

    private void LateUpdate()
    {
        // Player 위치에서 offSet을 빼면 초기 각도로 항상 Player를 바라봄
        transform.position = playerTransform.position - offSet;
    }
}
