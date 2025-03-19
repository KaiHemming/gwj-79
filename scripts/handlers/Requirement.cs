using Godot;
public class Requirement
{
	public PackedScene tileScene;
	public int numRequired;
	public Requirement(PackedScene tileScene, int numRequired) {
		this.tileScene = tileScene;
		this.numRequired = numRequired;
	}
}
