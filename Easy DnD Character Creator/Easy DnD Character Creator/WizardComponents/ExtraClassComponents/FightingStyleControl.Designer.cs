namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class FightingStyleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fightingStyleBox = new System.Windows.Forms.GroupBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.fightingStyleLabel = new System.Windows.Forms.Label();
            this.fightingStyleListBox = new System.Windows.Forms.ListBox();
            this.fightingStyleBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // fightingStyleBox
            // 
            this.fightingStyleBox.Controls.Add(this.descriptionLabel);
            this.fightingStyleBox.Controls.Add(this.fightingStyleLabel);
            this.fightingStyleBox.Controls.Add(this.fightingStyleListBox);
            this.fightingStyleBox.Location = new System.Drawing.Point(4, 4);
            this.fightingStyleBox.Name = "fightingStyleBox";
            this.fightingStyleBox.Size = new System.Drawing.Size(890, 138);
            this.fightingStyleBox.TabIndex = 0;
            this.fightingStyleBox.TabStop = false;
            this.fightingStyleBox.Text = "Fighting Style";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(133, 35);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(751, 95);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(58, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "description";
            // 
            // fightingStyleLabel
            // 
            this.fightingStyleLabel.AutoSize = true;
            this.fightingStyleLabel.Location = new System.Drawing.Point(7, 19);
            this.fightingStyleLabel.Name = "fightingStyleLabel";
            this.fightingStyleLabel.Size = new System.Drawing.Size(46, 13);
            this.fightingStyleLabel.TabIndex = 1;
            this.fightingStyleLabel.Text = "Choose:";
            // 
            // fightingStyleListBox
            // 
            this.fightingStyleListBox.FormattingEnabled = true;
            this.fightingStyleListBox.Location = new System.Drawing.Point(6, 35);
            this.fightingStyleListBox.Name = "fightingStyleListBox";
            this.fightingStyleListBox.Size = new System.Drawing.Size(120, 95);
            this.fightingStyleListBox.TabIndex = 0;
            this.fightingStyleListBox.SelectedIndexChanged += new System.EventHandler(this.fightingStyleListBox_SelectedIndexChanged);
            // 
            // FightingStyleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fightingStyleBox);
            this.Name = "FightingStyleControl";
            this.Size = new System.Drawing.Size(900, 150);
            this.fightingStyleBox.ResumeLayout(false);
            this.fightingStyleBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fightingStyleBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label fightingStyleLabel;
        private System.Windows.Forms.ListBox fightingStyleListBox;
    }
}
