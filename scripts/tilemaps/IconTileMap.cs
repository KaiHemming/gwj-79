using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class IconTileMap : TileMap
{
	public Dictionary<Tile, int> CountIconNeighbours(Vector2 location) {
		Dictionary<Tile, int> countedIconNeighbours = new Dictionary<Tile, int>();
		Vector2[] neighbourLocations;
		if (location.x%2 == 0) {
			neighbourLocations = sameColNeighbours.Concat(evenRowNeighbours).ToArray();
		} else {
			neighbourLocations = sameColNeighbours.Concat(oddRowNeighbours).ToArray();
		}
		foreach (Vector2 neighbourLocation in neighbourLocations) {
			var curLocation = new Vector2(location.x+neighbourLocation.x, location.y+neighbourLocation.y);
			if (IsAvailable(curLocation)) {
				continue;
			}
			var curAtlasCoord = GetCellAutotileCoord((int) curLocation.x, (int) curLocation.y);
			// if (countedIconNeighbours.ContainsKey(curAtlasCoord)) {
			// 	countedIconNeighbours[curAtlasCoord]++;
			// 	continue;
			// }
			// countedNeighbours.Add(curAtlasCoord, 1);
		}
		return countedIconNeighbours;
	}
}
