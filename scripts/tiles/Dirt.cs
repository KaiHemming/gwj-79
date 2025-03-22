using Godot;
using System;

public class Dirt : Tile
{
	private int numWater = 0;
	public Dirt() {
		atlasCoord = Vector2.Zero;
		name = "Dirt";
	}

	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == new Vector2(2,0)) { // water
			numWater++;
			if (numWater >= 2) {
				return new Vector2(3,0); // marsh
			}
		}
		return atlasCoord;
	}
}
