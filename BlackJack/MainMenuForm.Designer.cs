namespace BlackJack
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_newGame = new System.Windows.Forms.Button();
            this.Btn_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_newGame
            // 
            this.Btn_newGame.Location = new System.Drawing.Point(569, 100);
            this.Btn_newGame.Name = "Btn_newGame";
            this.Btn_newGame.Size = new System.Drawing.Size(120, 50);
            this.Btn_newGame.TabIndex = 0;
            this.Btn_newGame.Text = "New Game";
            this.Btn_newGame.UseVisualStyleBackColor = true;
            this.Btn_newGame.Click += new System.EventHandler(this.Btn_newGame_Click);
            // 
            // Btn_exit
            // 
            this.Btn_exit.Location = new System.Drawing.Point(569, 188);
            this.Btn_exit.Name = "Btn_exit";
            this.Btn_exit.Size = new System.Drawing.Size(120, 50);
            this.Btn_exit.TabIndex = 2;
            this.Btn_exit.Text = "Exit";
            this.Btn_exit.UseVisualStyleBackColor = true;
            this.Btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BlackJack.Properties.Resources.mainMenuBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.Btn_exit);
            this.Controls.Add(this.Btn_newGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Black Jack";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_newGame;
        private System.Windows.Forms.Button Btn_exit;
    }
}

