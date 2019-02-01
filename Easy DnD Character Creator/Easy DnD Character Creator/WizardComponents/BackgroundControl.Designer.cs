namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class BackgroundControl
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
            this.backgroundBox = new System.Windows.Forms.GroupBox();
            this.backgroundDescription = new System.Windows.Forms.Label();
            this.backgroundListBox = new System.Windows.Forms.ListBox();
            this.extraProficiencyLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.extraProficiencyLabel = new System.Windows.Forms.Label();
            this.extraProficiencyBox = new System.Windows.Forms.ListBox();
            this.backgroundBox.SuspendLayout();
            this.extraProficiencyLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundBox
            // 
            this.backgroundBox.Controls.Add(this.extraProficiencyLayout);
            this.backgroundBox.Controls.Add(this.backgroundDescription);
            this.backgroundBox.Controls.Add(this.backgroundListBox);
            this.backgroundBox.Location = new System.Drawing.Point(4, 4);
            this.backgroundBox.Name = "backgroundBox";
            this.backgroundBox.Size = new System.Drawing.Size(944, 188);
            this.backgroundBox.TabIndex = 0;
            this.backgroundBox.TabStop = false;
            this.backgroundBox.Text = "Background";
            // 
            // backgroundDescription
            // 
            this.backgroundDescription.AutoSize = true;
            this.backgroundDescription.Location = new System.Drawing.Point(134, 20);
            this.backgroundDescription.MaximumSize = new System.Drawing.Size(650, 160);
            this.backgroundDescription.Name = "backgroundDescription";
            this.backgroundDescription.Size = new System.Drawing.Size(118, 13);
            this.backgroundDescription.TabIndex = 1;
            this.backgroundDescription.Text = "background description";
            // 
            // backgroundListBox
            // 
            this.backgroundListBox.FormattingEnabled = true;
            this.backgroundListBox.Location = new System.Drawing.Point(7, 20);
            this.backgroundListBox.Name = "backgroundListBox";
            this.backgroundListBox.Size = new System.Drawing.Size(120, 160);
            this.backgroundListBox.TabIndex = 0;
            this.backgroundListBox.SelectedIndexChanged += new System.EventHandler(this.backgroundListBox_SelectedIndexChanged);
            // 
            // extraProficiencyLayout
            // 
            this.extraProficiencyLayout.Controls.Add(this.extraProficiencyLabel);
            this.extraProficiencyLayout.Controls.Add(this.extraProficiencyBox);
            this.extraProficiencyLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extraProficiencyLayout.Location = new System.Drawing.Point(813, 17);
            this.extraProficiencyLayout.Name = "extraProficiencyLayout";
            this.extraProficiencyLayout.Size = new System.Drawing.Size(125, 163);
            this.extraProficiencyLayout.TabIndex = 2;
            this.extraProficiencyLayout.Visible = false;
            // 
            // extraProficiencyLabel
            // 
            this.extraProficiencyLabel.AutoSize = true;
            this.extraProficiencyLabel.Location = new System.Drawing.Point(3, 3);
            this.extraProficiencyLabel.Margin = new System.Windows.Forms.Padding(3);
            this.extraProficiencyLabel.Name = "extraProficiencyLabel";
            this.extraProficiencyLabel.Size = new System.Drawing.Size(119, 39);
            this.extraProficiencyLabel.TabIndex = 0;
            this.extraProficiencyLabel.Text = "Please choose 1 of the following tools to be proficient in:";
            // 
            // extraProficiencyBox
            // 
            this.extraProficiencyBox.FormattingEnabled = true;
            this.extraProficiencyBox.Location = new System.Drawing.Point(3, 48);
            this.extraProficiencyBox.Name = "extraProficiencyBox";
            this.extraProficiencyBox.Size = new System.Drawing.Size(114, 108);
            this.extraProficiencyBox.TabIndex = 1;
            // 
            // BackgroundControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backgroundBox);
            this.Name = "BackgroundControl";
            this.Size = new System.Drawing.Size(960, 200);
            this.backgroundBox.ResumeLayout(false);
            this.backgroundBox.PerformLayout();
            this.extraProficiencyLayout.ResumeLayout(false);
            this.extraProficiencyLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox backgroundBox;
        private System.Windows.Forms.Label backgroundDescription;
        private System.Windows.Forms.ListBox backgroundListBox;
        private System.Windows.Forms.FlowLayoutPanel extraProficiencyLayout;
        private System.Windows.Forms.Label extraProficiencyLabel;
        private System.Windows.Forms.ListBox extraProficiencyBox;
    }
}
