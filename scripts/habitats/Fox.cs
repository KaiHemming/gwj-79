using Godot;
using System;

public class Fox : Habitat
{
	public Fox() {
		atlasCoord = Vector2.Zero;
	}

	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}
