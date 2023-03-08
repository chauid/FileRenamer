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
            ChangeButton = new Button();
            AppendRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            DeleteRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            NewPatternRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            ReplaceRadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            CloseButton = new Button();
            Divder = new MaterialSkin.Controls.MaterialDivider();
            DetailOptionPanel = new Panel();
            PatternTextBox = new TextBox();
            AdvancedDetailButton = new Button();
            SuspendLayout();
            // 
            // ChangeButton
            // 
            ChangeButton.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ChangeButton.Location = new Point(215, 215);
            ChangeButton.Name = "ChangeButton";
            ChangeButton.Size = new Size(85, 30);
            ChangeButton.TabIndex = 4;
            ChangeButton.Text = "변경";
            ChangeButton.UseVisualStyleBackColor = true;
            ChangeButton.Click += ChangeButton_Click;
            // 
            // AppendRadioButton
            // 
            AppendRadioButton.AutoSize = true;
            AppendRadioButton.Depth = 0;
            AppendRadioButton.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AppendRadioButton.Location = new Point(9, 6);
            AppendRadioButton.Margin = new Padding(0);
            AppendRadioButton.MouseLocation = new Point(-1, -1);
            AppendRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            AppendRadioButton.Name = "AppendRadioButton";
            AppendRadioButton.Ripple = true;
            AppendRadioButton.Size = new Size(139, 37);
            AppendRadioButton.TabIndex = 0;
            AppendRadioButton.TabStop = true;
            AppendRadioButton.Text = "기존 파일명에 추가";
            AppendRadioButton.UseVisualStyleBackColor = true;
            AppendRadioButton.CheckedChanged += AppendRadioButton_CheckedChanged;
            // 
            // DeleteRadioButton
            // 
            DeleteRadioButton.AutoSize = true;
            DeleteRadioButton.Depth = 0;
            DeleteRadioButton.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            DeleteRadioButton.Location = new Point(196, 6);
            DeleteRadioButton.Margin = new Padding(0);
            DeleteRadioButton.MouseLocation = new Point(-1, -1);
            DeleteRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            DeleteRadioButton.Name = "DeleteRadioButton";
            DeleteRadioButton.Ripple = true;
            DeleteRadioButton.Size = new Size(151, 37);
            DeleteRadioButton.TabIndex = 1;
            DeleteRadioButton.TabStop = true;
            DeleteRadioButton.Text = "기존 파일명에서 삭제";
            DeleteRadioButton.UseVisualStyleBackColor = true;
            DeleteRadioButton.CheckedChanged += DeleteRadioButton_CheckedChanged;
            // 
            // NewPatternRadioButton
            // 
            NewPatternRadioButton.AutoSize = true;
            NewPatternRadioButton.Depth = 0;
            NewPatternRadioButton.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            NewPatternRadioButton.Location = new Point(196, 43);
            NewPatternRadioButton.Margin = new Padding(0);
            NewPatternRadioButton.MouseLocation = new Point(-1, -1);
            NewPatternRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            NewPatternRadioButton.Name = "NewPatternRadioButton";
            NewPatternRadioButton.Ripple = true;
            NewPatternRadioButton.Size = new Size(146, 37);
            NewPatternRadioButton.TabIndex = 3;
            NewPatternRadioButton.TabStop = true;
            NewPatternRadioButton.Text = "새로운 파일명(고급)";
            NewPatternRadioButton.UseVisualStyleBackColor = true;
            NewPatternRadioButton.CheckedChanged += NewPatternRadioButton_CheckedChanged;
            // 
            // ReplaceRadioButton
            // 
            ReplaceRadioButton.AutoSize = true;
            ReplaceRadioButton.Depth = 0;
            ReplaceRadioButton.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ReplaceRadioButton.Location = new Point(9, 43);
            ReplaceRadioButton.Margin = new Padding(0);
            ReplaceRadioButton.MouseLocation = new Point(-1, -1);
            ReplaceRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            ReplaceRadioButton.Name = "ReplaceRadioButton";
            ReplaceRadioButton.Ripple = true;
            ReplaceRadioButton.Size = new Size(151, 37);
            ReplaceRadioButton.TabIndex = 2;
            ReplaceRadioButton.TabStop = true;
            ReplaceRadioButton.Text = "기존 파일명에서 대체";
            ReplaceRadioButton.UseVisualStyleBackColor = true;
            ReplaceRadioButton.CheckedChanged += ReplaceRadioButton_CheckedChanged;
            // 
            // CloseButton
            // 
            CloseButton.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            CloseButton.Location = new Point(305, 215);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(85, 30);
            CloseButton.TabIndex = 5;
            CloseButton.Text = "취소";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // Divder
            // 
            Divder.BackColor = Color.FromArgb(30, 0, 0, 0);
            Divder.Depth = 0;
            Divder.Location = new Point(8, 83);
            Divder.MouseState = MaterialSkin.MouseState.HOVER;
            Divder.Name = "Divder";
            Divder.Size = new Size(380, 5);
            Divder.TabIndex = 6;
            Divder.Text = "materialDivider1";
            // 
            // DetailOptionPanel
            // 
            DetailOptionPanel.Location = new Point(8, 90);
            DetailOptionPanel.Name = "DetailOptionPanel";
            DetailOptionPanel.Size = new Size(380, 110);
            DetailOptionPanel.TabIndex = 7;
            // 
            // PatternTextBox
            // 
            PatternTextBox.Location = new Point(55, 250);
            PatternTextBox.Name = "PatternTextBox";
            PatternTextBox.Size = new Size(280, 23);
            PatternTextBox.TabIndex = 0;
            PatternTextBox.TextChanged += PatternTextBox_TextChanged;
            // 
            // AdvancedDetailButton
            // 
            AdvancedDetailButton.Location = new Point(10, 210);
            AdvancedDetailButton.Name = "AdvancedDetailButton";
            AdvancedDetailButton.Size = new Size(95, 25);
            AdvancedDetailButton.TabIndex = 1;
            AdvancedDetailButton.Text = "고급 설정 ∨";
            AdvancedDetailButton.UseVisualStyleBackColor = true;
            AdvancedDetailButton.Click += AdvancedDetailButton_Click;
            // 
            // Rename
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(400, 250);
            Controls.Add(PatternTextBox);
            Controls.Add(AdvancedDetailButton);
            Controls.Add(DetailOptionPanel);
            Controls.Add(Divder);
            Controls.Add(CloseButton);
            Controls.Add(NewPatternRadioButton);
            Controls.Add(DeleteRadioButton);
            Controls.Add(ReplaceRadioButton);
            Controls.Add(AppendRadioButton);
            Controls.Add(ChangeButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Rename";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "이름 바꾸기";
            ResumeLayout(false);
            PerformLayout();
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