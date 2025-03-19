using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class IconTileMap : TileMap
{
	private TileMap tileMap;

	public override void _Ready()
	{
		tileMap = GetParent().GetNode<TileMap>("TileMap");
	}
	public override void PlaceTile(Vector2 pos, Tile tile) {
		Habitat bestHabitat = new Fox();
		var neighbouringLand = tileMap.CountNeighboursOfType(pos);
		var neighbouringIcons = CountNeighboursOfType(pos);
		GD.Print("Printing neighbouring land found");
		foreach (KeyValuePair<Vector2, int> countedNeighbour in neighbouringLand) {
			GD.Print(countedNeighbour);
		}
		GD.Print("Printing neighbouring icons found");
		foreach (KeyValuePair<Vector2, int> countedNeighbour in neighbouringIcons) {
			GD.Print(countedNeighbour);
		}

		foreach ((Habitat, LandRequirement[], IconRequirement[]) habitatRequirement in HabitatHandler.requirementMapping) {
			Boolean satisfiedLandRequirements = true;
			Boolean satisfiedIconRequirements = true;

			foreach (LandRequirement landRequirement in habitatRequirement.Item2) {
				if (!neighbouringLand.ContainsKey(landRequirement.atlasCoord)) satisfiedLandRequirements = false;
				if (neighbouringLand[landRequirement.atlasCoord] < landRequirement.numRequired) satisfiedLandRequirements = false;
			}
			if (!satisfiedLandRequirements) continue;

			foreach (IconRequirement iconRequirement in habitatRequirement.Item3) {
				if (!neighbouringIcons.ContainsKey(iconRequirement.atlasCoord)) satisfiedIconRequirements = false;
				if (neighbouringIcons[iconRequirement.atlasCoord] < iconRequirement.numRequired) satisfiedIconRequirements = false;
			}
			if (!satisfiedIconRequirements) continue;

			if (habitatRequirement.Item1.score > bestHabitat.score) {
				bestHabitat= habitatRequirement.Item1;
			}
		}
		SetCell((int) pos.x,(int) pos.y, 0, false, false, false, bestHabitat.atlasCoord);
		
		Vector2[] updates;
		if (pos.x%2 == 0) {
			updates = sameColNeighbours.Concat(evenRowNeighbours).ToArray();
		} else {
			updates = sameColNeighbours.Concat(oddRowNeighbours).ToArray();
		}
		foreach (Vector2 update in updates) {
			UpdateNeighbour(pos.x+update.x, pos.y+update.y, tile);
		}
	}
	// Changed to update neighbours on regular tilemap
	public override Vector2 UpdateNeighbour(float x, float y, Tile tile) {
		if (tileMap.IsTile((int) x, (int) y)) {
			return Vector2.Zero;
		}
		var selectedTileType = tileMap.GetCellAutotileCoord((int) x, (int) y);
		if (selectedTileType == new Vector2(1,5)) return Vector2.Zero;
		
		var newTileType = tile.GetUpdatedTile(selectedTileType);
		tileMap.SetCell((int) x,(int) y, 0, false, false, false, newTileType);

		return Vector2.Zero;
	}
	public override bool IsAvailable(Vector2 pos) {
		if (GetCell((int) pos.x, (int) pos.y) == TileMap.InvalidCell) {
			return true;
		}
		return false;
	}
}
