namespace FileRenamer
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
            PreviewButton = new Button();
            ErrorTextBox = new Label();
            AppendButton = new MaterialSkin.Controls.MaterialButton();
            ReplaceButton = new MaterialSkin.Controls.MaterialButton();
            DeleteButton = new MaterialSkin.Controls.MaterialButton();
            NewPatternButton = new MaterialSkin.Controls.MaterialButton();
            AdvancedDetailLabel = new Label();
            materialRadioButton1 = new MaterialSkin.Controls.MaterialRadioButton();
            SuspendLayout();
            // 
            // ChangeButton
            // 
            ChangeButton.BackColor = Color.Cyan;
            ChangeButton.FlatAppearance.BorderSize = 0;
            ChangeButton.FlatAppearance.MouseDownBackColor = Color.DarkCyan;
            ChangeButton.FlatAppearance.MouseOverBackColor = Color.Aqua;
            ChangeButton.FlatStyle = FlatStyle.Flat;
            ChangeButton.Font = new Font("맑은 고딕", 10F);
            ChangeButton.Location = new Point(477, 331);
            ChangeButton.Name = "ChangeButton";
            ChangeButton.Size = new Size(85, 30);
            ChangeButton.TabIndex = 4;
            ChangeButton.Text = "변경";
            ChangeButton.UseVisualStyleBackColor = false;
            ChangeButton.Click += ChangeButton_Click;
            // 
            // AppendRadioButton
            // 
            AppendRadioButton.AutoSize = true;
            AppendRadioButton.Depth = 0;
            AppendRadioButton.Font = new Font("맑은 고딕", 9F);
            AppendRadioButton.Location = new Point(10, 10);
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
            DeleteRadioButton.Font = new Font("맑은 고딕", 9F);
            DeleteRadioButton.Location = new Point(179, 10);
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
            NewPatternRadioButton.Font = new Font("맑은 고딕", 9F);
            NewPatternRadioButton.Location = new Point(541, 10);
            NewPatternRadioButton.Margin = new Padding(0);
            NewPatternRadioButton.MouseLocation = new Point(-1, -1);
            NewPatternRadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            NewPatternRadioButton.Name = "NewPatternRadioButton";
            NewPatternRadioButton.Ripple = true;
            NewPatternRadioButton.Size = new Size(111, 37);
            NewPatternRadioButton.TabIndex = 3;
            NewPatternRadioButton.TabStop = true;
            NewPatternRadioButton.Text = "새로운 파일명";
            NewPatternRadioButton.UseVisualStyleBackColor = true;
            NewPatternRadioButton.CheckedChanged += NewPatternRadioButton_CheckedChanged;
            // 
            // ReplaceRadioButton
            // 
            ReplaceRadioButton.AutoSize = true;
            ReplaceRadioButton.Depth = 0;
            ReplaceRadioButton.Font = new Font("맑은 고딕", 9F);
            ReplaceRadioButton.Location = new Point(360, 10);
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
            CloseButton.BackColor = Color.LightGray;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatAppearance.MouseDownBackColor = Color.Silver;
            CloseButton.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Font = new Font("맑은 고딕", 10F);
            CloseButton.Location = new Point(567, 331);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(85, 30);
            CloseButton.TabIndex = 5;
            CloseButton.Text = "취소";
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // Divder
            // 
            Divder.BackColor = Color.FromArgb(30, 0, 0, 0);
            Divder.Depth = 0;
            Divder.Location = new Point(10, 57);
            Divder.MouseState = MaterialSkin.MouseState.HOVER;
            Divder.Name = "Divder";
            Divder.Size = new Size(642, 5);
            Divder.TabIndex = 6;
            Divder.Text = "materialDivider1";
            // 
            // DetailOptionPanel
            // 
            DetailOptionPanel.Location = new Point(10, 67);
            DetailOptionPanel.Name = "DetailOptionPanel";
            DetailOptionPanel.Size = new Size(642, 110);
            DetailOptionPanel.TabIndex = 7;
            // 
            // PatternTextBox
            // 
            PatternTextBox.Location = new Point(10, 266);
            PatternTextBox.Name = "PatternTextBox";
            PatternTextBox.Size = new Size(642, 23);
            PatternTextBox.TabIndex = 0;
            PatternTextBox.TextChanged += PatternTextBox_TextChanged;
            // 
            // PreviewButton
            // 
            PreviewButton.BackColor = Color.LightSteelBlue;
            PreviewButton.FlatAppearance.BorderSize = 0;
            PreviewButton.FlatAppearance.MouseDownBackColor = Color.LightSlateGray;
            PreviewButton.FlatAppearance.MouseOverBackColor = Color.LightSkyBlue;
            PreviewButton.FlatStyle = FlatStyle.Flat;
            PreviewButton.Font = new Font("맑은 고딕", 10F);
            PreviewButton.Location = new Point(10, 331);
            PreviewButton.Name = "PreviewButton";
            PreviewButton.Size = new Size(80, 30);
            PreviewButton.TabIndex = 8;
            PreviewButton.Text = "미리보기";
            PreviewButton.UseVisualStyleBackColor = false;
            PreviewButton.Click += PreviewButton_Click;
            // 
            // ErrorTextBox
            // 
            ErrorTextBox.AutoSize = true;
            ErrorTextBox.ForeColor = Color.Red;
            ErrorTextBox.Location = new Point(10, 296);
            ErrorTextBox.Name = "ErrorTextBox";
            ErrorTextBox.Size = new Size(0, 15);
            ErrorTextBox.TabIndex = 9;
            // 
            // AppendButton
            // 
            AppendButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AppendButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            AppendButton.Depth = 0;
            AppendButton.HighEmphasis = true;
            AppendButton.Icon = null;
            AppendButton.Location = new Point(10, 221);
            AppendButton.Margin = new Padding(4, 6, 4, 6);
            AppendButton.MouseState = MaterialSkin.MouseState.HOVER;
            AppendButton.Name = "AppendButton";
            AppendButton.NoAccentTextColor = Color.Empty;
            AppendButton.Size = new Size(78, 36);
            AppendButton.TabIndex = 0;
            AppendButton.Text = "Append";
            AppendButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            AppendButton.UseAccentColor = false;
            AppendButton.UseVisualStyleBackColor = true;
            // 
            // ReplaceButton
            // 
            ReplaceButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ReplaceButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            ReplaceButton.Depth = 0;
            ReplaceButton.HighEmphasis = true;
            ReplaceButton.Icon = null;
            ReplaceButton.Location = new Point(98, 221);
            ReplaceButton.Margin = new Padding(4, 6, 4, 6);
            ReplaceButton.MouseState = MaterialSkin.MouseState.HOVER;
            ReplaceButton.Name = "ReplaceButton";
            ReplaceButton.NoAccentTextColor = Color.Empty;
            ReplaceButton.Size = new Size(84, 36);
            ReplaceButton.TabIndex = 14;
            ReplaceButton.Text = "Replace";
            ReplaceButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            ReplaceButton.UseAccentColor = false;
            ReplaceButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            DeleteButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DeleteButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            DeleteButton.Depth = 0;
            DeleteButton.HighEmphasis = true;
            DeleteButton.Icon = null;
            DeleteButton.Location = new Point(192, 221);
            DeleteButton.Margin = new Padding(4, 6, 4, 6);
            DeleteButton.MouseState = MaterialSkin.MouseState.HOVER;
            DeleteButton.Name = "DeleteButton";
            DeleteButton.NoAccentTextColor = Color.Empty;
            DeleteButton.Size = new Size(73, 36);
            DeleteButton.TabIndex = 15;
            DeleteButton.Text = "Delete";
            DeleteButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            DeleteButton.UseAccentColor = false;
            DeleteButton.UseVisualStyleBackColor = true;
            // 
            // NewPatternButton
            // 
            NewPatternButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            NewPatternButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            NewPatternButton.Depth = 0;
            NewPatternButton.HighEmphasis = true;
            NewPatternButton.Icon = null;
            NewPatternButton.Location = new Point(275, 221);
            NewPatternButton.Margin = new Padding(4, 6, 4, 6);
            NewPatternButton.MouseState = MaterialSkin.MouseState.HOVER;
            NewPatternButton.Name = "NewPatternButton";
            NewPatternButton.NoAccentTextColor = Color.Empty;
            NewPatternButton.Size = new Size(64, 36);
            NewPatternButton.TabIndex = 16;
            NewPatternButton.Text = "New";
            NewPatternButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            NewPatternButton.UseAccentColor = false;
            NewPatternButton.UseVisualStyleBackColor = true;
            // 
            // AdvancedDetailLabel
            // 
            AdvancedDetailLabel.AutoSize = true;
            AdvancedDetailLabel.Location = new Point(10, 200);
            AdvancedDetailLabel.Name = "AdvancedDetailLabel";
            AdvancedDetailLabel.Size = new Size(59, 15);
            AdvancedDetailLabel.TabIndex = 17;
            AdvancedDetailLabel.Text = "고급 설정";
            // 
            // materialRadioButton1
            // 
            materialRadioButton1.AutoSize = true;
            materialRadioButton1.Depth = 0;
            materialRadioButton1.Location = new Point(553, 220);
            materialRadioButton1.Margin = new Padding(0);
            materialRadioButton1.MouseLocation = new Point(-1, -1);
            materialRadioButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialRadioButton1.Name = "materialRadioButton1";
            materialRadioButton1.Ripple = true;
            materialRadioButton1.Size = new Size(99, 37);
            materialRadioButton1.TabIndex = 18;
            materialRadioButton1.TabStop = true;
            materialRadioButton1.Text = "사용자 지정";
            materialRadioButton1.UseVisualStyleBackColor = true;
            // 
            // Rename
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(662, 371);
            Controls.Add(materialRadioButton1);
            Controls.Add(AdvancedDetailLabel);
            Controls.Add(NewPatternButton);
            Controls.Add(DeleteButton);
            Controls.Add(ReplaceButton);
            Controls.Add(AppendButton);
            Controls.Add(ErrorTextBox);
            Controls.Add(PreviewButton);
            Controls.Add(PatternTextBox);
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
        private Button PreviewButton;
        private Label ErrorTextBox;
        private MaterialSkin.Controls.MaterialButton AppendButton;
        private MaterialSkin.Controls.MaterialButton ReplaceButton;
        private MaterialSkin.Controls.MaterialButton DeleteButton;
        private MaterialSkin.Controls.MaterialButton NewPatternButton;
        private Label AdvancedDetailLabel;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButton1;
    }
}