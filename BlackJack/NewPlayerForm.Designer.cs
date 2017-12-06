namespace BlackJack
{
    partial class NewPlayerForm
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
            this.Lbl_chooseName = new System.Windows.Forms.Label();
            this.Tb_name = new System.Windows.Forms.TextBox();
            this.Btn_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbl_chooseName
            // 
            this.Lbl_chooseName.AutoSize = true;
            this.Lbl_chooseName.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_chooseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Lbl_chooseName.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_chooseName.Location = new System.Drawing.Point(23, 91);
            this.Lbl_chooseName.Name = "Lbl_chooseName";
            this.Lbl_chooseName.Size = new System.Drawing.Size(237, 31);
            this.Lbl_chooseName.TabIndex = 0;
            this.Lbl_chooseName.Text = "Enter your name:";
            // 
            // Tb_name
            // 
            this.Tb_name.Location = new System.Drawing.Point(62, 133);
            this.Tb_name.Name = "Tb_name";
            this.Tb_name.Size = new System.Drawing.Size(150, 22);
            this.Tb_name.TabIndex = 1;
            // 
            // Btn_ok
            // 
            this.Btn_ok.Location = new System.Drawing.Point(86, 181);
            this.Btn_ok.Name = "Btn_ok";
            this.Btn_ok.Size = new System.Drawing.Size(100, 50);
            this.Btn_ok.TabIndex = 2;
            this.Btn_ok.Text = "OK";
            this.Btn_ok.UseVisualStyleBackColor = true;
            this.Btn_ok.Click += new System.EventHandler(this.Btn_ok_Click);
            // 
            // NewPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BlackJack.Properties.Resources.newPlayerBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.Btn_ok);
            this.Controls.Add(this.Tb_name);
            this.Controls.Add(this.Lbl_chooseName);
            this.Name = "NewPlayerForm";
            this.Text = "NewPlayerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_chooseName;
        private System.Windows.Forms.TextBox Tb_name;
        private System.Windows.Forms.Button Btn_ok;
    }
}