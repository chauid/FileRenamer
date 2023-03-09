namespace FileManager
{
    partial class Rename
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChangeButton = new System.Windows.Forms.Button();
            this.AppendRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            this.DeleteRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            this.NewPatternRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            this.ReplaceRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            this.CloseButton = new System.Windows.Forms.Button();
            this.Divder = new MaterialSkin.Controls.MaterialDivider();
            this.DetailOptionPanel = new System.Windows.Forms.Panel();
            this.PatternTextBox = new System.Windows.Forms.TextBox();
            this.AdvancedDetailButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChangeButton
            // 
            this.ChangeButton.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ChangeButton.Location = new System.Drawing.Point(215, 215);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(85, 30);
            this.ChangeButton.TabIndex = 4;
            this.ChangeButton.Text = "변경";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // AppendRadioButton
            // 
            this.AppendRadioButton.AutoSize = true;
            this.AppendRadioButton.Depth = 0;
            this.AppendRadioButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AppendRadioButton.Location = new System.Drawing.Point(9, 6);
            this.AppendRadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.AppendRadioButton.MouseLocation = new System.Drawing.Point(-1, -1);
            this.AppendRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.AppendRadioButton.Name = "AppendRadioButton";
            this.AppendRadioButton.Ripple = true;
            this.AppendRadioButton.Size = new System.Drawing.Size(139, 37);
            this.AppendRadioButton.TabIndex = 0;
            this.AppendRadioButton.TabStop = true;
            this.AppendRadioButton.Text = "기존 파일명에 추가";
            this.AppendRadioButton.UseVisualStyleBackColor = true;
            this.AppendRadioButton.CheckedChanged += new System.EventHandler(this.AppendRadioButton_CheckedChanged);
            // 
            // DeleteRadioButton
            // 
            this.DeleteRadioButton.AutoSize = true;
            this.DeleteRadioButton.Depth = 0;
            this.DeleteRadioButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeleteRadioButton.Location = new System.Drawing.Point(196, 6);
            this.DeleteRadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.DeleteRadioButton.MouseLocation = new System.Drawing.Point(-1, -1);
            this.DeleteRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DeleteRadioButton.Name = "DeleteRadioButton";
            this.DeleteRadioButton.Ripple = true;
            this.DeleteRadioButton.Size = new System.Drawing.Size(151, 37);
            this.DeleteRadioButton.TabIndex = 1;
            this.DeleteRadioButton.TabStop = true;
            this.DeleteRadioButton.Text = "기존 파일명에서 삭제";
            this.DeleteRadioButton.UseVisualStyleBackColor = true;
            this.DeleteRadioButton.CheckedChanged += new System.EventHandler(this.DeleteRadioButton_CheckedChanged);
            // 
            // NewPatternRadioButton
            // 
            this.NewPatternRadioButton.AutoSize = true;
            this.NewPatternRadioButton.Depth = 0;
            this.NewPatternRadioButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NewPatternRadioButton.Location = new System.Drawing.Point(196, 43);
            this.NewPatternRadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.NewPatternRadioButton.MouseLocation = new System.Drawing.Point(-1, -1);
            this.NewPatternRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.NewPatternRadioButton.Name = "NewPatternRadioButton";
            this.NewPatternRadioButton.Ripple = true;
            this.NewPatternRadioButton.Size = new System.Drawing.Size(146, 37);
            this.NewPatternRadioButton.TabIndex = 3;
            this.NewPatternRadioButton.TabStop = true;
            this.NewPatternRadioButton.Text = "새로운 파일명(고급)";
            this.NewPatternRadioButton.UseVisualStyleBackColor = true;
            this.NewPatternRadioButton.CheckedChanged += new System.EventHandler(this.NewPatternRadioButton_CheckedChanged);
            // 
            // ReplaceRadioButton
            // 
            this.ReplaceRadioButton.AutoSize = true;
            this.ReplaceRadioButton.Depth = 0;
            this.ReplaceRadioButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReplaceRadioButton.Location = new System.Drawing.Point(9, 43);
            this.ReplaceRadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReplaceRadioButton.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ReplaceRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.ReplaceRadioButton.Name = "ReplaceRadioButton";
            this.ReplaceRadioButton.Ripple = true;
            this.ReplaceRadioButton.Size = new System.Drawing.Size(151, 37);
            this.ReplaceRadioButton.TabIndex = 2;
            this.ReplaceRadioButton.TabStop = true;
            this.ReplaceRadioButton.Text = "기존 파일명에서 대체";
            this.ReplaceRadioButton.UseVisualStyleBackColor = true;
            this.ReplaceRadioButton.CheckedChanged += new System.EventHandler(this.ReplaceRadioButton_CheckedChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.Location = new System.Drawing.Point(305, 215);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(85, 30);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "취소";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Divder
            // 
            this.Divder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Divder.Depth = 0;
            this.Divder.Location = new System.Drawing.Point(8, 83);
            this.Divder.MouseState = MaterialSkin.MouseState.HOVER;
            this.Divder.Name = "Divder";
            this.Divder.Size = new System.Drawing.Size(380, 5);
            this.Divder.TabIndex = 6;
            this.Divder.Text = "materialDivider1";
            // 
            // DetailOptionPanel
            // 
            this.DetailOptionPanel.Location = new System.Drawing.Point(8, 90);
            this.DetailOptionPanel.Name = "DetailOptionPanel";
            this.DetailOptionPanel.Size = new System.Drawing.Size(380, 110);
            this.DetailOptionPanel.TabIndex = 7;
            // 
            // PatternTextBox
            // 
            this.PatternTextBox.Location = new System.Drawing.Point(55, 250);
            this.PatternTextBox.Name = "PatternTextBox";
            this.PatternTextBox.Size = new System.Drawing.Size(280, 23);
            this.PatternTextBox.TabIndex = 0;
            this.PatternTextBox.TextChanged += new System.EventHandler(this.PatternTextBox_TextChanged);
            // 
            // AdvancedDetailButton
            // 
            this.AdvancedDetailButton.Location = new System.Drawing.Point(10, 210);
            this.AdvancedDetailButton.Name = "AdvancedDetailButton";
            this.AdvancedDetailButton.Size = new System.Drawing.Size(95, 25);
            this.AdvancedDetailButton.TabIndex = 1;
            this.AdvancedDetailButton.Text = "고급 설정 ∨";
            this.AdvancedDetailButton.UseVisualStyleBackColor = true;
            this.AdvancedDetailButton.Click += new System.EventHandler(this.AdvancedDetailButton_Click);
            // 
            // Rename
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.PatternTextBox);
            this.Controls.Add(this.AdvancedDetailButton);
            this.Controls.Add(this.DetailOptionPanel);
            this.Controls.Add(this.Divder);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.NewPatternRadioButton);
            this.Controls.Add(this.DeleteRadioButton);
            this.Controls.Add(this.ReplaceRadioButton);
            this.Controls.Add(this.AppendRadioButton);
            this.Controls.Add(this.ChangeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Rename";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "이름 바꾸기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ChangeButton;
        private Button CloseButton;
        private MaterialSkin.Controls.MaterialRadioButton AppendRadioButton;
        private MaterialSkin.Controls.MaterialRadioButton DeleteRadioButton;
        private MaterialSkin.Controls.MaterialRadioButton NewPatternRadioButton;
        private MaterialSkin.Controls.MaterialRadioButton ReplaceRadioButton;
        private MaterialSkin.Controls.MaterialDivider Divder;
        private Panel DetailOptionPanel;
        private TextBox PatternTextBox;
        private Button AdvancedDetailButton;
    }
}