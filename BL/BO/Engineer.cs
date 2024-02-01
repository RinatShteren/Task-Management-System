﻿namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerLevel? Level { get; set; } 
    public double Cost { get; set; }
    public BO.TaskInEngineer task { get; set; }
}