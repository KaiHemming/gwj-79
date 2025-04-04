using Godot;
using System;

public class EndScreen : Control
{
	public void SetScore(int score) {
		GetNode("VBoxContainer").GetNode("HBoxContainer").GetNode<Label>("Score").Text = "" + score;
	}
	private void _on_Button_pressed()
	{
		if(!Visible) return;
		var newGame = GD.Load<PackedScene>("res://scenes/Main.tscn");
		GetTree().ChangeSceneTo(newGame);
	}
}
