using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _inputDir; // Player의 움직임 벡터
    [SerializeField]
    private float sensitivity = 0.7f; // 움직임 감도

    private bool _shouldJump; // Jump를 하려고하는가
    [SerializeField]
    private float _jumpForce = 25f; 

    private bool _isFloor = false;

    private float restartPosY = -20f; // 다시 시작하는 Y 좌표

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y <= restartPosY) // 만약 떨어지면
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        _inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _inputDir = _inputDir.normalized;

        if (_isFloor && Input.GetKeyDown(KeyCode.Space)) 
        {
            _shouldJump = true;
        }

        _isFloor = Physics.Raycast(transform.position, Vector3.down, 0.7f, LayerMask.GetMask("Floor")); // Floor에 닿아있는지 확인
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.7f);
    }

    void FixedUpdate()
    {
        DoJump();
        DoMove();
    }

    private void DoJump() 
    {
        if(_shouldJump) 
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _shouldJump = false;
        }
    }

    private void DoMove()
    {
        _rigidbody.AddForce(_inputDir * sensitivity, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Flag"))
        {
            if (GameManager.Instance.Score == GameManager.Instance.TotalScore)
            {
                Debug.Log("Next Stage...");
                int totalSceneCount = SceneManager.sceneCountInBuildSettings;
                if (SceneManager.GetActiveScene().buildIndex + 1 < totalSceneCount) // 다음 스테이지가 있으면
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                Debug.Log("Please get all items...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
