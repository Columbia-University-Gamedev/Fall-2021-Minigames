using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private Board board;
    private BoardFiller filler;

    // Start is called before the first frame update
    void Start() {
        Initialize();
        FillBoard();
        RenderBoard();
    }

    private void Initialize() {
        board = GetComponent<Board>();
        filler = new BoardFiller(board);
    }

    private void FillBoard() {    //Just an example, change this later
        Vector2 v1 = new Vector2(-3.0f, -3.0f);
        Vector2 v2 = new Vector2(3.0f, 3.0f);
        board = filler.GroundRectangleFill(v1, v2);
    }

    private void RenderBoard() {
        board.RenderBoard();
    }

}
