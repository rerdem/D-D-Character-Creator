namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class ExtraClassChoiceControl
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
            this.classChoiceBox = new System.Windows.Forms.GroupBox();
            this.classChoiceLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.classChoiceBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // classChoiceBox
            // 
            this.classChoiceBox.Controls.Add(this.classChoiceLayout);
            this.classChoiceBox.Location = new System.Drawing.Point(4, 4);
            this.classChoiceBox.Name = "classChoiceBox";
            this.classChoiceBox.Size = new System.Drawing.Size(948, 488);
            this.classChoiceBox.TabIndex = 0;
            this.classChoiceBox.TabStop = false;
            this.classChoiceBox.Text = "Additional Class Choices";
            // 
            // classChoiceLayout
            // 
            this.classChoiceLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classChoiceLayout.AutoScroll = true;
            this.classChoiceLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.classChoiceLayout.Location = new System.Drawing.Point(7, 20);
            this.classChoiceLayout.Name = "classChoiceLayout";
            this.classChoiceLayout.Size = new System.Drawing.Size(935, 462);
            this.classChoiceLayout.TabIndex = 0;
            this.classChoiceLayout.WrapContents = false;
            // 
            // ExtraClassChoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.classChoiceBox);
            this.Name = "ExtraClassChoiceControl";
            this.Size = new System.Drawing.Size(960, 500);
            this.classChoiceBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox classChoiceBox;
        private System.Windows.Forms.FlowLayoutPanel classChoiceLayout;
    }
}
