namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class TotemControl
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
            this.totemBox = new System.Windows.Forms.GroupBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.totemLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.totemBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // totemBox
            // 
            this.totemBox.AutoSize = true;
            this.totemBox.Controls.Add(this.totemLayout);
            this.totemBox.Controls.Add(this.introLabel);
            this.totemBox.Location = new System.Drawing.Point(4, 4);
            this.totemBox.Name = "totemBox";
            this.totemBox.Size = new System.Drawing.Size(890, 188);
            this.totemBox.TabIndex = 0;
            this.totemBox.TabStop = false;
            this.totemBox.Text = "Totems";
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(735, 14);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(188, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose for your totem features:";
            // 
            // totemLayout
            // 
            this.totemLayout.AutoSize = true;
            this.totemLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.totemLayout.Location = new System.Drawing.Point(10, 37);
            this.totemLayout.Name = "totemLayout";
            this.totemLayout.Size = new System.Drawing.Size(870, 127);
            this.totemLayout.TabIndex = 1;
            // 
            // TotemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.totemBox);
            this.Name = "TotemControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.totemBox.ResumeLayout(false);
            this.totemBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox totemBox;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.FlowLayoutPanel totemLayout;
    }
}
