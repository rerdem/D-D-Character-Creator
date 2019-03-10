namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class RaceControl
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
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.raceBox = new System.Windows.Forms.GroupBox();
            this.extraChoiceLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.extraChoiceLabel = new System.Windows.Forms.Label();
            this.extraChoiceBox = new System.Windows.Forms.ListBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.subraceListBox = new System.Windows.Forms.ListBox();
            this.raceListBox = new System.Windows.Forms.ListBox();
            this.raceBox.SuspendLayout();
            this.extraChoiceLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // raceBox
            // 
            this.raceBox.Controls.Add(this.extraChoiceLayout);
            this.raceBox.Controls.Add(this.descriptionLabel);
            this.raceBox.Controls.Add(this.subraceListBox);
            this.raceBox.Controls.Add(this.raceListBox);
            this.raceBox.Location = new System.Drawing.Point(3, 3);
            this.raceBox.Name = "raceBox";
            this.raceBox.Size = new System.Drawing.Size(925, 188);
            this.raceBox.TabIndex = 0;
            this.raceBox.TabStop = false;
            this.raceBox.Text = "Race/Subrace";
            // 
            // extraChoiceLayout
            // 
            this.extraChoiceLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.extraChoiceLayout.Controls.Add(this.extraChoiceLabel);
            this.extraChoiceLayout.Controls.Add(this.extraChoiceBox);
            this.extraChoiceLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extraChoiceLayout.Location = new System.Drawing.Point(794, 19);
            this.extraChoiceLayout.Name = "extraChoiceLayout";
            this.extraChoiceLayout.Size = new System.Drawing.Size(125, 163);
            this.extraChoiceLayout.TabIndex = 3;
            this.extraChoiceLayout.Visible = false;
            // 
            // extraChoiceLabel
            // 
            this.extraChoiceLabel.AutoSize = true;
            this.extraChoiceLabel.Location = new System.Drawing.Point(3, 3);
            this.extraChoiceLabel.Margin = new System.Windows.Forms.Padding(3);
            this.extraChoiceLabel.Name = "extraChoiceLabel";
            this.extraChoiceLabel.Size = new System.Drawing.Size(119, 39);
            this.extraChoiceLabel.TabIndex = 0;
            this.extraChoiceLabel.Text = "Please choose 1 of the following tools to be proficient in:";
            // 
            // extraChoiceBox
            // 
            this.extraChoiceBox.FormattingEnabled = true;
            this.extraChoiceBox.Location = new System.Drawing.Point(3, 48);
            this.extraChoiceBox.Name = "extraChoiceBox";
            this.extraChoiceBox.Size = new System.Drawing.Size(114, 108);
            this.extraChoiceBox.TabIndex = 1;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(260, 19);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(650, 160);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(58, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "description";
            // 
            // subraceListBox
            // 
            this.subraceListBox.FormattingEnabled = true;
            this.subraceListBox.Location = new System.Drawing.Point(132, 19);
            this.subraceListBox.Name = "subraceListBox";
            this.subraceListBox.Size = new System.Drawing.Size(120, 160);
            this.subraceListBox.TabIndex = 1;
            this.subraceListBox.SelectedIndexChanged += new System.EventHandler(this.subraceListBox_SelectedIndexChanged);
            // 
            // raceListBox
            // 
            this.raceListBox.FormattingEnabled = true;
            this.raceListBox.Location = new System.Drawing.Point(6, 19);
            this.raceListBox.Name = "raceListBox";
            this.raceListBox.Size = new System.Drawing.Size(120, 160);
            this.raceListBox.TabIndex = 0;
            this.raceListBox.SelectedIndexChanged += new System.EventHandler(this.raceListBox_SelectedIndexChanged);
            // 
            // RaceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.raceBox);
            this.Name = "RaceControl";
            this.Size = new System.Drawing.Size(937, 200);
            this.raceBox.ResumeLayout(false);
            this.raceBox.PerformLayout();
            this.extraChoiceLayout.ResumeLayout(false);
            this.extraChoiceLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.Windows.Forms.GroupBox raceBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ListBox subraceListBox;
        private System.Windows.Forms.ListBox raceListBox;
        private System.Windows.Forms.FlowLayoutPanel extraChoiceLayout;
        private System.Windows.Forms.Label extraChoiceLabel;
        private System.Windows.Forms.ListBox extraChoiceBox;
    }
}
