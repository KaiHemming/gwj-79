using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
	float ZOOM_SPEED = 0.15f;
	float MOVEMENT_SPEED = 0.02f;
	float MIN_ZOOM = 0.5f;
	float MAX_ZOOM = 4f;
	public Vector2 START_POS = new Vector2(9,3);
	public TileMap tileMap;
	private Sprite sprite;

	public override void _Ready()
	{
		sprite = GetParent().GetNode("UI").GetNode<Sprite>("Sprite");
		tileMap = GetParent().GetNode<TileMap>("TileMap");
		CenterScreenOnTileMap();
	}

	public void CenterScreenOnTileMap() {
		var center = tileMap.GetTileGlobalPosition(START_POS) + (tileMap.CellSize/2 * tileMap.Scale);
		Position = center - GetViewportRect().Size/2 * Zoom;
	}
	
	public override void _Process(float delta) {
		if (Input.IsActionPressed("right_click")) {
			Vector2 movement = Input.GetLastMouseSpeed();
			movement *= MOVEMENT_SPEED * Zoom;
			Position += movement;
		}
		else if (Input.IsActionJustPressed("snap")) {
			CenterScreenOnTileMap();
		}
	}
	public override void _Input(InputEvent inputEvent) {
		if (sprite.paused) return;
		if (inputEvent.IsActionPressed("zoom_in")) {
			var zoomAmount = Mathf.Clamp(Zoom.x + ZOOM_SPEED, MIN_ZOOM, MAX_ZOOM);
			var newZoom = new Vector2(zoomAmount, zoomAmount);
			var zoomDelta = newZoom - Zoom;
			Zoom = newZoom;
			Position -= GetViewportRect().Size / 2 * zoomDelta;
		}
		else if (inputEvent.IsActionPressed("zoom_out")) {
			var zoomAmount = Mathf.Clamp(Zoom.x - ZOOM_SPEED, MIN_ZOOM, MAX_ZOOM);
			var newZoom = new Vector2(zoomAmount, zoomAmount);
			var zoomDelta = newZoom - Zoom;
			Zoom = newZoom;
			Position -= GetViewportRect().Size / 2 * zoomDelta;
		}
	}
}
