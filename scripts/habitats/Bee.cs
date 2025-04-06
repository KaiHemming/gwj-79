using Godot;
using System;

public class Bee : Habitat
{
	public Bee() {
		atlasCoord = new Vector2(1,0);
		name = "Bee";
        score = 1;
		discoveryAddition = 1;
		discoveryTitle = "A Bee has moved in, flowers bloom! +1 point";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
        if (atlasCoord == new Vector2(1,0)) {
            return new Vector2(5,0);
        }
		return atlasCoord;
	}
}
