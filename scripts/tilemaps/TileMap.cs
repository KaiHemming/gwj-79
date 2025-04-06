using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileMap : Godot.TileMap
{
	public int xDiff = 34; // difference  between x size and x custom transform
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
	private Sprite sprite;
	private Camera2D camera2D;

	public override void _Ready()
	{
		tilesDiscovered.Add(Vector2.Zero); //dirt
		tilesDiscovered.Add(new Vector2(2,0)); //water
		ui = GetParent().GetNode<UI>("UI");
		sprite = ui.GetNode<Sprite>("Sprite");
		camera2D = GetParent().GetNode<Camera2D>("Camera2D");
	}

	public override void _Process(float delta)
	{
		var pos = GetMousePosition();
		// ui.GetNode<Label>("Position").Text = 
		// 		"Pos:" + pos + 
		// 		" Hovered tile:" + GetCellAutotileCoord((int) pos.x, (int) pos.y);
				// " Zoom: " + camera2D.Zoom +
				// " Cur bag tile: " + sprite.curTile.GetName() +
				// " Bag size: " + sprite.bag.Count;
	}

	public Vector2 GetMousePosition() {
		var mousePos = GetLocalMousePosition();
		
		var squareY = (mousePos.y-xDiff)/CellSize.x;
		var squareX = mousePos.x/CellSize.y;

		mousePos.y -= 45;
		mousePos.x -= xDiff;

		return this.WorldToMap(mousePos);
	}

	public Tile GetBestTile(Vector2 pos, Tile tile) {
		var neighbouringLand = CountNeighboursOfType(pos);
		var neighbouringIcons = GetParent().GetNode<IconTileMap>("IconTileMap").CountNeighboursOfType(pos);
		var bestTile = tile;

		GD.Print("Position: "+ pos + " Tile " + tile.name);

		if (!TileHandler.requirementMapping.ContainsKey(bestTile.atlasCoord)) return tile;
		var mappings = TileHandler.requirementMapping[bestTile.atlasCoord];

		GD.Print("Found mappings for Position: "+ pos + " Tile " + tile.name);

		// Output tile, Land requirements, icon requirements
		foreach((Tile, LandRequirement[], IconRequirement[]) tileRequirement in mappings) {
			Boolean satisfiedLandRequirements = true;
			Boolean satisfiedIconRequirements = true;

			foreach (LandRequirement landRequirement in tileRequirement.Item2) {
				if (!neighbouringLand.ContainsKey(landRequirement.atlasCoord)) {
					satisfiedLandRequirements = false;
					break;
				}
				if (neighbouringLand[landRequirement.atlasCoord] < landRequirement.numRequired) {
					satisfiedLandRequirements = false;
					break;
				}
			}
			if (!satisfiedLandRequirements) continue;

			foreach (IconRequirement iconRequirement in tileRequirement.Item3) {
				if (!neighbouringIcons.ContainsKey(iconRequirement.atlasCoord)) {
					satisfiedIconRequirements = false;
					break;
				}
				if (neighbouringIcons[iconRequirement.atlasCoord] < iconRequirement.numRequired) {
					satisfiedIconRequirements = false;
					break;
				}
			}
			if (!satisfiedIconRequirements) continue;

			GD.Print("Found suitable new tile " + tileRequirement.Item1.name);

			if (tileRequirement.Item1.score > bestTile.score) {
				GD.Print("Confirmed higher score true");
				bestTile = tileRequirement.Item1;
			}
		}
		GD.Print("Returning best tile: " + bestTile.name);
		return bestTile;
	}
	// Places a tile at pos
	public virtual void PlaceTile(Vector2 pos, Tile tile) {
		GD.Print("\nPlacing " + tile.name);
		var bestTile = GetBestTile(pos, tile);

		if (!tilesDiscovered.Contains(bestTile.atlasCoord)) {
			// GD.Print("Tile: " + bestTile.atlasCoord);
			// PrintTilesDiscovered();
			GetParent().GetParent().GetNode<Sprite>("Sprite").tileDiscovered(bestTile);
			tilesDiscovered.Add(bestTile.atlasCoord);
		}

		SetCellv(pos, 0, false, false, false, bestTile.GetAtlasCoord());
		CheckForHabitat(pos);
		ui.AddScore(bestTile.score);

		Vector2[] updates;
		if (pos.x%2 == 0) {
			updates = sameColNeighbours.Concat(evenRowNeighbours).ToArray();
		} else {
			updates = sameColNeighbours.Concat(oddRowNeighbours).ToArray();
		}
		foreach (Vector2 update in updates) {
			UpdateNeighbour(pos.x+update.x, pos.y+update.y, bestTile);
		}
		
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
			var location = new Vector2((int) x, (int) y);
			var selectedTileType = GetCellAutotileCoord((int) x, (int) y);
			if (selectedTileType == availableTileType) return tile.GetAtlasCoord();
			
			var selectedTile = (Tile)TileHandler.GetTileScene(selectedTileType).Instance();

			var bestTile = GetBestTile(location, selectedTile);
			if (bestTile.atlasCoord != selectedTileType) {
				CheckForHabitat(location);
				SetCellv(location, 0, false, false, false, bestTile.atlasCoord);
				ui.AddScore(bestTile.score - selectedTile.score);

				if (!tilesDiscovered.Contains(bestTile.atlasCoord)) {
					ui.GetNode<Sprite>("Sprite").tileDiscovered(bestTile);
					tilesDiscovered.Add(bestTile.atlasCoord);
				}
				return bestTile.atlasCoord;
			}
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
			if (IsEmpty(curLocation)) {
				continue;
			}
			var curAtlasCoord = GetCellAutotileCoord((int) curLocation.x, (int) curLocation.y);
			if (countedNeighbours.ContainsKey(curAtlasCoord)) {
				countedNeighbours[curAtlasCoord]++;
				continue;
			}
			countedNeighbours.Add(curAtlasCoord, 1);
		}
		GD.Print("Printing neighbours found");
		foreach (KeyValuePair<Vector2, int> countedNeighbour in countedNeighbours) {
			GD.Print(countedNeighbour);
		}
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
	public bool IsEmpty(Vector2 pos) {
		if (GetCell((int) pos.x, (int) pos.y) == TileMap.InvalidCell) {
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
			ui.DeductScore(habitat.score);
			iconTileMap.SetCellv(pos, TileMap.InvalidCell);
		}
	}

	public Vector2 GetTileGlobalPosition(Vector2 tilePos) {
		var localPosition = MapToWorld(tilePos);
		var globalPosition = ToGlobal(localPosition);
		return globalPosition;
	}

	// public void setCell(int x, int y, int tileType) {
	// 	SetCell(x, y, tileType);
	// 	GetCell(x, y).
	// }
}
