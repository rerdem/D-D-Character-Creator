namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    partial class BackgroundStoryControl
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
            this.backgroundComboBox = new System.Windows.Forms.ComboBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.backgroundBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundBox
            // 
            this.backgroundBox.Controls.Add(this.backgroundComboBox);
            this.backgroundBox.Controls.Add(this.introLabel);
            this.backgroundBox.Location = new System.Drawing.Point(4, 4);
            this.backgroundBox.Name = "backgroundBox";
            this.backgroundBox.Size = new System.Drawing.Size(890, 88);
            this.backgroundBox.TabIndex = 0;
            this.backgroundBox.TabStop = false;
            this.backgroundBox.Text = "Heading";
            // 
            // backgroundComboBox
            // 
            this.backgroundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.backgroundComboBox.FormattingEnabled = true;
            this.backgroundComboBox.Location = new System.Drawing.Point(7, 61);
            this.backgroundComboBox.Name = "backgroundComboBox";
            this.backgroundComboBox.Size = new System.Drawing.Size(877, 21);
            this.backgroundComboBox.TabIndex = 1;
            this.backgroundComboBox.SelectedIndexChanged += new System.EventHandler(this.backgroundComboBox_SelectedIndexChanged);
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(877, 38);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(47, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "intro text";
            // 
            // BackgroundStoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backgroundBox);
            this.Name = "BackgroundStoryControl";
            this.Size = new System.Drawing.Size(900, 100);
            this.backgroundBox.ResumeLayout(false);
            this.backgroundBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox backgroundBox;
        private System.Windows.Forms.ComboBox backgroundComboBox;
        private System.Windows.Forms.Label introLabel;
    }
}
