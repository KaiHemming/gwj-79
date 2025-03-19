using Godot;
using System.Collections.Generic;

public class HabitatHandler : Node
{
	// Habitats	
	static Fox fox = new Fox();

	private static Dictionary<Habitat, Requirement[]> requirementMapping = new Dictionary<Habitat, Requirement[]>{
		{ fox, new Requirement[] { new Requirement(TileHandler.grassScene, 2)}}
	};
	
	// TODO:
	public static Habitat GetHabitat(Dictionary<Vector2, int> countedNeighboursOfType, Tile curTile) {
		return null;
	}
}
