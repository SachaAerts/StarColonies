namespace StarColonies.Domains.Models;

public class Enemy
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required Type EnemyType { get; set; }
    public required int Strength { get; set; }
    public required int Stamina { get; set; }
    public required string ImagePath { get; set; }
}