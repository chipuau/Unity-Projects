using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    //Materials Used in Highlighting: 
    [SerializeField]
    private Material _selectedPieceMaterial;

    [SerializeField]
    private Material _selectedTileMaterial;

    [SerializeField]
    private Material _originalPieceMaterialPlayer;

    [SerializeField]
    private Material _originalPieceMaterialEnemy;

    [SerializeField]
    private Material _originalTileMaterial;

    [SerializeField]
    private GameManager _gameManager; 

    //Booleans to help with Selection Options: 
    public bool objectSelected = false;
    public bool canSelectTile = false;

    //Stored Pieces and Tiles: 
    private Piece _currentPiece = null;
    private Tile _currentTile = null;

    private Piece _toJumpLeft = null;
    private Piece _toJumpRight = null;

    // Update is called once per frame
    void Update()
    {
        //If an object is not selected, continue to highlight hovered Pieces: 
        if (objectSelected == false)
        {
            ResetHighlights();
            HighlightPieces();
        }

        //Check if the user has made a selection: 
        CheckSelection();
    }

    //Highlights the pieces that the player controls: 
    public void HighlightPieces()
    {
        //Use the mouse to determine the hovered piece:
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var found = hit.transform;

            //If the object hit is a Piece, change the tile material and grab 
            //the current tile: 
            if (found.CompareTag("PlayerPiece") || found.CompareTag("EnemyPiece"))
            {
                _currentPiece = found.GetComponent<Piece>();
                _currentPiece.ChangeMaterial(_selectedPieceMaterial);
                _currentTile = _currentPiece.currentTile;
            }

        }
    }

    public void ResetHighlights()
    {
        //If an object is already highlighted, reset it back to default:  
        if (_currentPiece != null)
        {
            if (_currentPiece.CompareTag("PlayerPiece"))
            {
                _currentPiece.ChangeMaterial(_originalPieceMaterialPlayer);
                _currentPiece = null;
            }

            else if (_currentPiece.CompareTag("EnemyPiece"))
            {
                _currentPiece.ChangeMaterial(_originalPieceMaterialEnemy);
                _currentPiece = null;
            }
        }

        if (_currentTile != null)
        {
            Tile leftTile = _currentTile.leftMove;
            Tile rightTile = _currentTile.rightMove;
            Tile leftTileDown = _currentTile.leftMoveDown;
            Tile rightTileDown = _currentTile.rightMoveDown;

            if (leftTile)
            {
                leftTile.ResetTile(_originalTileMaterial);
                if (leftTile.leftMove)
                {
                    leftTile.leftMove.ResetTile(_originalTileMaterial);
                }

            }

            if (rightTile)
            {
                rightTile.ResetTile(_originalTileMaterial);
                if (rightTile.rightMove)
                {
                    rightTile.rightMove.ResetTile(_originalTileMaterial);
                }
            }

            if (rightTileDown)
            {
                rightTileDown.ResetTile(_originalTileMaterial);
                if (rightTileDown.rightMoveDown)
                {
                    rightTileDown.rightMoveDown.ResetTile(_originalTileMaterial);
                }
            }

            if (leftTileDown)
            {
                leftTileDown.ResetTile(_originalTileMaterial);
                if (leftTileDown.leftMoveDown)
                {
                    leftTileDown.leftMoveDown.ResetTile(_originalTileMaterial);
                }
            }

            _currentTile = null;

        }

        //Reset flag and current tile: 
        canSelectTile = false;
    }


    public void CheckSelection()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //If a piece is clicked on, mark it as selected and highlight available moves: 
            if (objectSelected == false)
            {
                objectSelected = true;
                HighlightMoves();
            }

            //If an object is selected and you can select a tile, select a tile: 
            else if (objectSelected == true && canSelectTile == true)
            { 
                SelectTile();
            }

            //If a piece is already selected, clicking again will deselect it: 
            else
            {
                objectSelected = false;
            }
        }
    }

    //Highlight Moves available to the player: 
    public void HighlightMoves()
    { 
        if (_currentPiece)
        {
            
            //If a left tile is available, check if it is blocked. If it is not blocked, then highlight it: 
            if (_currentPiece.moveLeft)
            {
                if (!_currentPiece.moveLeft.CheckIfBlocked())
                {
                    _currentPiece.moveLeft.ChangeMaterial(_selectedTileMaterial);
                    _currentPiece.moveLeft.SetMovable(true); 
                }

                else
                {
                    _toJumpLeft = _currentPiece.moveLeft.GetPiece(); 
                    if (_toJumpLeft && !_toJumpLeft.CompareTag(_currentPiece.tag))
                    {
                        if (_currentPiece.jumpLeft && !_currentPiece.jumpLeft.CheckIfBlocked())
                        {
                            _currentPiece.jumpLeft.ChangeMaterial(_selectedTileMaterial);
                            _currentPiece.jumpLeft.SetMovable(true);
                        }
                       
                    }
                }

            }

            //If a right tile is available, check if it is blocked. If it is not blocked, then highlight it: 
            if (_currentPiece.moveRight)
            {
                if (!_currentPiece.moveRight.CheckIfBlocked())
                {
                    _currentPiece.moveRight.ChangeMaterial(_selectedTileMaterial);
                    _currentPiece.moveRight.SetMovable(true); 
                }

                else
                {
                    _toJumpRight = _currentPiece.moveRight.GetPiece();

                    Debug.Log(_toJumpRight.tag); 
                    if (_toJumpRight && !_toJumpRight.CompareTag(_currentPiece.tag))
                    {
                        if (_currentPiece.jumpRight && !_currentPiece.jumpRight.CheckIfBlocked())
                        {
                            _currentPiece.jumpRight.ChangeMaterial(_selectedTileMaterial);
                            _currentPiece.jumpRight.SetMovable(true);
                        }
                        
                    }
                }

            }

            canSelectTile = true; 
        }

    }

    //Selects a tile and updates the current piece's position: 
    public void SelectTile()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Determine if an object is detected by the raycast: 
        if (Physics.Raycast(ray, out hit))
        {
            var found = hit.transform;

            //If a tile is selected and can be moved to, update the piece position to the selected
            //tile position: 
            if (found.CompareTag("Tile"))
            {
                Tile selectedTile = found.GetComponent<Tile>();

                if (selectedTile != null && selectedTile.isMovable == true)
                {   
                    if (selectedTile == _currentPiece.jumpRight)
                    {
                        Destroy(_toJumpRight.gameObject); 
                    }

                    if (selectedTile == _currentPiece.jumpLeft)
                    {
                        Destroy(_toJumpLeft.gameObject); 
                    }

                    Vector3 updatedPosition = new Vector3(selectedTile.transform.position.x, _currentPiece.transform.position.y, selectedTile.transform.position.z);
                    _currentPiece.UpdatePosition(updatedPosition);
                }

            }

            canSelectTile = false;
            objectSelected = false;
            _toJumpLeft = null;
            _toJumpRight = null;
            
            ResetHighlights();
        }
    }
}

