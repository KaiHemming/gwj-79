using Godot;
using System;

public class Fox : Habitat
{
	public Fox() {
		atlasCoord = Vector2.Zero;
		name = "Fox";
		score = 0;
		discoveryTitle = "A fox has moved in!";
	}

	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}
