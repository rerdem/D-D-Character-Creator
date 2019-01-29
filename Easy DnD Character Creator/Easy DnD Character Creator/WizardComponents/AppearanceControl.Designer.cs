namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class AppearanceControl
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
            this.appearanceBox = new System.Windows.Forms.GroupBox();
            this.appearanceLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.eyeLabel = new System.Windows.Forms.Label();
            this.eyeColorBox = new System.Windows.Forms.TextBox();
            this.skinLabel = new System.Windows.Forms.Label();
            this.skinColorBox = new System.Windows.Forms.TextBox();
            this.hairLabel = new System.Windows.Forms.Label();
            this.hairColorBox = new System.Windows.Forms.TextBox();
            this.appearanceBox.SuspendLayout();
            this.appearanceLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // appearanceBox
            // 
            this.appearanceBox.Controls.Add(this.appearanceLayout);
            this.appearanceBox.Location = new System.Drawing.Point(3, 3);
            this.appearanceBox.Name = "appearanceBox";
            this.appearanceBox.Size = new System.Drawing.Size(944, 68);
            this.appearanceBox.TabIndex = 0;
            this.appearanceBox.TabStop = false;
            this.appearanceBox.Text = "Physical Appearance";
            // 
            // appearanceLayout
            // 
            this.appearanceLayout.Controls.Add(this.eyeLabel);
            this.appearanceLayout.Controls.Add(this.eyeColorBox);
            this.appearanceLayout.Controls.Add(this.skinLabel);
            this.appearanceLayout.Controls.Add(this.skinColorBox);
            this.appearanceLayout.Controls.Add(this.hairLabel);
            this.appearanceLayout.Controls.Add(this.hairColorBox);
            this.appearanceLayout.Location = new System.Drawing.Point(6, 24);
            this.appearanceLayout.Name = "appearanceLayout";
            this.appearanceLayout.Size = new System.Drawing.Size(926, 37);
            this.appearanceLayout.TabIndex = 0;
            this.appearanceLayout.WrapContents = false;
            // 
            // eyeLabel
            // 
            this.eyeLabel.AutoSize = true;
            this.eyeLabel.Location = new System.Drawing.Point(3, 6);
            this.eyeLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.eyeLabel.Name = "eyeLabel";
            this.eyeLabel.Size = new System.Drawing.Size(55, 13);
            this.eyeLabel.TabIndex = 0;
            this.eyeLabel.Text = "Eye Color:";
            // 
            // eyeColorBox
            // 
            this.eyeColorBox.Location = new System.Drawing.Point(64, 3);
            this.eyeColorBox.Name = "eyeColorBox";
            this.eyeColorBox.Size = new System.Drawing.Size(200, 20);
            this.eyeColorBox.TabIndex = 1;
            this.eyeColorBox.TextChanged += new System.EventHandler(this.eyeColorBox_TextChanged);
            // 
            // skinLabel
            // 
            this.skinLabel.AutoSize = true;
            this.skinLabel.Location = new System.Drawing.Point(287, 6);
            this.skinLabel.Margin = new System.Windows.Forms.Padding(20, 6, 3, 0);
            this.skinLabel.Name = "skinLabel";
            this.skinLabel.Size = new System.Drawing.Size(58, 13);
            this.skinLabel.TabIndex = 2;
            this.skinLabel.Text = "Skin Color:";
            // 
            // skinColorBox
            // 
            this.skinColorBox.Location = new System.Drawing.Point(351, 3);
            this.skinColorBox.Name = "skinColorBox";
            this.skinColorBox.Size = new System.Drawing.Size(200, 20);
            this.skinColorBox.TabIndex = 3;
            this.skinColorBox.TextChanged += new System.EventHandler(this.skinColorBox_TextChanged);
            // 
            // hairLabel
            // 
            this.hairLabel.AutoSize = true;
            this.hairLabel.Location = new System.Drawing.Point(574, 6);
            this.hairLabel.Margin = new System.Windows.Forms.Padding(20, 6, 3, 0);
            this.hairLabel.Name = "hairLabel";
            this.hairLabel.Size = new System.Drawing.Size(56, 13);
            this.hairLabel.TabIndex = 4;
            this.hairLabel.Text = "Hair Color:";
            // 
            // hairColorBox
            // 
            this.hairColorBox.Location = new System.Drawing.Point(636, 3);
            this.hairColorBox.Name = "hairColorBox";
            this.hairColorBox.Size = new System.Drawing.Size(200, 20);
            this.hairColorBox.TabIndex = 5;
            this.hairColorBox.TextChanged += new System.EventHandler(this.hairColorBox_TextChanged);
            // 
            // AppearanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.appearanceBox);
            this.Name = "AppearanceControl";
            this.Size = new System.Drawing.Size(960, 80);
            this.appearanceBox.ResumeLayout(false);
            this.appearanceLayout.ResumeLayout(false);
            this.appearanceLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox appearanceBox;
        private System.Windows.Forms.FlowLayoutPanel appearanceLayout;
        private System.Windows.Forms.Label eyeLabel;
        private System.Windows.Forms.TextBox eyeColorBox;
        private System.Windows.Forms.Label skinLabel;
        private System.Windows.Forms.TextBox skinColorBox;
        private System.Windows.Forms.Label hairLabel;
        private System.Windows.Forms.TextBox hairColorBox;
    }
}
