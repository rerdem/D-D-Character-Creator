namespace Easy_DnD_Character_Creator.WizardComponents.StoryComponents
{
    partial class PersonalityControl
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.choiceTextBox = new System.Windows.Forms.TextBox();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.customCheckBox = new System.Windows.Forms.CheckBox();
            this.randomButton = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.tableLayout);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(890, 108);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Heading";
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayout.Controls.Add(this.choiceTextBox, 0, 0);
            this.tableLayout.Controls.Add(this.upButton, 1, 0);
            this.tableLayout.Controls.Add(this.downButton, 1, 1);
            this.tableLayout.Controls.Add(this.customCheckBox, 0, 2);
            this.tableLayout.Controls.Add(this.randomButton, 1, 2);
            this.tableLayout.Location = new System.Drawing.Point(7, 20);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 3;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayout.Size = new System.Drawing.Size(877, 82);
            this.tableLayout.TabIndex = 0;
            // 
            // choiceTextBox
            // 
            this.choiceTextBox.Enabled = false;
            this.choiceTextBox.Location = new System.Drawing.Point(3, 3);
            this.choiceTextBox.Multiline = true;
            this.choiceTextBox.Name = "choiceTextBox";
            this.tableLayout.SetRowSpan(this.choiceTextBox, 2);
            this.choiceTextBox.Size = new System.Drawing.Size(783, 50);
            this.choiceTextBox.TabIndex = 0;
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(792, 3);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(75, 22);
            this.upButton.TabIndex = 1;
            this.upButton.Text = "button1";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(792, 31);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(75, 22);
            this.downButton.TabIndex = 2;
            this.downButton.Text = "button2";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // customCheckBox
            // 
            this.customCheckBox.AutoSize = true;
            this.customCheckBox.Location = new System.Drawing.Point(3, 59);
            this.customCheckBox.Name = "customCheckBox";
            this.customCheckBox.Size = new System.Drawing.Size(100, 17);
            this.customCheckBox.TabIndex = 3;
            this.customCheckBox.Text = "Write your own.";
            this.customCheckBox.UseVisualStyleBackColor = true;
            this.customCheckBox.CheckedChanged += new System.EventHandler(this.customCheckBox_CheckedChanged);
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(792, 59);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(75, 20);
            this.randomButton.TabIndex = 4;
            this.randomButton.Text = "Random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // PersonalityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "PersonalityControl";
            this.Size = new System.Drawing.Size(900, 120);
            this.groupBox.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.TextBox choiceTextBox;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.CheckBox customCheckBox;
        private System.Windows.Forms.Button randomButton;
    }
}
