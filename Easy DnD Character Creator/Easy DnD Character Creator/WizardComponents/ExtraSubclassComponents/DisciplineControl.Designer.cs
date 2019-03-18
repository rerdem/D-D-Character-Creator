namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class DisciplineControl
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
            this.disciplineBox = new System.Windows.Forms.GroupBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.disciplineListBox = new System.Windows.Forms.ListBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.disciplineBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // disciplineBox
            // 
            this.disciplineBox.Controls.Add(this.descriptionLabel);
            this.disciplineBox.Controls.Add(this.disciplineListBox);
            this.disciplineBox.Controls.Add(this.introLabel);
            this.disciplineBox.Location = new System.Drawing.Point(4, 4);
            this.disciplineBox.Name = "disciplineBox";
            this.disciplineBox.Size = new System.Drawing.Size(890, 188);
            this.disciplineBox.TabIndex = 0;
            this.disciplineBox.TabStop = false;
            this.disciplineBox.Text = "Elemental Disciplines";
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(750, 13);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(46, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Choose:";
            // 
            // disciplineListBox
            // 
            this.disciplineListBox.FormattingEnabled = true;
            this.disciplineListBox.Location = new System.Drawing.Point(7, 37);
            this.disciplineListBox.Name = "disciplineListBox";
            this.disciplineListBox.Size = new System.Drawing.Size(120, 147);
            this.disciplineListBox.TabIndex = 1;
            this.disciplineListBox.SelectedIndexChanged += new System.EventHandler(this.disciplineListBox_SelectedIndexChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(134, 49);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(720, 120);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(168, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "No elemental disciplines available.";
            // 
            // DisciplineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.disciplineBox);
            this.Name = "DisciplineControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.disciplineBox.ResumeLayout(false);
            this.disciplineBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox disciplineBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ListBox disciplineListBox;
        private System.Windows.Forms.Label introLabel;
    }
}
