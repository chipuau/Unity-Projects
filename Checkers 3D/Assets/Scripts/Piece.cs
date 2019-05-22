using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public Tile currentTile = null; 
    public Tile moveLeft = null;
    public Tile moveRight = null;
    public Tile jumpLeft = null;
    public Tile jumpRight = null;

    public bool isKing = false;
    
    void Update()
    {
        SetCurrentTile();
        FindMoves(); 
    }

    public void SetCurrentTile()
    {
        //Find the tile the piece is on: 
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var found = hit.transform;

            if (found.CompareTag("Tile"))
            {
                currentTile =  found.GetComponent<Tile>(); 
            }
        }

    }

    public void FindMoves()
    {
        if (tag == "PlayerPiece")
        {
            moveLeft = currentTile.leftMove;
            moveRight = currentTile.rightMove;

            if (moveLeft)
            {
                jumpLeft = moveLeft.leftMove;
            }
            
            if (moveRight)
            {
                jumpRight = moveRight.rightMove;
            }
        }

        else if (tag == "EnemyPiece")
        {
            moveLeft = currentTile.leftMoveDown;
            moveRight = currentTile.rightMoveDown;

            if (moveLeft)
            {
                jumpLeft = moveLeft.leftMoveDown;
            }
            
            if (moveRight)
            {
                jumpRight = moveRight.rightMoveDown;
            }
           
        }
    }

    public void ChangeMaterial(Material material)
    {
        Renderer pieceRenderer = GetComponent<Renderer>();

        if (pieceRenderer != null)
        {
            pieceRenderer.material = material;
        }
    }

  
    public void UpdatePosition(Vector3 position)
    {
        transform.position = position; 
    }

}
