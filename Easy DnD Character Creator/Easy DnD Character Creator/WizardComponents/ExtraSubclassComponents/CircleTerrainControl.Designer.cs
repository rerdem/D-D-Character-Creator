namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class CircleTerrainControl
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
            this.circleTerrainComboBox = new System.Windows.Forms.ComboBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.circleTerrainBox = new System.Windows.Forms.GroupBox();
            this.circleTerrainBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // circleTerrainComboBox
            // 
            this.circleTerrainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.circleTerrainComboBox.FormattingEnabled = true;
            this.circleTerrainComboBox.Location = new System.Drawing.Point(270, 23);
            this.circleTerrainComboBox.Name = "circleTerrainComboBox";
            this.circleTerrainComboBox.Size = new System.Drawing.Size(200, 21);
            this.circleTerrainComboBox.TabIndex = 1;
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(6, 26);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(258, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose the terrain where you became a druid:";
            // 
            // circleTerrainBox
            // 
            this.circleTerrainBox.Controls.Add(this.circleTerrainComboBox);
            this.circleTerrainBox.Controls.Add(this.introLabel);
            this.circleTerrainBox.Location = new System.Drawing.Point(5, 6);
            this.circleTerrainBox.Name = "circleTerrainBox";
            this.circleTerrainBox.Size = new System.Drawing.Size(890, 58);
            this.circleTerrainBox.TabIndex = 1;
            this.circleTerrainBox.TabStop = false;
            this.circleTerrainBox.Text = "Druid Circle Terrain";
            // 
            // CircleTerrainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.circleTerrainBox);
            this.Name = "CircleTerrainControl";
            this.Size = new System.Drawing.Size(900, 70);
            this.circleTerrainBox.ResumeLayout(false);
            this.circleTerrainBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox circleTerrainComboBox;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.GroupBox circleTerrainBox;
    }
}
