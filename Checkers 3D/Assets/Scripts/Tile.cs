using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    //Stores left and right options: 
    public Tile leftMove = null;
    public Tile rightMove = null;
    public Tile leftMoveDown = null;
    public Tile rightMoveDown = null;

    //Booleans to check if a tile can be moved to: 
    public bool isMovable = false; 
 
	// Use this for initialization
	void Start ()
    {
        FindMoveTiles();
    }
	
    public void FindMoveTiles()
    {
        //Find point to raycast from: 
        Vector3 diagonalRight = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z + 1);
        Vector3 diagonalLeft = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z + 1);
        Vector3 diagonalRightDown = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z - 1);
        Vector3 diagonalLeftDown = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z - 1);

        rightMove = SetMoveTiles(diagonalRight);
        leftMove = SetMoveTiles(diagonalLeft);
        rightMoveDown = SetMoveTiles(diagonalRightDown);
        leftMoveDown = SetMoveTiles(diagonalLeftDown);
    }

    public Tile SetMoveTiles(Vector3 rayPosition)
    {
        RaycastHit hit;

        //Find right option: 
        if (Physics.Raycast(rayPosition, Vector3.down, out hit, 1))
        {
            var found = hit.transform;

            if (found.CompareTag("Tile"))
            {
                return found.GetComponent<Tile>();
            }
        }

        return null; 
    }

    public void ChangeMaterial(Material material)
    {
        Renderer tileRenderer = GetComponent<Renderer>();
        
        if (tileRenderer != null)
        {
            tileRenderer.material = material;
        }
        
    }

    public void SetMovable(bool val)
    {
        isMovable = val;
    }

    public bool CheckIfBlocked()
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            return false; 
        }

        return true; 
    }

    public Piece GetPiece()
    {
        //Find the piece on the tile: 
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            var found = hit.transform;

            if (found.CompareTag("PlayerPiece") || found.CompareTag("EnemyPiece"))
            {
                return found.GetComponent<Piece>();
            }
        }

        return null;
    }

    public void ResetTile(Material material)
    {
        SetMovable(false);
        ChangeMaterial(material);
    }

}
