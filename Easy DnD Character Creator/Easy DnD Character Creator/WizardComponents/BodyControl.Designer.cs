namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class BodyControl
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
            this.bodyBox = new System.Windows.Forms.GroupBox();
            this.heightWeightLayout = new System.Windows.Forms.TableLayoutPanel();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.heightModifier = new System.Windows.Forms.NumericUpDown();
            this.weightModifier = new System.Windows.Forms.NumericUpDown();
            this.heightResultLabel = new System.Windows.Forms.Label();
            this.weightResultLabel = new System.Windows.Forms.Label();
            this.randomizeButton = new System.Windows.Forms.Button();
            this.bodyBox.SuspendLayout();
            this.heightWeightLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightModifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightModifier)).BeginInit();
            this.SuspendLayout();
            // 
            // bodyBox
            // 
            this.bodyBox.Controls.Add(this.heightWeightLayout);
            this.bodyBox.Location = new System.Drawing.Point(4, 4);
            this.bodyBox.Name = "bodyBox";
            this.bodyBox.Size = new System.Drawing.Size(925, 148);
            this.bodyBox.TabIndex = 0;
            this.bodyBox.TabStop = false;
            this.bodyBox.Text = "Height/Weight";
            // 
            // heightWeightLayout
            // 
            this.heightWeightLayout.ColumnCount = 3;
            this.heightWeightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.heightWeightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.heightWeightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.heightWeightLayout.Controls.Add(this.descriptionLabel, 0, 0);
            this.heightWeightLayout.Controls.Add(this.heightLabel, 0, 1);
            this.heightWeightLayout.Controls.Add(this.weightLabel, 0, 2);
            this.heightWeightLayout.Controls.Add(this.heightModifier, 1, 1);
            this.heightWeightLayout.Controls.Add(this.weightModifier, 1, 2);
            this.heightWeightLayout.Controls.Add(this.heightResultLabel, 2, 1);
            this.heightWeightLayout.Controls.Add(this.weightResultLabel, 2, 2);
            this.heightWeightLayout.Controls.Add(this.randomizeButton, 1, 3);
            this.heightWeightLayout.Location = new System.Drawing.Point(7, 20);
            this.heightWeightLayout.Name = "heightWeightLayout";
            this.heightWeightLayout.RowCount = 4;
            this.heightWeightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.heightWeightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.heightWeightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.heightWeightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.heightWeightLayout.Size = new System.Drawing.Size(912, 120);
            this.heightWeightLayout.TabIndex = 0;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.heightWeightLayout.SetColumnSpan(this.descriptionLabel, 3);
            this.descriptionLabel.Location = new System.Drawing.Point(3, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(772, 13);
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "By changing the height and weight modifiers, you can change your character\'s meas" +
    "urements. The limits of the modifiers are determined by your choice of subrace.";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(3, 30);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 1;
            this.heightLabel.Text = "Height:";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(3, 60);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(44, 13);
            this.weightLabel.TabIndex = 2;
            this.weightLabel.Text = "Weight:";
            // 
            // heightModifier
            // 
            this.heightModifier.Location = new System.Drawing.Point(306, 33);
            this.heightModifier.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.heightModifier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightModifier.Name = "heightModifier";
            this.heightModifier.Size = new System.Drawing.Size(120, 20);
            this.heightModifier.TabIndex = 3;
            this.heightModifier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightModifier.ValueChanged += new System.EventHandler(this.heightModifier_ValueChanged);
            // 
            // weightModifier
            // 
            this.weightModifier.Location = new System.Drawing.Point(306, 63);
            this.weightModifier.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.weightModifier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.weightModifier.Name = "weightModifier";
            this.weightModifier.Size = new System.Drawing.Size(120, 20);
            this.weightModifier.TabIndex = 4;
            this.weightModifier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.weightModifier.ValueChanged += new System.EventHandler(this.weightModifier_ValueChanged);
            // 
            // heightResultLabel
            // 
            this.heightResultLabel.AutoSize = true;
            this.heightResultLabel.Location = new System.Drawing.Point(610, 30);
            this.heightResultLabel.Name = "heightResultLabel";
            this.heightResultLabel.Size = new System.Drawing.Size(64, 13);
            this.heightResultLabel.TabIndex = 5;
            this.heightResultLabel.Text = "height result";
            // 
            // weightResultLabel
            // 
            this.weightResultLabel.AutoSize = true;
            this.weightResultLabel.Location = new System.Drawing.Point(610, 60);
            this.weightResultLabel.Name = "weightResultLabel";
            this.weightResultLabel.Size = new System.Drawing.Size(66, 13);
            this.weightResultLabel.TabIndex = 6;
            this.weightResultLabel.Text = "weight result";
            // 
            // randomizeButton
            // 
            this.randomizeButton.Location = new System.Drawing.Point(306, 93);
            this.randomizeButton.Name = "randomizeButton";
            this.randomizeButton.Size = new System.Drawing.Size(168, 23);
            this.randomizeButton.TabIndex = 7;
            this.randomizeButton.Text = "Randomize Height/Weight";
            this.randomizeButton.UseVisualStyleBackColor = true;
            this.randomizeButton.Click += new System.EventHandler(this.randomizeButton_Click);
            // 
            // BodyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bodyBox);
            this.Name = "BodyControl";
            this.Size = new System.Drawing.Size(937, 160);
            this.bodyBox.ResumeLayout(false);
            this.heightWeightLayout.ResumeLayout(false);
            this.heightWeightLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightModifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightModifier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox bodyBox;
        private System.Windows.Forms.TableLayoutPanel heightWeightLayout;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.NumericUpDown heightModifier;
        private System.Windows.Forms.NumericUpDown weightModifier;
        private System.Windows.Forms.Label heightResultLabel;
        private System.Windows.Forms.Label weightResultLabel;
        private System.Windows.Forms.Button randomizeButton;
    }
}
