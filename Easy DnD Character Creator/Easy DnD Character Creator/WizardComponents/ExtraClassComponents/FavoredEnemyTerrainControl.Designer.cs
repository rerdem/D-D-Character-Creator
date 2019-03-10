namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class FavoredEnemyTerrainControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoredEnemyTerrainControl));
            this.favoredBox = new System.Windows.Forms.GroupBox();
            this.favoredLayout = new System.Windows.Forms.TableLayoutPanel();
            this.favoredLabel = new System.Windows.Forms.Label();
            this.terrainBox = new System.Windows.Forms.ListBox();
            this.enemyBox = new System.Windows.Forms.ListBox();
            this.favoredBox.SuspendLayout();
            this.favoredLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // favoredBox
            // 
            this.favoredBox.Controls.Add(this.favoredLayout);
            this.favoredBox.Location = new System.Drawing.Point(4, 4);
            this.favoredBox.Name = "favoredBox";
            this.favoredBox.Size = new System.Drawing.Size(890, 188);
            this.favoredBox.TabIndex = 0;
            this.favoredBox.TabStop = false;
            this.favoredBox.Text = "Favored Enemy / Favored Terrain";
            // 
            // favoredLayout
            // 
            this.favoredLayout.ColumnCount = 2;
            this.favoredLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.favoredLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.favoredLayout.Controls.Add(this.favoredLabel, 0, 0);
            this.favoredLayout.Controls.Add(this.terrainBox, 1, 1);
            this.favoredLayout.Controls.Add(this.enemyBox, 0, 1);
            this.favoredLayout.Location = new System.Drawing.Point(7, 20);
            this.favoredLayout.Name = "favoredLayout";
            this.favoredLayout.RowCount = 2;
            this.favoredLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.favoredLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.favoredLayout.Size = new System.Drawing.Size(877, 162);
            this.favoredLayout.TabIndex = 3;
            // 
            // favoredLabel
            // 
            this.favoredLabel.AutoSize = true;
            this.favoredLayout.SetColumnSpan(this.favoredLabel, 2);
            this.favoredLabel.Location = new System.Drawing.Point(3, 3);
            this.favoredLabel.Margin = new System.Windows.Forms.Padding(3);
            this.favoredLabel.MaximumSize = new System.Drawing.Size(898, 30);
            this.favoredLabel.Name = "favoredLabel";
            this.favoredLabel.Size = new System.Drawing.Size(864, 26);
            this.favoredLabel.TabIndex = 0;
            this.favoredLabel.Text = resources.GetString("favoredLabel.Text");
            // 
            // terrainBox
            // 
            this.terrainBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.terrainBox.FormattingEnabled = true;
            this.terrainBox.Location = new System.Drawing.Point(597, 45);
            this.terrainBox.Name = "terrainBox";
            this.terrainBox.Size = new System.Drawing.Size(120, 108);
            this.terrainBox.TabIndex = 2;
            this.terrainBox.SelectedIndexChanged += new System.EventHandler(this.terrainBox_SelectedIndexChanged);
            // 
            // enemyBox
            // 
            this.enemyBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enemyBox.FormattingEnabled = true;
            this.enemyBox.Location = new System.Drawing.Point(159, 45);
            this.enemyBox.Name = "enemyBox";
            this.enemyBox.Size = new System.Drawing.Size(120, 108);
            this.enemyBox.TabIndex = 1;
            this.enemyBox.SelectedIndexChanged += new System.EventHandler(this.enemyBox_SelectedIndexChanged);
            // 
            // FavoredEnemyTerrainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.favoredBox);
            this.Name = "FavoredEnemyTerrainControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.favoredBox.ResumeLayout(false);
            this.favoredLayout.ResumeLayout(false);
            this.favoredLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox favoredBox;
        private System.Windows.Forms.Label favoredLabel;
        private System.Windows.Forms.TableLayoutPanel favoredLayout;
        private System.Windows.Forms.ListBox terrainBox;
        private System.Windows.Forms.ListBox enemyBox;
    }
}
