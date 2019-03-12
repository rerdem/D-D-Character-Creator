namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class ExtraSubclassChoiceControl
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
            this.extraSubclassChoiceBox = new System.Windows.Forms.GroupBox();
            this.subclassChoiceLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.extraSubclassChoiceBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // extraSubclassChoiceBox
            // 
            this.extraSubclassChoiceBox.Controls.Add(this.subclassChoiceLayout);
            this.extraSubclassChoiceBox.Location = new System.Drawing.Point(4, 4);
            this.extraSubclassChoiceBox.Name = "extraSubclassChoiceBox";
            this.extraSubclassChoiceBox.Size = new System.Drawing.Size(925, 484);
            this.extraSubclassChoiceBox.TabIndex = 0;
            this.extraSubclassChoiceBox.TabStop = false;
            this.extraSubclassChoiceBox.Text = "Additional Subclass Choices";
            // 
            // subclassChoiceLayout
            // 
            this.subclassChoiceLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subclassChoiceLayout.AutoSize = true;
            this.subclassChoiceLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.subclassChoiceLayout.Location = new System.Drawing.Point(7, 20);
            this.subclassChoiceLayout.Name = "subclassChoiceLayout";
            this.subclassChoiceLayout.Size = new System.Drawing.Size(912, 458);
            this.subclassChoiceLayout.TabIndex = 0;
            // 
            // ExtraSubclassChoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.extraSubclassChoiceBox);
            this.Name = "ExtraSubclassChoiceControl";
            this.Size = new System.Drawing.Size(937, 496);
            this.extraSubclassChoiceBox.ResumeLayout(false);
            this.extraSubclassChoiceBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox extraSubclassChoiceBox;
        private System.Windows.Forms.FlowLayoutPanel subclassChoiceLayout;
    }
}
