using Godot;
public class LandRequirement
{
	public PackedScene tileScene;
	public Vector2 atlasCoord;
	public int numRequired;
	public LandRequirement(PackedScene tileScene, int numRequired) {
		this.tileScene = tileScene;
		this.numRequired = numRequired;
		atlasCoord = ((Tile)tileScene.Instance()).atlasCoord;
	}
}
