﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    // 큐를 저장할 변수
    private Queue<Transform> playerQueue = new Queue<Transform>();

    // 플레이어의 프리팹 (큐에 추가할 대상)
    public Transform playerPrefab;

    // 줄서기 포인트
    public Transform[] points;

    public static GuestManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
        }
    }

    void Update()
    {
        // 키보드 입력 등을 통해 새로운 플레이어를 큐에 추가하는 예시
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnqueuePlayer();
        }

        // 큐에서 플레이어를 제거하는 예시
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DequeuePlayer();
        }
    }

    // 큐에 플레이어 추가
    void EnqueuePlayer()
    {
        if (playerQueue.Count < points.Length) 
        {
            Transform newPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            playerQueue.Enqueue(newPlayer);
            UpdatePlayerPositions();
        }
    }

    // 큐에서 플레이어 제거
    void DequeuePlayer()
    {
        if (playerQueue.Count > 0)
        {
            Transform dequeuedPlayer = playerQueue.Dequeue();
            Destroy(dequeuedPlayer.gameObject);
            UpdatePlayerPositions();
        }
    }

    // 플레이어들의 위치를 업데이트
    void UpdatePlayerPositions()
    {
        int positionIndex = 0;
        foreach (Transform player in playerQueue)
        {
            //player.position = points[positionIndex].position;
            player.GetComponent<GuestMovement>().currentIndex = positionIndex;
            positionIndex++;
        }
    }
}
