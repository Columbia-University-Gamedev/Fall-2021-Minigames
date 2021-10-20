using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFiller
{
    private Board Board {get; set;}

    public BoardFiller(Board board) {
        this.Board = board;
    }

    public Board GroundRectangleFill(Vector3 corner1, Vector3 corner2) {
        for (float i = corner1.x; i < corner2.x; i++) {
            for (float j = corner1.y; j < corner2.y; j++) {
                Board.AddGroundTile(new Vector2(i, j));
            }
        }
        return Board;
    }

}
