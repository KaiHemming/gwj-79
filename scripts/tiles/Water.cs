using Godot;
using System;

public class Water : Tile
{
	public Water() {
		atlasCoord = new Vector2(6,0);
		score = 1;
	}
	
	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == new Vector2(0,3)) { // dirt
			return new Vector2(0,0); // grass
		}
		return atlasCoord;
	}
}
