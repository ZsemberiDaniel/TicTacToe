﻿using UnityEngine;
using DG.Tweening;

public class BluetoothTTTGameLogic : TTTGameLogic {

    /// <summary>
    /// Which type the server uses
    /// </summary>
    public Cell.CellOcc serverType = Cell.CellOcc.X;

    /// <summary>
    /// Where last sign was placed
    /// </summary>
    public int[] LastSignPlaced {
        get { return ((BluetoothGrid) grid).LastSignPos; }
    }
    public Cell.CellOcc LastType {
        get { return ((BluetoothGrid) grid).LastSignType; }
    }

    public override void Start() {
        grid = FindObjectOfType<BluetoothGrid>();
    }

    public override void NextTurn(int[] gridPos, out bool won) {
        bool didWin;
        base.NextTurn(gridPos, out didWin);

        // Send whose turn it is
        Bluetooth.Instance().Send(BluetoothMessageStrings.TURN_OF + "#" + WhoseTurn.ToString());

        won = didWin;
    }

    public override void AddBorderToGame(int[,] points, float[,] winLinePoints, Color color) {
        base.AddBorderToGame(points, winLinePoints, color);

        string s = BluetoothMessageStrings.ADD_BORDER + "#" + points.GetLength(0).ToString() + "#" +
            winLinePoints[0, 0].ToString() + "#" + winLinePoints[0, 1].ToString() + "#" + winLinePoints[1, 0].ToString() + "#" + winLinePoints[1, 1].ToString() + "#";
        for (int i = 0; i < points.GetLength(0); i++)
            s += points[i, 0].ToString() + "#" + points[i, 1].ToString() + "#";
        s += color.r.ToString() + "#" + color.g.ToString() + "#" + color.b.ToString();

        Bluetooth.Instance().Send(s);
    }

    public override void StartNewGame(int[] gridPos) {
        // New game is being started
        if (!GameSarted) {
            // If server is starting new turn
            if (WhoseTurn == serverType) {
                // Send jump command to client
                Bluetooth.Instance().Send("JPT#" + gridPos[0] + "#" + gridPos[1]);
            } else {
                // Client placed so jump server to pos
                JumpCameraTo(gridPos);
            }
        }

        base.StartNewGame(gridPos);
    }

    public bool IsItServersTurn() {
        return WhoseTurn == serverType;
    }

    public void JumpCameraTo(int[] pos) {
        Camera.main.transform.DOMove(new Vector3(pos[0], pos[1]), 0.5f);
    }
	
}
