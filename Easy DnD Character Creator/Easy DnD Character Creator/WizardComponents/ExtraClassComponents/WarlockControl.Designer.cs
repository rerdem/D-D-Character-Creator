namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class WarlockControl
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
            this.warlockBox = new System.Windows.Forms.GroupBox();
            this.warlockLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.warlockBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // warlockBox
            // 
            this.warlockBox.AutoSize = true;
            this.warlockBox.Controls.Add(this.warlockLayout);
            this.warlockBox.Location = new System.Drawing.Point(4, 4);
            this.warlockBox.Name = "warlockBox";
            this.warlockBox.Size = new System.Drawing.Size(890, 402);
            this.warlockBox.TabIndex = 0;
            this.warlockBox.TabStop = false;
            this.warlockBox.Text = "Warlock Choices";
            // 
            // warlockLayout
            // 
            this.warlockLayout.AutoSize = true;
            this.warlockLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.warlockLayout.Location = new System.Drawing.Point(7, 20);
            this.warlockLayout.Name = "warlockLayout";
            this.warlockLayout.Size = new System.Drawing.Size(877, 363);
            this.warlockLayout.TabIndex = 0;
            this.warlockLayout.WrapContents = false;
            // 
            // WarlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.warlockBox);
            this.Name = "WarlockControl";
            this.Size = new System.Drawing.Size(900, 412);
            this.warlockBox.ResumeLayout(false);
            this.warlockBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox warlockBox;
        private System.Windows.Forms.FlowLayoutPanel warlockLayout;
    }
}
