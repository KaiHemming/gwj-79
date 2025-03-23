using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileMap : Godot.TileMap
{
	private int xDiff = 34; // difference  between x size and x custom transform
	public int score = 0;
	public Vector2 availableTileType = new Vector2(4,0);
	protected Vector2[] sameColNeighbours = {
		new Vector2(0,1),
		new Vector2(0,-1)
	};
	protected Vector2[] evenRowNeighbours = {
		new Vector2(-1,-1),
		new Vector2(-1,0),
		new Vector2(1,-1),
		new Vector2(1,0)
	};
	protected Vector2[] oddRowNeighbours = {
		new Vector2(-1,1),
		new Vector2(-1,0),
		new Vector2(1,1),
		new Vector2(1,0)
	};
	public HashSet<Vector2> tilesDiscovered = new HashSet<Vector2>() {};
	private UI ui;

	public override void _Ready()
	{
		tilesDiscovered.Add(Vector2.Zero); //dirt
		tilesDiscovered.Add(new Vector2(2,0)); //water
		ui = GetParent().GetParent().GetNode<UI>("UI");
	}

	public override void _Process(float delta)
	{
		var pos = GetMousePosition();
		GetParent().GetParent().GetNode("UI")
			.GetNode<Label>("Position").Text = 
				"Pos:" + pos + 
				" Hovered tile:" + GetCellAutotileCoord((int) pos.x, (int) pos.y) +
				" Scale: " + GetParent<Camera2D>().Scale +
				" Cur bag tile: " + GetParent().GetParent().GetNode<Sprite>("Sprite").curTile.GetName() +
				" Bag size: " + GetParent().GetParent().GetNode<Sprite>("Sprite").bag.Count;
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
		
		// if (Math.Floor(squareX%2) == 0) {
		// 	mousePos.y = mousePos.y - (CellSize.y+xDiff)/2;
		// } else {
		// 	mousePos.y = mousePos.y - CellSize.y+xDiff;
		// }
		
		//mousePos.x = mousePos.x + CellSize.x/4; // TODO: This is innacurate

		mousePos.y -= 45;
		mousePos.x -= xDiff;
		
		//GD.Print("mousePos after" + mousePos);
		//GD.Print();
		return this.WorldToMap(mousePos);
	}
	// Places a tile at pos
	public virtual void PlaceTile(Vector2 pos, Tile tile) {
		HashSet<Vector2> potentialPlacements = new HashSet<Vector2>();
		
		Vector2[] updates;
		if (pos.x%2 == 0) {
			updates = sameColNeighbours.Concat(evenRowNeighbours).ToArray();
		} else {
			updates = sameColNeighbours.Concat(oddRowNeighbours).ToArray();
		}
		foreach (Vector2 update in updates) {
			potentialPlacements.Add(UpdateNeighbour(pos.x+update.x, pos.y+update.y, tile));
		}
		
		var bestTile = tile;
		foreach (Vector2 potentialPlacement in potentialPlacements) {
			var potentialTile = (Tile)TileHandler.GetTileScene(potentialPlacement).Instance();
			if (potentialTile.score > bestTile.score) bestTile = potentialTile;
		}
		if (!tilesDiscovered.Contains(bestTile.atlasCoord)) {
			// GD.Print("Tile: " + bestTile.atlasCoord);
			// PrintTilesDiscovered();
			GetParent().GetParent().GetNode<Sprite>("Sprite").tileDiscovered(bestTile);
			tilesDiscovered.Add(bestTile.atlasCoord);
		}
		SetCellv(pos, 0, false, false, false, bestTile.GetAtlasCoord());
		CheckForHabitat(pos);
		ui.AddScore(bestTile.score);
		CountNeighboursOfType(pos);
	}
	public void PrintTilesDiscovered() {
		GD.Print("Printing discovered tiles");
		foreach (Vector2 tileAtlas in tilesDiscovered) {
			GD.Print(tileAtlas);
		}
		GD.Print();
	}
	
	// Returns a different tile atlasCoord if it would change type.
	public virtual Vector2 UpdateNeighbour(float x, float y, Tile tile) {
		if (IsTile((int) x, (int) y)) {
			SetCell((int) x,(int) y, 0, false, false, false, availableTileType);
		}
		else {
			// Check if adjacent tile would change current tile being placed
			var selectedTileType = GetCellAutotileCoord((int) x, (int) y);
			if (selectedTileType == availableTileType) return tile.GetAtlasCoord();

			var newTileType = tile.GetUpdatedTile(selectedTileType);
			var newTile = (Tile)TileHandler.GetTileScene(newTileType).Instance();
			// TODO: why doesn't this work
			if (newTileType == selectedTileType) return newTile.GetUpdatedTile(tile.GetAtlasCoord());

			SetCell((int) x,(int) y, 0, false, false, false, newTileType);
			CheckForHabitat(new Vector2(x, y));
			
			if (!tilesDiscovered.Contains(newTile.atlasCoord)) {
				// GD.Print("Tile: " + newTile.atlasCoord);
				// PrintTilesDiscovered();
				GetParent().GetParent().GetNode<Sprite>("Sprite").tileDiscovered(newTile);
				tilesDiscovered.Add(newTile.atlasCoord);
			}
			var selectedTile = (Tile)TileHandler.GetTileScene(selectedTileType).Instance();
			ui.AddScore(newTile.score - selectedTile.score);
			return newTile.GetUpdatedTile(tile.GetAtlasCoord());
		}
		return tile.GetAtlasCoord(); // inefficient
	}
	
	public Dictionary<Vector2, int> CountNeighboursOfType(Vector2 location) {
		Dictionary<Vector2, int> countedNeighbours = new Dictionary<Vector2, int>();
		Vector2[] neighbourLocations;
		if (location.x%2 == 0) {
			neighbourLocations = sameColNeighbours.Concat(evenRowNeighbours).ToArray();
		} else {
			neighbourLocations = sameColNeighbours.Concat(oddRowNeighbours).ToArray();
		}
		foreach (Vector2 neighbourLocation in neighbourLocations) {
			var curLocation = new Vector2(location.x+neighbourLocation.x, location.y+neighbourLocation.y);
			if (IsAvailable(curLocation)) {
				continue;
			}
			var curAtlasCoord = GetCellAutotileCoord((int) curLocation.x, (int) curLocation.y);
			if (countedNeighbours.ContainsKey(curAtlasCoord)) {
				countedNeighbours[curAtlasCoord]++;
				continue;
			}
			countedNeighbours.Add(curAtlasCoord, 1);
		}
		// GD.Print("Printing neighbours found");
		// foreach (KeyValuePair<Vector2, int> countedNeighbour in countedNeighbours) {
		// 	GD.Print(countedNeighbour);
		// }
		return countedNeighbours;
	}
	
	// Returns true if pos is an available tile 
	public virtual bool IsAvailable(Vector2 pos) {
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

	private void CheckForHabitat(Vector2 pos) {
		var iconTileMap = GetParent().GetNode<TileMap>("IconTileMap");
		var cell = iconTileMap.GetCell((int) pos.x, (int) pos.y);
		if (cell != TileMap.InvalidCell) {
			var habitat = HabitatHandler.GetHabitat(iconTileMap.GetCellAutotileCoord((int) pos.x, (int) pos.y));
			GetParent().GetParent().GetNode<UI>("UI").DeductScore(habitat.score);
			iconTileMap.SetCellv(pos, TileMap.InvalidCell);
		}
	}
}
