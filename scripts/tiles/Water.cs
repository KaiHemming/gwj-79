using Godot;
using System;

public class Water : Tile
{
	public Water() {
		atlasCoord = new Vector2(2,0);
		score = 0;
		name = "Water";
	}
	
	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == Vector2.Zero) { // dirt
			return new Vector2(1,0); // grass
		}
		return atlasCoord;
	}
}
