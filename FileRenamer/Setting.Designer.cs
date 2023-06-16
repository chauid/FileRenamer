namespace FileRenamer
{
    partial class Setting
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
            DefaultFileNameLabel = new Label();
            InputFileNameTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            BatchDateTimeCheckBox = new MaterialSkin.Controls.MaterialCheckbox();
            ApplyButton = new Button();
            CloseButton = new Button();
            LabelDateTimeInfo = new Label();
            SuspendLayout();
            // 
            // DefaultFileNameLabel
            // 
            DefaultFileNameLabel.AutoSize = true;
            DefaultFileNameLabel.Location = new Point(39, 30);
            DefaultFileNameLabel.Name = "DefaultFileNameLabel";
            DefaultFileNameLabel.Size = new Size(126, 15);
            DefaultFileNameLabel.TabIndex = 0;
            DefaultFileNameLabel.Text = "현재 기본 파일 이름 : ";
            // 
            // InputFileNameTextBox
            // 
            InputFileNameTextBox.AnimateReadOnly = false;
            InputFileNameTextBox.BackgroundImageLayout = ImageLayout.None;
            InputFileNameTextBox.CharacterCasing = CharacterCasing.Normal;
            InputFileNameTextBox.Depth = 0;
            InputFileNameTextBox.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            InputFileNameTextBox.HideSelection = true;
            InputFileNameTextBox.Hint = "기본 생성 파일 이름 설정";
            InputFileNameTextBox.LeadingIcon = null;
            InputFileNameTextBox.Location = new Point(39, 58);
            InputFileNameTextBox.MaxLength = 32767;
            InputFileNameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            InputFileNameTextBox.Name = "InputFileNameTextBox";
            InputFileNameTextBox.PasswordChar = '\0';
            InputFileNameTextBox.PrefixSuffixText = null;
            InputFileNameTextBox.ReadOnly = false;
            InputFileNameTextBox.RightToLeft = RightToLeft.No;
            InputFileNameTextBox.SelectedText = "";
            InputFileNameTextBox.SelectionLength = 0;
            InputFileNameTextBox.SelectionStart = 0;
            InputFileNameTextBox.ShortcutsEnabled = true;
            InputFileNameTextBox.Size = new Size(330, 48);
            InputFileNameTextBox.TabIndex = 1;
            InputFileNameTextBox.TabStop = false;
            InputFileNameTextBox.TextAlign = HorizontalAlignment.Left;
            InputFileNameTextBox.TrailingIcon = null;
            InputFileNameTextBox.UseSystemPasswordChar = false;
            InputFileNameTextBox.KeyPress += InputFileNameTextBox_KeyPress;
            InputFileNameTextBox.TextChanged += InputFileNameTextBox_TextChanged;
            // 
            // BatchDateTimeCheckBox
            // 
            BatchDateTimeCheckBox.AutoSize = true;
            BatchDateTimeCheckBox.Depth = 0;
            BatchDateTimeCheckBox.Location = new Point(39, 130);
            BatchDateTimeCheckBox.Margin = new Padding(0);
            BatchDateTimeCheckBox.MouseLocation = new Point(-1, -1);
            BatchDateTimeCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            BatchDateTimeCheckBox.Name = "BatchDateTimeCheckBox";
            BatchDateTimeCheckBox.ReadOnly = false;
            BatchDateTimeCheckBox.Ripple = true;
            BatchDateTimeCheckBox.Size = new Size(285, 37);
            BatchDateTimeCheckBox.TabIndex = 2;
            BatchDateTimeCheckBox.Text = "만든 날짜, 수정한 날짜, 액세스 날짜 일괄 변경";
            BatchDateTimeCheckBox.UseVisualStyleBackColor = true;
            BatchDateTimeCheckBox.CheckedChanged += BatchDateTimeCheckBox_CheckedChanged;
            // 
            // ApplyButton
            // 
            ApplyButton.BackColor = Color.Cyan;
            ApplyButton.FlatAppearance.BorderSize = 0;
            ApplyButton.FlatAppearance.MouseDownBackColor = Color.Thistle;
            ApplyButton.FlatAppearance.MouseOverBackColor = Color.LawnGreen;
            ApplyButton.FlatStyle = FlatStyle.Flat;
            ApplyButton.Location = new Point(215, 210);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(85, 30);
            ApplyButton.TabIndex = 3;
            ApplyButton.Text = "확인";
            ApplyButton.UseVisualStyleBackColor = false;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.LightGray;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatAppearance.MouseDownBackColor = Color.Thistle;
            CloseButton.FlatAppearance.MouseOverBackColor = Color.Salmon;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Location = new Point(305, 210);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(85, 30);
            CloseButton.TabIndex = 4;
            CloseButton.Text = "취소";
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // LabelDateTimeInfo
            // 
            LabelDateTimeInfo.AutoSize = true;
            LabelDateTimeInfo.ForeColor = SystemColors.ControlDarkDark;
            LabelDateTimeInfo.Location = new Point(73, 165);
            LabelDateTimeInfo.Name = "LabelDateTimeInfo";
            LabelDateTimeInfo.Size = new Size(202, 15);
            LabelDateTimeInfo.TabIndex = 5;
            LabelDateTimeInfo.Text = "(날짜 변경 선택 과정을 생략합니다.)";
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(400, 250);
            Controls.Add(BatchDateTimeCheckBox);
            Controls.Add(LabelDateTimeInfo);
            Controls.Add(CloseButton);
            Controls.Add(ApplyButton);
            Controls.Add(InputFileNameTextBox);
            Controls.Add(DefaultFileNameLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Setting";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "설정";
            Load += Setting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label DefaultFileNameLabel;
        private MaterialSkin.Controls.MaterialTextBox2 InputFileNameTextBox;
        private MaterialSkin.Controls.MaterialCheckbox BatchDateTimeCheckBox;
        private Button ApplyButton;
        private Button CloseButton;
        private Label LabelDateTimeInfo;
    }
}