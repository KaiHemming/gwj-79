using Godot;
using System;

public class TileMap : Godot.TileMap
{
	private int xDiff = 10; // difference  between x size and x custom transform

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		var pos = GetMousePosition();
		GetParent().GetParent().GetNode("UI")
			.GetNode<Label>("Position").Text = 
				"" + pos + " " + GetCellAutotileCoord((int) pos.x, (int) pos.y);
	}

	// https://www.redblobgames.com/grids/hexagons/
	//function axial_to_oddq(hex):
		//var col = hex.q
		//var row = hex.r + (hex.q - (hex.q&1)) / 2
		//return OffsetCoord(col, row)

	//function oddq_to_axial(hex):
		//var q = hex.col
		//var r = hex.row - (hex.col - (hex.col&1)) / 2
		//return Hex(q, r)

	public Vector2 GetMousePosition() {
		var mousePos = GetLocalMousePosition();
		//GD.Print("mousePos " + mousePos);
		
		var squareY = (mousePos.y-xDiff)/CellSize.x;
		var squareX = mousePos.x/CellSize.y;
		//GD.Print("squareX " + squareX);
		//GD.Print("squareY " + squareY);
		//GD.Print("col odd/even " + Math.Floor(squareX%2));
		
		if (Math.Floor(squareX%2) == 0) {
			mousePos.y = mousePos.y - (CellSize.y+xDiff)/2;
		} else {
			mousePos.y = mousePos.y - CellSize.y+xDiff;
		}
		
		mousePos.x = mousePos.x + CellSize.x/4; // This is innacurate
		
		//GD.Print("mousePos after" + mousePos);
		//GD.Print();
		return this.WorldToMap(mousePos);
	}
}
