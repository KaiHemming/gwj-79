using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
	public override void _Process(float delta) {
		if (Input.IsActionPressed("zoom")) {
			Vector2 movement = Input.GetLastMouseSpeed();
			movement *= -0.02f;
			Position += movement;
		}
		else if (Input.IsActionPressed("snap")) {
			Position = Vector2.Zero;
		}
	}
}
