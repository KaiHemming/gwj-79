using Godot;
public class IconRequirement
{
	public Vector2 atlasCoord;
	public int numRequired;
	public IconRequirement(Vector2 iconAtlasCoord, int numRequired) {
		this.atlasCoord = iconAtlasCoord;
		this.numRequired = numRequired;
	}
}
