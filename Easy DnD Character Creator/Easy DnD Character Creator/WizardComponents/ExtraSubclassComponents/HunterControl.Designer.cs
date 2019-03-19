namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class HunterControl
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
            this.hunterLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.introLabel = new System.Windows.Forms.Label();
            this.hunterBox = new System.Windows.Forms.GroupBox();
            this.hunterBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // hunterLayout
            // 
            this.hunterLayout.AutoSize = true;
            this.hunterLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.hunterLayout.Location = new System.Drawing.Point(10, 37);
            this.hunterLayout.Name = "hunterLayout";
            this.hunterLayout.Size = new System.Drawing.Size(870, 127);
            this.hunterLayout.TabIndex = 1;
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(735, 14);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(192, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose for your hunter features:";
            // 
            // hunterBox
            // 
            this.hunterBox.AutoSize = true;
            this.hunterBox.Controls.Add(this.hunterLayout);
            this.hunterBox.Controls.Add(this.introLabel);
            this.hunterBox.Location = new System.Drawing.Point(5, 6);
            this.hunterBox.Name = "hunterBox";
            this.hunterBox.Size = new System.Drawing.Size(890, 188);
            this.hunterBox.TabIndex = 1;
            this.hunterBox.TabStop = false;
            this.hunterBox.Text = "Hunter Features";
            // 
            // HunterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hunterBox);
            this.Name = "HunterControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.hunterBox.ResumeLayout(false);
            this.hunterBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel hunterLayout;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.GroupBox hunterBox;
    }
}
