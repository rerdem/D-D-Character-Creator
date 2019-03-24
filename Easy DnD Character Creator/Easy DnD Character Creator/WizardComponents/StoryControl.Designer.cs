namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class StoryControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoryControl));
            this.introLabel = new System.Windows.Forms.Label();
            this.storyLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(4, 4);
            this.introLabel.Margin = new System.Windows.Forms.Padding(3);
            this.introLabel.MaximumSize = new System.Drawing.Size(930, 45);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(894, 39);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = resources.GetString("introLabel.Text");
            // 
            // storyLayout
            // 
            this.storyLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.storyLayout.Location = new System.Drawing.Point(7, 55);
            this.storyLayout.Name = "storyLayout";
            this.storyLayout.Size = new System.Drawing.Size(925, 742);
            this.storyLayout.TabIndex = 1;
            // 
            // StoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.storyLayout);
            this.Controls.Add(this.introLabel);
            this.Name = "StoryControl";
            this.Size = new System.Drawing.Size(937, 800);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.FlowLayoutPanel storyLayout;
    }
}
