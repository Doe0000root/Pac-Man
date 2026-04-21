using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace pac_man;

public class ScoreEntry {
    public string Name { get; set; }
    public int Score { get; set; }
}

public class Database {
    private const string ConnectionString = "Data Source=pacman_scores.db";

    public static void Initialize() {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Scores (Name TEXT, Value INTEGER)";
        cmd.ExecuteNonQuery();
    }

    public static void AddScore(string name, int score) {
        try {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Scores (Name, Value) VALUES ($n, $s)";
            cmd.Parameters.AddWithValue("$n", name);
            cmd.Parameters.AddWithValue("$s", score);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Score saved successfully!");
        }
        catch (Exception ex) {
            MessageBox.Show("Database Error: " + ex.Message);
        }
    }

    public static List<ScoreEntry> GetTopScores() {
        var scores = new List<ScoreEntry>();
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Name, Value FROM Scores ORDER BY Value DESC LIMIT 5";
        
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) {
            scores.Add(new ScoreEntry { 
                Name = reader.GetString(0), 
                Score = reader.GetInt32(1) 
            });
        }
        return scores;
    }
}