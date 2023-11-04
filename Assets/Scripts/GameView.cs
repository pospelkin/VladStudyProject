using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameView : MonoBehaviour
{
    public CubeBehaviour[,] blocks;
    const int size = 4;
    [SerializeField] GameObject blockPrefab;
    public Canvas canvas;
    private float startX = -569f;
    private float startY = 569f;
    private float scaleX;
    private float scaleY;  
    private Vector3 lastMousePosition;
    private Vector3 initialMousePosition;
    public AudioManager audioManager;

    public VictoryMenu victoryMenu;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        blocks = new CubeBehaviour[size, size];
        SetUpBlocks();
        ShuffleMatrix(blocks);
        Debug.Log(scaleX);
    }


    private void SetUpBlocks()
    {
        int totalBlocks = size * size - 1;
        int currentBlockNumber = 1;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (currentBlockNumber <= totalBlocks)
                {
                    Vector3 startPosition = transform.position + new Vector3(startX, startY, 0f);
                    GameObject newBlock = Instantiate(blockPrefab);
                    newBlock.transform.SetParent(canvas.transform, false);
                    RectTransform blockRectTransform = newBlock.GetComponent<RectTransform>();
                    SpriteRenderer spriteRenderer = newBlock.GetComponent<SpriteRenderer>();
                    Vector3 scale = spriteRenderer.transform.localScale;
                    scaleX = scale.x + 7;
                    scaleY = scale.y + 7;


                    if (blockRectTransform != null)
                    {
                        Vector3 blockPos = startPosition + new Vector3(col * scaleX, -row * scaleY, 0f);
                        blockRectTransform.anchoredPosition = blockPos;
                        CubeBehaviour cubeBehaviour = newBlock.GetComponent<CubeBehaviour>();
                        blocks[row, col] = cubeBehaviour;
                        int cubeNumber = row * size + col + 1;
                        cubeBehaviour.winPositionX = row;
                        cubeBehaviour.winPositionY = col;

                        cubeBehaviour.cubeNumber.text = cubeNumber.ToString();
                        currentBlockNumber++;
                        cubeBehaviour.cubeIndex = cubeNumber;

                    }
                }
            }
        }
        ShuffleMatrix(blocks);
    }

    public void ShuffleMatrix(CubeBehaviour[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int randomRow = Random.Range(0, rows);
                int randomCol = Random.Range(0, columns);

                CubeBehaviour tempCube = matrix[row, col];
                matrix[row, col] = matrix[randomRow, randomCol];
                matrix[randomRow, randomCol] = tempCube;

                UpdateCubePosition(matrix[row, col], row, col);   
                UpdateCubePosition(matrix[randomRow, randomCol], randomRow, randomCol);
            }
        }
    }






    void Update()
    {
        PauseMenui pauseMenu = GameObject.Find("CanvasUI").GetComponent<PauseMenui>();
        

        if (pauseMenu != null && !pauseMenu.isPaused && !victoryMenu.isWined)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 currentMousePosition = Input.mousePosition;
                lastMousePosition = currentMousePosition;
                Vector3 mouseDelta = lastMousePosition - initialMousePosition;
                float blockSize = scaleX;

                if (mouseDelta.magnitude > blockSize * 0.2)
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(currentMousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                    if (hit.collider != null)
                    {
                        GameObject clickedObject = hit.collider.gameObject;
                        CubeBehaviour clickedCube = clickedObject.GetComponent<CubeBehaviour>();
                        int positionX = clickedCube.positionX;
                        int positionY = clickedCube.positionY;
                        Vector3 direction = mouseDelta.normalized;


                        if (direction.x >0 && positionY < size - 1 && blocks[positionX, positionY + 1] == null)
                        {
                            SwapBlocks(positionX, positionY, positionX, positionY + 1);
                            //CheckWinPosition();
                        }
                        else if (direction.x <0 && positionY > 0 && blocks[positionX, positionY - 1] == null)
                        {
                            SwapBlocks(positionX, positionY, positionX, positionY - 1);
                            //CheckWinPosition();
                        }
                        else if (direction.y >0 && positionX > 0 && blocks[positionX - 1, positionY] == null)
                        {
                            SwapBlocks(positionX, positionY, positionX - 1, positionY);
                            //CheckWinPosition();
                        }
                        else if (direction.y < 0 && positionX < size - 1 && blocks[positionX + 1, positionY] == null)
                        {
                            SwapBlocks(positionX, positionY, positionX + 1, positionY);
                            //CheckWinPosition();
                        }

                    }
                }
            }
        }
    }

    public void CheckWinPosition()
    {
        if (CheckWinCondition())
        {
            TimeManager timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
            timeManager.SaveCurrentTime();
            Debug.Log("VICTORY");
            victoryMenu.Setup(); 
        }
    }






    public bool CheckEmptySpace(int row, int col)
    {
        if (row > 0 && blocks[row - 1, col] == null)
        {
            SwapBlocks(row, col, row - 1, col);
            return true;           
        }
        if (row < size - 1 && blocks[row + 1, col] == null)
        {
            SwapBlocks(row, col, row + 1, col);
            return true;
        }
        if (col > 0 && blocks[row, col - 1] == null)
        {
            SwapBlocks(row, col, row, col -1);
            return true;
        }
        if (col < size - 1 && blocks[row, col + 1] == null)
        {
            SwapBlocks(row, col, row, col+1);
            return true;
        }
        return false;
    }

    private void UpdateCubePosition(CubeBehaviour cube, int row, int col)
    {
        if (cube != null)
        {
            Vector3 blockPos = new Vector3(startX + col * scaleX, startY - row * scaleY, 0f);
            cube.transform.localPosition = blockPos;
            cube.positionX = row;
            cube.positionY = col;
        }
    }

    IEnumerator Move(CubeBehaviour cube, int row, int col)
    {
        float elapsedTime = 0;
        float duration = 0.2f;
        Vector2 start = cube.gameObject.transform.localPosition;
        Vector2 end = new Vector2(startX + col * scaleX, startY - row * scaleY);
        cube.transform.localPosition = end;
        cube.positionX = row;
        cube.positionY = col;
        audioManager.PlaySFX(audioManager.swipe);
        while (elapsedTime < duration)
        {
            cube.gameObject.transform.localPosition = Vector2.Lerp(start, end, (elapsedTime/duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cube.gameObject.transform.localPosition = end;
        CheckWinPosition();
    }
    private void SwapBlocks(int row1, int col1, int row2, int col2)
    {

        if (row1 >= 0 && row1 < size && col1 >= 0 && col1 < size && row2 >= 0 && row2 < size && col2 >= 0 && col2 < size &&
            blocks[row1, col1] != null && blocks[row2, col2] == null)
        {
            CubeBehaviour tempCube = blocks[row1, col1];
            blocks[row1, col1] = blocks[row2, col2];  
            blocks[row2, col2] = tempCube;

            StartCoroutine(Move(tempCube, row2, col2));
        }
    }

    public bool CheckWinCondition()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (blocks[row, col] != null && !blocks[row, col].WinPosition())
                {
                    return false;
                }
            }
        }
        return true; 
    }
}