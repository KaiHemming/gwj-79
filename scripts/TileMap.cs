using Godot;
using System;
using System.Collections.Generic;

public class TileMap : Godot.TileMap
{
	private int xDiff = 10; // difference  between x size and x custom transform
	private Vector2 availableTileType = new Vector2(1,5);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		var pos = GetMousePosition();
		GetParent().GetParent().GetNode("UI")
			.GetNode<Label>("Position").Text = 
				"" + pos + 
				" " + GetCellAutotileCoord((int) pos.x, (int) pos.y) +
				" Scale: " + GetParent<Camera2D>().Scale;
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
		
		//mousePos.x = mousePos.x + CellSize.x/4; // TODO: This is innacurate
		
		//GD.Print("mousePos after" + mousePos);
		//GD.Print();
		return this.WorldToMap(mousePos);
	}
	// Places a tile at pos
	public void PlaceTile(Vector2 pos, Tile tile) {
		HashSet<Vector2> potentialPlacements = new HashSet<Vector2>();
		potentialPlacements.Add(UpdateNeighbour(pos.x, pos.y+1, tile));
		potentialPlacements.Add(UpdateNeighbour(pos.x, pos.y-1, tile));
		
		if (pos.x%2 == 0) {
			potentialPlacements.Add(UpdateNeighbour(pos.x-1, pos.y-1, tile));
			potentialPlacements.Add(UpdateNeighbour(pos.x-1, pos.y, tile));
			potentialPlacements.Add(UpdateNeighbour(pos.x+1, pos.y-1, tile));
			potentialPlacements.Add(UpdateNeighbour(pos.x+1, pos.y, tile));
		} else {
			potentialPlacements.Add(UpdateNeighbour(pos.x-1, pos.y+1, tile));
			potentialPlacements.Add(UpdateNeighbour(pos.x-1, pos.y, tile));
			potentialPlacements.Add(UpdateNeighbour(pos.x+1, pos.y+1, tile));
			potentialPlacements.Add(UpdateNeighbour(pos.x+1, pos.y, tile));
		}
		var bestTile = tile;
		foreach (Vector2 potentialPlacement in potentialPlacements) {
			var potentialTile = (Tile)TileHandler.GetTileScene(potentialPlacement).Instance();
			if (potentialTile.score > bestTile.score) bestTile = potentialTile;
		}
		SetCellv(pos, 0, false, false, false, bestTile.GetAtlasCoord());
	}
	
	// Returns true if place is empty and now available
	public Vector2 UpdateNeighbour(float x, float y, Tile tile) {
		if (IsTile((int) x, (int) y)) {
			SetCell((int) x,(int) y, 0, false, false, false, availableTileType);
		}
		else {
			var selectedTileType = GetCellAutotileCoord((int) x, (int) y);
			if (selectedTileType == new Vector2(1,5)) return tile.GetAtlasCoord();
			
			var newTileType = tile.GetUpdatedTile(selectedTileType);
			SetCell((int) x,(int) y, 0, false, false, false, newTileType);
			// Check if adjacent tile would change current tile being placed
			return ((Tile)TileHandler.GetTileScene(newTileType).Instance()).GetUpdatedTile(tile.GetAtlasCoord());
		}
		return tile.GetAtlasCoord(); // inefficient
	}
	
	// Returns true if pos is an available tile 
	public bool IsAvailable(Vector2 pos) {
		var selectedTileType = GetCellAutotileCoord((int) pos.x, (int) pos.y);
		if (selectedTileType == availableTileType) {
			return true;
		}
		return false;
	}
	
	public bool IsTile(int x, int y) {
		if (GetCell(x, y) == -1) {
			return true;
		}
		return false;
	}
}
