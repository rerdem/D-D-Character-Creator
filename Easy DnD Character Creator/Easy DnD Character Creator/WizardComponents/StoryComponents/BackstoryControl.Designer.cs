namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    partial class BackstoryControl
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
            this.storyBox = new System.Windows.Forms.GroupBox();
            this.storyTextBox = new System.Windows.Forms.TextBox();
            this.storyBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // storyBox
            // 
            this.storyBox.Controls.Add(this.storyTextBox);
            this.storyBox.Location = new System.Drawing.Point(4, 4);
            this.storyBox.Name = "storyBox";
            this.storyBox.Size = new System.Drawing.Size(890, 489);
            this.storyBox.TabIndex = 0;
            this.storyBox.TabStop = false;
            this.storyBox.Text = "Character Backstory";
            // 
            // storyTextBox
            // 
            this.storyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.storyTextBox.Location = new System.Drawing.Point(7, 20);
            this.storyTextBox.Multiline = true;
            this.storyTextBox.Name = "storyTextBox";
            this.storyTextBox.Size = new System.Drawing.Size(877, 463);
            this.storyTextBox.TabIndex = 0;
            // 
            // BackstoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.storyBox);
            this.Name = "BackstoryControl";
            this.Size = new System.Drawing.Size(900, 496);
            this.storyBox.ResumeLayout(false);
            this.storyBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox storyBox;
        private System.Windows.Forms.TextBox storyTextBox;
    }
}
