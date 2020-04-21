using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Obejct")]
    public Transform Bush;
    public Character Head;
    public Transform BorderLimit;

    [Header("Delay")]
    public float MoveDelay = 0.12f;
    public float SpawnDelay = 0.12f;

    Vector2 _defaultPos;
    Vector2 _defaultBushPos;
    Vector2 _inputDirection;
    Vector2 _walkDirection;
    Vector2 _lastTailPos;
    List<Character> _tail = new List<Character>();
    List<Vector2> _tailPositions = new List<Vector2>();

    bool _adding = false;

    // Start is called before the first frame update
    void Start()
    {
        _tail.Add(Head);
        _tailPositions.Add((Vector2)Head.transform.position);
        _defaultPos = Head.transform.position;
        _defaultBushPos = Bush.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")) {
            GameManager.Instance.Pause();
        }

        if (Input.GetAxisRaw("Vertical") > 0 & _walkDirection != Vector2.down) {
            _inputDirection = Vector2.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0 & _walkDirection != Vector2.up) {
            _inputDirection = Vector2.down;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 & _walkDirection != Vector2.left) {
            _inputDirection = Vector2.right;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 & _walkDirection != Vector2.right) {
            _inputDirection = Vector2.left;
        }
    }

    IEnumerator SnakeMove() {
        while(true) {
            yield return new WaitUntil(() => !_adding);

            Vector2 NextPos = _tailPositions[0] + _inputDirection;
            _tailPositions.Insert(0, NextPos);
            _lastTailPos = _tailPositions[_tailPositions.Count - 1];
            _tailPositions.RemoveAt(_tailPositions.Count - 1);

            _walkDirection = _inputDirection;

            if(_tailPositions[0].x > BorderLimit.position.x || _tailPositions[0].x < 0 || 
               _tailPositions[0].y > BorderLimit.position.y || _tailPositions[0].y < 0) {
                Stop();
                Head.GetComponent<Animator>().SetBool("isDefeat", true);
                GameManager.Instance.GameOver();
            }

            for(int i = 4; i < _tailPositions.Count; i++) {
                if(_tailPositions[0] == _tailPositions[i]) {
                    Stop();
                    Head.GetComponent<Animator>().SetBool("isDefeat", true);
                    GameManager.Instance.GameOver();
                }
            }

            for(int i = 0; i < _tailPositions.Count; i++) {
                _tail[i].Walk(_tailPositions[i], MoveDelay);
            }
            yield return new WaitForSeconds(MoveDelay-0.01f);

            if(_tailPositions[0] == (Vector2)Bush.position) {
                AddTail();
                MoveBush();
            }
        }
    }

    public void AddTail() {
        _adding = true;

        Character tail = SpawnManager.Instance.SpawnCharacter().GetComponent<Character>();
        tail.transform.SetParent(transform);
        tail.transform.position = Bush.position;
        tail.Spawn(500 - SpawnManager.Instance.TotalRescueCount);
        
        _tail.Add(tail);
        _tailPositions.Add(_lastTailPos);

        _adding = false;
    }

    public void MoveBush() {
        Vector2 pos = new Vector2();

        do {
            float x = Mathf.Round(Random.Range(0, BorderLimit.position.x));
            float y = Mathf.Round(Random.Range(0, BorderLimit.position.y));

            pos.x = x;
            pos.y = y;
        } while(CheckTail(pos));
        
        Bush.transform.position = pos;
    }

    public bool CheckTail(Vector2 pos) {
        foreach (Vector2 i in _tailPositions)
        {
            if(i == pos)
                return true;
        }

        return false;
    }

    public void Init() {
        for(int i = 1; i < _tail.Count; i++) {
            Destroy(_tail[i].gameObject);
        }
        _tail.Clear();
        _tailPositions.Clear();

        Head.transform.position = _defaultPos;
        _tail.Add(Head);
        _tailPositions.Add(_defaultPos);
        Head.GetComponent<Animator>().SetBool("isDefeat", false);

        Bush.transform.position = _defaultBushPos;

        _inputDirection = Vector2.zero;
        _walkDirection = Vector2.zero;
    }

    public void Play() {
        StartCoroutine("SnakeMove");
    }

    public void Stop() {
        StopCoroutine("SnakeMove");
    }
}
