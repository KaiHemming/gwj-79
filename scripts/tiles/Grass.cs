using Godot;
using System;

public class Grass : Tile
{
	int numWater = 0;
	public Grass() {
		name = "Grass";
		score = 2;
		atlasCoord = new Vector2 (1,0);
		discoveryTitle = "You discovered grass!";
	}

	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == new Vector2(2,0)) { // water
			if (numWater >= 2) {
				return new Vector2(3,0); // marsh
			}
		}
		return atlasCoord;
	}
}
