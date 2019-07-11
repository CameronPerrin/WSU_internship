﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public Puzzle puzzlePrefab;
    public GameObject winGameMenu;

    private List<Puzzle> puzzleList = new List<Puzzle>();
    private List<Vector3> puzzlePositions = new List<Vector3>();
    private List<int> randomNumbers = new List<int>();

    private Vector2 startPosition = new Vector2(-3.55f, 1.77f);
    private Vector2 offset = new Vector2(1.05f, 1.05f);

    //moving puzzle
    public LayerMask collisionMask;

    //collision
    Ray ray_up, ray_down, ray_left, ray_right;
    RaycastHit hit;
    private BoxCollider collider;
    private Vector3 collider_size;
    private Vector3 collider_center;

    public string FolderName;
    public GameObject FullPicture;

    [HideInInspector]
    public static GameStatus game_status = new GameStatus();

    // Start is called before the first frame update
    void Start()
    {
        SpawnPuzzle(7);
        setStartPosition();
        ApplyMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        switch (game_status.status)
        {
            case GameStatus.GameState.start_pressed:
                MixPuzzles();
                game_status.status = GameStatus.GameState.play;
                break;
            case GameStatus.GameState.play:
                if (HaveWeWon() == true)
                {
                    game_status.status = GameStatus.GameState.win;
                }
                MovePuzzle();
                break;
            case GameStatus.GameState.win:
                //end game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }

    private void SpawnPuzzle(int number)
    {
        for (int i = 0; i <= number; i++)
        {
            puzzleList.Add(Instantiate(puzzlePrefab, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 180.0f, 0.0f)) as Puzzle);
        }

    }
    private void setStartPosition()
    {
        //first line
        puzzleList[0].transform.position = new Vector3(startPosition.x, startPosition.y, 0.0f);
        puzzleList[1].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y, 0.0f);

        //second line
        puzzleList[2].transform.position = new Vector3(startPosition.x, startPosition.y - offset.y, 0.0f);
        puzzleList[3].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - offset.y, 0.0f);
        puzzleList[4].transform.position = new Vector3(startPosition.x + (offset.x * 2), startPosition.y - offset.y, 0.0f);

        //third line
        puzzleList[5].transform.position = new Vector3(startPosition.x, startPosition.y - (offset.y*2), 0.0f);
        puzzleList[6].transform.position = new Vector3(startPosition.x + offset.x, startPosition.y - (offset.y * 2), 0.0f);
        puzzleList[7].transform.position = new Vector3(startPosition.x + (offset.x * 2), startPosition.y - (offset.y * 2), 0.0f);
    }

    void MovePuzzle()
    {
        foreach (Puzzle puzzle in puzzleList)
        {
            puzzle.move_amount = offset;
            if (puzzle.clicked)
            {
                //rays up and down
                collider = puzzle.GetComponent<BoxCollider>();
                collider_size = collider.size;
                collider_center = collider.center;

                float move_amount = offset.x;
                float direction = Mathf.Sign(move_amount);

                //up and down rays
                float x = (puzzle.transform.position.x + collider_center.x - collider_size.x / 2) + collider_size.x / 2;
                float y_up = puzzle.transform.position.y + collider_center.y + collider_size.y / 2 * direction;
                float y_down = puzzle.transform.position.y + collider_center.y + collider_size.y / 2 * -direction;

                ray_up = new Ray(new Vector2(x, y_up), new Vector2(0, direction));
                ray_down = new Ray(new Vector2(x, y_down), new Vector2(0, -direction));

                //draw rays
                Debug.DrawRay(ray_up.origin, ray_up.direction);
                Debug.DrawRay(ray_down.origin, ray_down.direction);

                //rays left and right
                float y = (puzzle.transform.position.y + collider_center.y - collider_size.y / 2) + collider_size.y / 2;
                float x_right = puzzle.transform.position.x + collider_center.x + collider_size.x / 2 * direction;
                float x_left = puzzle.transform.position.x + collider_center.x + collider_size.x / 2 * -direction;

                ray_left = new Ray(new Vector2(x_left, y), new Vector2(-direction, 0f));
                ray_right = new Ray(new Vector2(x_right, y), new Vector2(direction, 0f));

                //draw rays
                Debug.DrawRay(ray_left.origin, ray_left.direction);
                Debug.DrawRay(ray_right.origin, ray_right.direction);

                //check collision up
                if((Physics.Raycast(ray_up, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.y < startPosition.y))
                {
                    Debug.Log("Move Up Allowed");
                    puzzle.go_up = true;
                }
                //check collision down
                if ((Physics.Raycast(ray_down, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.y > (startPosition.y - 2 * offset.y)))
                {
                    Debug.Log("Move Down Allowed");
                    puzzle.go_down = true;
                }
                //check collision left
                if ((Physics.Raycast(ray_left, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.x > startPosition.x))
                {
                    Debug.Log("Move Left Allowed");
                    puzzle.go_left = true;
                }
                //check collision right
                if ((Physics.Raycast(ray_right, out hit, 1.0f, collisionMask) == false) && (puzzle.moved == false) && (puzzle.transform.position.x < (startPosition.x + 2 * offset.x)))
                {
                    Debug.Log("Move Right Allowed");
                    puzzle.go_right = true;
                }

            }
        }
    }

    void ApplyMaterial()
    {
        string filePath;
        for (int i = 1; i <= puzzleList.Count; i++)
        {
            if (i > 2)
            {
                filePath = "Puzzles/" + FolderName + "/Cube" + (i + 1);
            }
            else
            {
                filePath = "Puzzles/" + FolderName + "/Cube" + i;
            }

            Texture2D mat = Resources.Load(filePath, typeof(Texture2D)) as Texture2D;
            puzzleList[i - 1].GetComponent<Renderer>().material.mainTexture = mat;
        }
            filePath = "Puzzles/" + FolderName + "/pic";
            Texture2D mat1 = Resources.Load(filePath, typeof(Texture2D)) as Texture2D;
            FullPicture.GetComponent<Renderer>().material.mainTexture = mat1;
    }

    void MixPuzzles()
    {
        int number;
        foreach(Puzzle p in puzzleList)
        {
            puzzlePositions.Add(p.transform.position);
        }
        foreach (Puzzle p in puzzleList)
        {
            number = Random.Range(0, puzzleList.Count);
            while (randomNumbers.Contains(number))
            {
                number = Random.Range(0, puzzleList.Count);
            }
            randomNumbers.Add(number);
            p.transform.position = puzzlePositions[number];
        }
    }

    bool HaveWeWon()
    {
        foreach(Puzzle p in puzzleList)
        {
            if(p.transform.position != p.winPosition)
            {
                return false;
            }
        }
        return true;
    }
}