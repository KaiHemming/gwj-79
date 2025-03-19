using Godot;
using System;

public class Fox : Habitat
{
	public Fox() {
		atlasCoord = Vector2.Zero;
		name = "Fox";
		score = 0;
	}

	public virtual Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}
