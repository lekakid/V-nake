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
    public float InputDelay = 0.12f;
    public float SpawnDelay = 0.12f;

    Vector2 _defaultPos;
    Vector2 _defaultBushPos;
    Queue<Vector2> _inputBuffer = new Queue<Vector2>();
    Vector2 _lastInput;
    Vector2 _walkDirection;
    Vector2 _lastTailPos;
    List<Character> _tail = new List<Character>();
    List<Vector2> _tailPositions = new List<Vector2>();

    bool _adding = false;

    void Awake()
    {
        _tail.Add(Head);
        _tailPositions.Add((Vector2)Head.transform.position);
        _defaultPos = Head.transform.position;
        _defaultBushPos = Bush.transform.position;
    }

    void Start() {
        Play();
    }

    void Update()
    {
        if(GameManager.Instance.ViewState != GameManager.ViewStateType.PLAY)
            return;
            
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(x, (x == 0) ? y: 0);
        bool check = (_lastInput.x + input.x) != 0 || (_lastInput.y + input.y) != 0;

        if(input != Vector2.zero && _lastInput != input && check && _inputBuffer.Count < 3) {
            _inputBuffer.Enqueue(input);
            _lastInput = input;
        }
    }

    IEnumerator SnakeMove() {
        while(true) {
            yield return new WaitUntil(() => !_adding);

            _walkDirection = (_inputBuffer.Count > 0) ? _inputBuffer.Dequeue() : _walkDirection;

            Vector2 NextPos = _tailPositions[0] + _walkDirection;
            _tailPositions.Insert(0, NextPos);
            _lastTailPos = _tailPositions[_tailPositions.Count - 1];
            _tailPositions.RemoveAt(_tailPositions.Count - 1);

            if(_tailPositions[0].x > BorderLimit.position.x || _tailPositions[0].x < 0 || 
               _tailPositions[0].y > BorderLimit.position.y || _tailPositions[0].y < 0) {
                Head.GetComponent<Animator>().SetBool("isDefeat", true);
                Stop();
                GameManager.Instance.GameOver();
            }

            for(int i = 4; i < _tailPositions.Count; i++) {
                if(_tailPositions[0] == _tailPositions[i]) {
                    Head.GetComponent<Animator>().SetBool("isDefeat", true);
                    Stop();
                    GameManager.Instance.GameOver();
                }
            }

            for(int i = 0; i < _tailPositions.Count; i++) {
                _tail[i].Walk(_tailPositions[i], InputDelay);
            }
            yield return new WaitForSeconds(InputDelay-0.01f);

            if(_tailPositions[0] == (Vector2)Bush.position) {
                AddTail();
                MoveBush();
            }
        }
    }

    public void AddTail() {
        _adding = true;

        SpawnController spawner = GameManager.Instance.SpawnController;

        Character tail = spawner.SpawnCharacter();
        tail.transform.SetParent(transform);
        tail.transform.position = Bush.position;
        tail.Spawn(500 - spawner.TotalRescueCount);
        
        _tail.Add(tail);
        _tailPositions.Add(_lastTailPos);

        _adding = false;

        SoundManager.Instance.PlaySFX("Bush");
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
        Stop();
        
        _lastInput = Vector2.zero;
        _walkDirection = Vector2.zero;
        _inputBuffer.Clear();

        for(int i = 1; i < _tail.Count; i++) {
            _tail[i].Remove();
        }
        _tail.Clear();
        _tailPositions.Clear();

        Head.transform.position = _defaultPos;
        _tail.Add(Head);
        _tailPositions.Add(_defaultPos);
        Head.GetComponent<Animator>().SetBool("isDefeat", false);
        Head.CancelWalk();

        Bush.transform.position = _defaultBushPos;
        
        Play();
    }

    public void Play() {
        StartCoroutine("SnakeMove");
    }

    public void Stop() {
        StopCoroutine("SnakeMove");
    }
}
