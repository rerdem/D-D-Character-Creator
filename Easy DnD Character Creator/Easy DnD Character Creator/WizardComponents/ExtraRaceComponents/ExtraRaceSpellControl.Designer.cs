namespace Easy_DnD_Character_Creator.WizardComponents.ExtraRaceComponents
{
    partial class ExtraRaceSpellControl
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
            this.spellBox = new System.Windows.Forms.GroupBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.spellListBox = new System.Windows.Forms.ListBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.spellBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // spellBox
            // 
            this.spellBox.Controls.Add(this.descriptionLabel);
            this.spellBox.Controls.Add(this.spellListBox);
            this.spellBox.Controls.Add(this.introLabel);
            this.spellBox.Location = new System.Drawing.Point(5, 6);
            this.spellBox.Name = "spellBox";
            this.spellBox.Size = new System.Drawing.Size(890, 188);
            this.spellBox.TabIndex = 1;
            this.spellBox.TabStop = false;
            this.spellBox.Text = "Additional Spells";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(134, 49);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(740, 122);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(98, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "No spells available.";
            // 
            // spellListBox
            // 
            this.spellListBox.FormattingEnabled = true;
            this.spellListBox.Location = new System.Drawing.Point(7, 37);
            this.spellListBox.Name = "spellListBox";
            this.spellListBox.Size = new System.Drawing.Size(120, 134);
            this.spellListBox.TabIndex = 1;
            this.spellListBox.SelectedIndexChanged += new System.EventHandler(this.spellListBox_SelectedIndexChanged);
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(840, 23);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(162, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose a spell you know:";
            // 
            // ExtraRaceSpellControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spellBox);
            this.Name = "ExtraRaceSpellControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.spellBox.ResumeLayout(false);
            this.spellBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox spellBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ListBox spellListBox;
        private System.Windows.Forms.Label introLabel;
    }
}
