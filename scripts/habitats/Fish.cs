using Godot;
using System;

public class Fish : Habitat
{
	public Fish() {
		atlasCoord = new Vector2(3,0);
		name = "Fish";
        score = 1;
		discoveryTitle = "A fish has moved in! +1 for each neighbouring water.";
	}

	public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
        if (atlasCoord == new Vector2(2,0)) {
            // TODO: Add to score;
            score++;
        }
		return atlasCoord;
	}
}
