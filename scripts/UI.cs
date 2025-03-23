using Godot;
using System;

public class UI : Control
{
	private int score = 0;
	public static PackedScene scoreAdditionScene;
	public override void _Ready()
	{
		scoreAdditionScene = GD.Load<PackedScene>("res://scenes/ScoreAddition.tscn");
		PopUp();
	}
	public void PopUp() {
		GetParent().GetNode<Sprite>("Sprite").paused = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
		GetNode<WindowDialog>("Tutorial").Show();
	}
	public override void _Process(float delta) {
		//GD.Print(GetViewportRect().Size);
		this.RectSize = GetViewportRect().Size;
		//GetNode<AcceptDialog>("AcceptDialog").Popup_();
	}
	private void _on_CloseButton_pressed()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		GetParent().GetNode<Sprite>("Sprite").paused = false;
		GetNode<WindowDialog>("Tutorial").Hide();
	}
	public void AddScore(int increment) {
		if (increment == 0) return;
		this.score += increment;
		GetNode("HBoxContainer").GetNode<Label>("Score").Text = "" + score;
		ScoreAddition scoreAddition = (ScoreAddition)scoreAdditionScene.Instance();
		scoreAddition.SetScoreAddition(increment);
		GetNode("HBoxContainer").GetNode<VBoxContainer>("ScoreAdditions").AddChild(scoreAddition);
	}
	public void DeductScore(int increment) {
		if (increment == 0) return;
		this.score -= increment;
		GetNode("HBoxContainer").GetNode<Label>("Score").Text = "" + score;
		ScoreAddition scoreAddition = (ScoreAddition)scoreAdditionScene.Instance();
		scoreAddition.SetScoreReduction(increment);
		GetNode("HBoxContainer").GetNode<VBoxContainer>("ScoreAdditions").AddChild(scoreAddition);
	}
	public int GetScore() {
		return score;
	}
	private void _on_HelpButton_pressed()
	{
		PopUp();
	}
	public void UpdateNumRemaining(int bagSize) {
		GetNode("VBoxContainer").GetNode<Label>("RemainingTiles").Text = bagSize + " remaining";
	}
	public void UpdateTileName(String tileName) {
		GetNode("VBoxContainer").GetNode<Label>("TileName").Text = tileName;
	}
}
