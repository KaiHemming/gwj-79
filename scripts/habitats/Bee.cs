using Godot;
using System;

public class Bee : Habitat
{
	public Bee() {
		atlasCoord = new Vector2(1,0);
		name = "Bee";
        score = 1;
		discoveryTitle = "A Bee has moved in, flowers bloom!";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
        GD.Print(atlasCoord);
        if (atlasCoord == new Vector2(1,0)) {
            GD.Print("Beep");
            return new Vector2(5,0);
        }
		return atlasCoord;
	}
}
