namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    partial class WildShapeControl
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
            this.terrainComboBox = new System.Windows.Forms.ComboBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.wildShapeBox = new System.Windows.Forms.GroupBox();
            this.wildShapeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // terrainComboBox
            // 
            this.terrainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.terrainComboBox.FormattingEnabled = true;
            this.terrainComboBox.Location = new System.Drawing.Point(7, 61);
            this.terrainComboBox.Name = "terrainComboBox";
            this.terrainComboBox.Size = new System.Drawing.Size(877, 21);
            this.terrainComboBox.TabIndex = 1;
            this.terrainComboBox.SelectedIndexChanged += new System.EventHandler(this.terrainComboBox_SelectedIndexChanged);
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(7, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(877, 38);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(80, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose:";
            // 
            // wildShapeBox
            // 
            this.wildShapeBox.Controls.Add(this.terrainComboBox);
            this.wildShapeBox.Controls.Add(this.introLabel);
            this.wildShapeBox.Location = new System.Drawing.Point(5, 6);
            this.wildShapeBox.Name = "wildShapeBox";
            this.wildShapeBox.Size = new System.Drawing.Size(890, 88);
            this.wildShapeBox.TabIndex = 1;
            this.wildShapeBox.TabStop = false;
            this.wildShapeBox.Text = "Wild Shape";
            // 
            // WildShapeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wildShapeBox);
            this.Name = "WildShapeControl";
            this.Size = new System.Drawing.Size(900, 100);
            this.wildShapeBox.ResumeLayout(false);
            this.wildShapeBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox terrainComboBox;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.GroupBox wildShapeBox;
    }
}
