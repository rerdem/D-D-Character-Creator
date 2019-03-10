namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class ExtraRaceChoiceControl
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
            this.raceChoiceGroupBox = new System.Windows.Forms.GroupBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.choiceList = new System.Windows.Forms.ListBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.raceChoiceGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // raceChoiceGroupBox
            // 
            this.raceChoiceGroupBox.Controls.Add(this.descriptionLabel);
            this.raceChoiceGroupBox.Controls.Add(this.choiceList);
            this.raceChoiceGroupBox.Controls.Add(this.introLabel);
            this.raceChoiceGroupBox.Location = new System.Drawing.Point(4, 4);
            this.raceChoiceGroupBox.Name = "raceChoiceGroupBox";
            this.raceChoiceGroupBox.Size = new System.Drawing.Size(925, 193);
            this.raceChoiceGroupBox.TabIndex = 0;
            this.raceChoiceGroupBox.TabStop = false;
            this.raceChoiceGroupBox.Text = "Additional Race Choices";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(134, 52);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(785, 134);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(58, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "description";
            // 
            // choiceList
            // 
            this.choiceList.FormattingEnabled = true;
            this.choiceList.Location = new System.Drawing.Point(7, 52);
            this.choiceList.Name = "choiceList";
            this.choiceList.Size = new System.Drawing.Size(120, 134);
            this.choiceList.TabIndex = 1;
            this.choiceList.SelectedIndexChanged += new System.EventHandler(this.choiceList_SelectedIndexChanged);
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(940, 29);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(82, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "introductory text";
            // 
            // ExtraRaceChoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.raceChoiceGroupBox);
            this.Name = "ExtraRaceChoiceControl";
            this.Size = new System.Drawing.Size(937, 200);
            this.raceChoiceGroupBox.ResumeLayout(false);
            this.raceChoiceGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox raceChoiceGroupBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ListBox choiceList;
        private System.Windows.Forms.Label introLabel;
    }
}
