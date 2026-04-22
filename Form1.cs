namespace pac_man;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public partial class Form1 : Form {
    private enum GameState { Menu, Playing }
    private GameState currentState = GameState.Menu;

    private string playerName = "Player";
    
    private Timer gameTimer = new Timer();
    private Point pacmanPos = new Point(1, 1);
    private Point ghostPos = new Point(8, 8);
    private Point pacmanDir = new Point(0, 0); 
    private int score = 0;

    private bool announcment = false;

    
    private int ghostDifficulty = 1; 
    private int tickCounter = 0;

   private int[,] map = {
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
    {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,0,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,0,1},
    {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
    {1,1,1,1,0,1,1,1,2,1,2,1,1,1,0,1,1,1,1},
    {2,2,2,1,0,1,2,2,2,2,2,2,2,1,0,1,2,2,2},
    {1,1,1,1,0,1,2,1,1,2,1,1,2,1,0,1,1,1,1},
    {0,0,0,0,0,2,2,1,2,2,2,1,2,2,0,0,0,0,0},
    {1,1,1,1,0,1,2,1,1,1,1,1,2,1,0,1,1,1,1},
    {2,2,2,1,0,1,2,2,2,2,2,2,2,1,0,1,2,2,2},
    {1,1,1,1,0,1,2,1,1,1,1,1,2,1,0,1,1,1,1},
    {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
    {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
    {1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
    {1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1},
    {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
    {1,0,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,0,1},
    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
};

    public Form1() {
        InitializeComponent(); 
        this.DoubleBuffered = true;
        Screen_Display();
        this.Text = "Pac-Man";
        btnEasy.Click += (s, e) => PrepareGame(3); 
        btnHard.Click += (s, e) => PrepareGame(1); 
        btnLeaderboard.Click += (s, e) => ShowLeaderboard();
        gameTimer.Interval = 150; 
        gameTimer.Tick += UpdateGame;
    }

    public void Screen_Display()
    {
        int tileSize = 20; 
        int mapWidth = map.GetLength(1) * tileSize;
        int mapHeight = map.GetLength(0) * tileSize;
        int uiHeight = 40; 
        this.ClientSize = new Size(mapWidth, mapHeight + uiHeight);
        this.FormBorderStyle = FormBorderStyle.FixedSingle; 
        this.MaximizeBox = false; 
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void StartGame(int difficulty) {
        this.ghostDifficulty = difficulty;
        this.currentState = GameState.Playing;
        btnEasy.Visible = false;
        btnHard.Visible = false;
        lblTitle.Visible = false;
        btnLeaderboard.Visible = false;
        
        this.Focus(); 
        gameTimer.Start();
    }
    private void PrepareGame(int difficulty) {
        string input = Interaction.InputBox("Enter your nickname:", "New Game", "PacPlayer");
        
        if (!string.IsNullOrWhiteSpace(input)) {
            playerName = input;
            StartGame(difficulty);
        } else {
            MessageBox.Show("Name cannot be empty!");
        }
    }

    private void ShowLeaderboard() {
        var topScores = Database.GetTopScores(); 
        string message = "Top 5 players\n\n";
        
        if (topScores.Count == 0) {
            message += "No scores yet!";
        } else {
            foreach (var s in topScores) {
                message += $"{s.Name.PadRight((int)5.5)}: {s.Score} points\n";
            }
        }
        
        MessageBox.Show(message, "Leaderboard");
    }

    private void UpdateGame(object? sender, EventArgs e) {
        if (currentState != GameState.Playing) return;

        tickCounter++;

        Point nextPos = new Point(pacmanPos.X + pacmanDir.X, pacmanPos.Y + pacmanDir.Y);
        if (CanMove(nextPos)) {
            pacmanPos = nextPos;
        }

        if (map[pacmanPos.Y, pacmanPos.X] == 0) {
            map[pacmanPos.Y, pacmanPos.X] = 2; 
            score += 10;
        }

        if (tickCounter % ghostDifficulty == 0) {
            ghostPos = AStar.GetNextMove(ghostPos, pacmanPos, map);
        }

        if (pacmanPos == ghostPos) {
            gameTimer.Stop();
            Database.AddScore(playerName, score);
            MessageBox.Show($"Game Over! Your Score: {score}");
            Application.Restart(); 
        }

        GameOverPoint();
        this.Invalidate(); 
    }

    private bool CanMove(Point p) {
        if (p.Y < 0 || p.Y >= map.GetLength(0) || p.X < 0 || p.X >= map.GetLength(1)) return false;
        return map[p.Y, p.X] != 1; 
    }

    protected override void OnKeyDown(KeyEventArgs e) {
        if (currentState != GameState.Playing) return;

        switch (e.KeyCode) {
            case Keys.W:    pacmanDir = new Point(0, -1); break;
            case Keys.S:  pacmanDir = new Point(0, 1);  break;
            case Keys.A:  pacmanDir = new Point(-1, 0); break;
            case Keys.D: pacmanDir = new Point(1, 0);  break;
        }
    }
    private int GetTopScore()
    {
        var topScore = Database.GetTopScores();
        if (topScore.Count > 0) {
            return topScore[0].Score;
        }
        
        return 0; 
    }

    private void GameOverPoint()
    {
        int currentTopScore = GetTopScore();

        if(score >= currentTopScore && !announcment)
        {
            gameTimer.Stop();

            MessageBox.Show($"You have got the new top score in the leaderboards, {playerName}!\nScore: {score}");

            announcment = true;

            gameTimer.Start();

        }
    }


    private void Additional_Abilities()
    {
        /////
    }

    
    private void HandleGameOver() {
        gameTimer.Stop();
        Database.AddScore(playerName, score); 
        MessageBox.Show($"Game Over, {playerName}!\nScore: {score}");
        Application.Restart();
    }


    protected override void OnPaint(PaintEventArgs e) {
        if (currentState != GameState.Playing) return;
        Graphics g = e.Graphics;
        int s = 20; 
        int offset = 40; 
        g.DrawString($"SCORE: {score}", new Font("Arial", 12, FontStyle.Bold), Brushes.White, 10, 10);
        g.DrawLine(Pens.Gray, 0, offset - 1, this.Width, offset - 1);
        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                if (map[y, x] == 1) 
                    g.FillRectangle(Brushes.Blue, x * s, (y * s) + offset, s - 2, s - 2);
                else if (map[y, x] == 0) 
                    g.FillEllipse(Brushes.White, x * s + 8, (y * s) + offset + 8, 4, 4);
            }
        }
        g.FillPie(Brushes.Yellow, pacmanPos.X * s, (pacmanPos.Y * s) + offset, s, s, 45, 270);
        g.FillEllipse(Brushes.Red, ghostPos.X * s, (ghostPos.Y * s) + offset, s, s);
    }
}