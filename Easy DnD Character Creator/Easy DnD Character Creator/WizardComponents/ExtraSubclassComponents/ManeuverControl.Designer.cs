namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class ManeuverControl
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
            this.maneuverBox = new System.Windows.Forms.GroupBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.maneuverListBox = new System.Windows.Forms.ListBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.maneuverBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // maneuverBox
            // 
            this.maneuverBox.Controls.Add(this.descriptionLabel);
            this.maneuverBox.Controls.Add(this.maneuverListBox);
            this.maneuverBox.Controls.Add(this.introLabel);
            this.maneuverBox.Location = new System.Drawing.Point(3, 3);
            this.maneuverBox.Name = "maneuverBox";
            this.maneuverBox.Size = new System.Drawing.Size(890, 188);
            this.maneuverBox.TabIndex = 0;
            this.maneuverBox.TabStop = false;
            this.maneuverBox.Text = "Maneuvers";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(134, 55);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(720, 120);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(124, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "No maneuvers available.";
            // 
            // maneuverListBox
            // 
            this.maneuverListBox.FormattingEnabled = true;
            this.maneuverListBox.Location = new System.Drawing.Point(7, 37);
            this.maneuverListBox.Name = "maneuverListBox";
            this.maneuverListBox.Size = new System.Drawing.Size(120, 147);
            this.maneuverListBox.TabIndex = 1;
            this.maneuverListBox.SelectedIndexChanged += new System.EventHandler(this.maneuverListBox_SelectedIndexChanged);
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(750, 13);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(111, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose below:";
            // 
            // ManeuverControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.maneuverBox);
            this.Name = "ManeuverControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.maneuverBox.ResumeLayout(false);
            this.maneuverBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox maneuverBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ListBox maneuverListBox;
        private System.Windows.Forms.Label introLabel;
    }
}
