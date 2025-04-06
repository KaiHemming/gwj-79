using Godot;
using System;

public class TitleScreen : Control
{
	private void _on_Button_pressed()
	{
		var newGame = GD.Load<PackedScene>("res://scenes/Main.tscn");
		GetTree().ChangeSceneTo(newGame);
		QueueFree();
	}
}
