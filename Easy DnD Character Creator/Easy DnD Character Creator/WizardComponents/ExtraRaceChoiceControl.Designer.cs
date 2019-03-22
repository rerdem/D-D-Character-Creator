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
            this.raceChoiceLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.raceChoiceGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // raceChoiceGroupBox
            // 
            this.raceChoiceGroupBox.Controls.Add(this.raceChoiceLayout);
            this.raceChoiceGroupBox.Location = new System.Drawing.Point(4, 4);
            this.raceChoiceGroupBox.Name = "raceChoiceGroupBox";
            this.raceChoiceGroupBox.Size = new System.Drawing.Size(925, 484);
            this.raceChoiceGroupBox.TabIndex = 0;
            this.raceChoiceGroupBox.TabStop = false;
            this.raceChoiceGroupBox.Text = "Additional Race Choices";
            // 
            // raceChoiceLayout
            // 
            this.raceChoiceLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.raceChoiceLayout.AutoSize = true;
            this.raceChoiceLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.raceChoiceLayout.Location = new System.Drawing.Point(6, 19);
            this.raceChoiceLayout.Name = "raceChoiceLayout";
            this.raceChoiceLayout.Size = new System.Drawing.Size(912, 458);
            this.raceChoiceLayout.TabIndex = 1;
            // 
            // ExtraRaceChoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.raceChoiceGroupBox);
            this.Name = "ExtraRaceChoiceControl";
            this.Size = new System.Drawing.Size(937, 496);
            this.raceChoiceGroupBox.ResumeLayout(false);
            this.raceChoiceGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox raceChoiceGroupBox;
        private System.Windows.Forms.FlowLayoutPanel raceChoiceLayout;
    }
}
