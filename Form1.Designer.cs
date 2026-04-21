namespace pac_man;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Button btnEasy;
    private System.Windows.Forms.Button btnHard;
    private System.Windows.Forms.Label lblTitle;

    private Button btnLeaderboard;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.btnEasy = new System.Windows.Forms.Button();
        this.btnHard = new System.Windows.Forms.Button();
        this.lblTitle = new System.Windows.Forms.Label();
        this.btnLeaderboard = new Button();
        this.SuspendLayout();

      
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblTitle.ForeColor = System.Drawing.Color.Yellow;
        this.lblTitle.Location = new System.Drawing.Point(0, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(250, 40);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "PAC-MAN";
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

     
        this.btnEasy.BackColor = System.Drawing.Color.DarkGreen;
        this.btnEasy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnEasy.ForeColor = System.Drawing.Color.White;
        this.btnEasy.Location = new System.Drawing.Point(50, 75);
        this.btnEasy.Name = "btnEasy";
        this.btnEasy.Size = new System.Drawing.Size(150, 35);
        this.btnEasy.TabIndex = 1;
        this.btnEasy.Text = "EASY LEVEL";
        this.btnEasy.UseVisualStyleBackColor = false;
    
        this.btnHard.BackColor = System.Drawing.Color.DarkRed;
        this.btnHard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnHard.ForeColor = System.Drawing.Color.White;
        this.btnHard.Location = new System.Drawing.Point(50, 120);
        this.btnHard.Name = "btnHard";
        this.btnHard.Size = new System.Drawing.Size(150, 35);
        this.btnHard.TabIndex = 2;
        this.btnHard.Text = "HARD LEVEL";
        this.btnHard.UseVisualStyleBackColor = false;


        this.btnLeaderboard.Text = "LEADERBOARD";
        this.btnLeaderboard.Location = new Point(50, 165);
        this.btnLeaderboard.Size = new System.Drawing.Size(150, 35);
        this.btnLeaderboard.BackColor = Color.Gray;
        this.btnLeaderboard.ForeColor = Color.White;

   
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.Black;
        this.ClientSize = new System.Drawing.Size(250, 200); 
        this.Controls.Add(this.lblTitle);
        this.Controls.Add(this.btnEasy);
        this.Controls.Add(this.btnHard);
        this.Controls.Add(this.btnLeaderboard);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Pac-Man Menu";
        this.ResumeLayout(false);
    }

    #endregion
}