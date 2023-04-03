using MaterialSkin.Controls;

namespace FileRenamer
{
    /// <summary>
    /// 이름 바꾸기 이벤트 핸들러 
    /// </summary>
    /// <param name="Regular">이름 변경 표현식(정규식)</param>
    public delegate void RenameEventArgs(string? Regular);
    public partial class Rename : Form
    {
        private bool AdvancedOption = false;
        public event RenameEventArgs? ExcuteRename;

        #region Rename
        public Rename()
        {
            InitializeComponent();
            DetailOptionPanel.Select();
            AppendRadioButton.Checked = true;
            PatternTextBox.KeyPress += KeyPress_RenameTextBox;

            // 종료 이벤트 
            foreach (Control controls in this.Controls)
            {
                controls.KeyDown += KeyDown_RenameForm;
            }
        }
        #endregion

        #region OKCancel
        private void ChangeButton_Click(object sender, EventArgs e) // 변경 
        {
            ExcuteRename?.Invoke(PatternTextBox.Text);
            Close();
        }
        private void CloseButton_Click(object sender, EventArgs e) { Close(); } // 취소 
        #endregion

        #region Append
        private void AppendRadioButton_CheckedChanged(object sender, EventArgs e) // Append 
        {
            if (AppendRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
                MaterialRadioButton AppendBeforeRadioButton = new()
                {
                    Location = new Point(0, 0),
                    AutoSize = true,
                    Text = "파일명 앞에 추가"
                };
                MaterialRadioButton AppendAfterRadioButton = new()
                {
                    Location = new Point(AppendBeforeRadioButton.Width + 80, 0),
                    AutoSize = true,
                    Text = "파일명 뒤에 추가"
                };
                MaterialTextBox2 AppendTextBox = new()
                {
                    Location = new Point(55, 60),
                    AutoSize = true
                };
                AppendBeforeRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendBeforeRadioButton.Checked)
                    {
                        AppendTextBox.Hint = "파일명 앞에 추가할 문자열 입력";
                        if (AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{Append:\"{0}\", 0, {1}}}", AppendTextBox.Text, true);
                    }
                };
                AppendAfterRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendAfterRadioButton.Checked)
                    {
                        AppendTextBox.Hint = "파일명 뒤에 추가할 문자열 입력";
                        if (AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{Append:\"{0}\", 0, {1}}}", AppendTextBox.Text, false);
                    }
                };
                AppendTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendBeforeRadioButton.Checked && AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{Append:\"{0}\", 0, {1}}}", AppendTextBox.Text, true);
                    if (AppendAfterRadioButton.Checked && AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{Append:\"{0}\", 0, {1}}}", AppendTextBox.Text, false);
                    if (AppendTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                AppendTextBox.KeyPress += KeyPress_RenameTextBox;
                PatternTextBox.Text = string.Empty;
                DetailOptionPanel.Controls.Add(AppendBeforeRadioButton);
                DetailOptionPanel.Controls.Add(AppendAfterRadioButton);
                DetailOptionPanel.Controls.Add(AppendTextBox);
                AppendBeforeRadioButton.Checked = true;
                AppendTextBox.Focus();
                AppendTextBox.Select();

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }
        #endregion

        #region Delete
        private void DeleteRadioButton_CheckedChanged(object sender, EventArgs e) // Delete 
        {
            if (DeleteRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
                MaterialRadioButton DeleteBeforeRadioButton = new()
                {
                    Location = new Point(0, 0),
                    AutoSize = true,
                    Text = "파일명 앞에서부터 삭제"
                };
                MaterialRadioButton DeleteAfterRadioButton = new()
                {
                    Location = new Point(DeleteBeforeRadioButton.Width + 80, 0),
                    AutoSize = true,
                    Text = "파일명 뒤에서부터 삭제"
                };
                Label DeleteNumberLabel = new()
                {
                    Location = new Point(70, 62),
                    Text = "삭제 범위 :",
                    AutoSize = true
                };
                NumericUpDown DeleteNumeric = new()
                {
                    Location = new Point(140, 60),
                    Maximum = 65535,
                    Minimum = 1,
                    Value = 1,
                    ThousandsSeparator = true,
                    AutoSize = true
                };
                DeleteBeforeRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    PatternTextBox.Text = string.Format("{{Delete:{0}, 0, {1}}}", DeleteNumeric.Value, true);
                };
                DeleteAfterRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    PatternTextBox.Text = string.Format("{{Delete:{0}, 0, {1}}}", DeleteNumeric.Value, false);
                };
                DeleteNumeric.ValueChanged += delegate (object? sender, EventArgs e)
                {
                    if (DeleteBeforeRadioButton.Checked) PatternTextBox.Text = string.Format("{{Delete:{0}, 0, {1}}}", DeleteNumeric.Value, true);
                    if (DeleteAfterRadioButton.Checked) PatternTextBox.Text = string.Format("{{Delete:{0}, 0, {1}}}", DeleteNumeric.Value, false);
                };
                DetailOptionPanel.Controls.Add(DeleteBeforeRadioButton);
                DetailOptionPanel.Controls.Add(DeleteAfterRadioButton);
                DetailOptionPanel.Controls.Add(DeleteNumberLabel);
                DetailOptionPanel.Controls.Add(DeleteNumeric);
                DeleteBeforeRadioButton.Checked = true;

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }
        #endregion

        #region Replace
        private void ReplaceRadioButton_CheckedChanged(object sender, EventArgs e) // Replace 
        {
            if (ReplaceRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
                MaterialTextBox2 SearchTextBox = new()
                {
                    Location = new Point(55, 5),
                    Hint = "찾을 문자열",
                    AutoSize = true
                };
                MaterialTextBox2 ReplaceTextBox = new()
                {
                    Location = new Point(55, 60),
                    Hint = "대체할 문자열",
                    AutoSize = true
                };
                SearchTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (SearchTextBox.Text != string.Empty && ReplaceTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{Replace:\"{0}\", \"{1}\"}}", SearchTextBox.Text, ReplaceTextBox.Text);
                    if (SearchTextBox.Text == string.Empty || ReplaceTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                SearchTextBox.KeyPress += KeyPress_RenameTextBox;
                ReplaceTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (SearchTextBox.Text != string.Empty && ReplaceTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{Replace:\"{0}\", \"{1}\"}}", SearchTextBox.Text, ReplaceTextBox.Text);
                    if (SearchTextBox.Text == string.Empty || ReplaceTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                ReplaceTextBox.KeyPress += KeyPress_RenameTextBox;
                PatternTextBox.Text = string.Empty;
                DetailOptionPanel.Controls.Add(SearchTextBox);
                DetailOptionPanel.Controls.Add(ReplaceTextBox);

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }
        #endregion

        #region NewPattern
        private void NewPatternRadioButton_CheckedChanged(object sender, EventArgs e) // NewPattern 
        {
            if (NewPatternRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
                MaterialCheckbox AutoIncrement = new()
                {
                    Location = new Point(10, 0),
                    Text = "자동 숫자 증가 설정",
                    AutoSize = true
                };
                Label StartNumberLabel = new()
                {
                    Location = new Point(170, 12),
                    Text = "시작 숫자 :",
                    AutoSize = true
                };
                NumericUpDown StartNumber = new()
                {
                    Location = new Point(240, 10),
                    Maximum = 65535,
                    Minimum = 1,
                    Value = 1,
                    ThousandsSeparator = true,
                    AutoSize = true
                };
                MaterialTextBox2 NewNameTextBox = new()
                {
                    Location = new Point(20, 60),
                    Size = new Size(220, 48),
                    Hint = "새로운 파일명 입력"
                };
                MaterialTextBox2 ExtensionTextBox = new()
                {
                    Location = new Point(260, 60),
                    Size = new Size(100, 48),
                    Hint = "확장자 입력"
                };
                AutoIncrement.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    if (AutoIncrement.Checked)
                    {
                        StartNumberLabel.Show();
                        StartNumber.Show();
                        if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, {2}}}", NewNameTextBox.Text, StartNumber.Value, 1);
                    }
                    else
                    {
                        StartNumberLabel.Hide();
                        StartNumber.Hide();
                        if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:1, 1}}", NewNameTextBox.Text);
                    }
                };
                StartNumber.ValueChanged += delegate (object? sender, EventArgs e)
                {
                    if (NewNameTextBox.Text != string.Empty)
                    {
                        if (AutoIncrement.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, {2}}}", NewNameTextBox.Text, StartNumber.Value, 1);
                        else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:1, 1}}", NewNameTextBox.Text);
                    }
                    if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                NewNameTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (NewNameTextBox.Text != string.Empty)
                    {
                        if (ExtensionTextBox.Text != string.Empty)
                        {
                            if (AutoIncrement.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, 1}}{{NewNameSet:\".{2}\"}}", NewNameTextBox.Text, StartNumber.Value, ExtensionTextBox.Text);
                            else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:1, 1}}{{NewNameSet:\"{1}\"}}", NewNameTextBox.Text, ExtensionTextBox.Text);
                        }
                        else
                        {
                            if (AutoIncrement.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, 1}}", NewNameTextBox.Text, StartNumber.Value);
                            else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:1, 1}}", NewNameTextBox.Text);
                        }
                    }
                    if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = "{FileName}";
                };
                ExtensionTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (NewNameTextBox.Text != string.Empty)
                    {
                        if (ExtensionTextBox.Text != string.Empty)
                        {
                            if (AutoIncrement.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, 1}}{{NewNameSet:\".{2}\"}}", NewNameTextBox.Text, StartNumber.Value, ExtensionTextBox.Text);
                            else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:1, 1}}{{NewNameSet:\"{1}\"}}", NewNameTextBox.Text, ExtensionTextBox.Text);
                        }
                        else
                        {
                            if (AutoIncrement.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, 1}}", NewNameTextBox.Text, StartNumber.Value);
                            else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:1, 1}}", NewNameTextBox.Text);
                        }
                    }
                    if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                NewNameTextBox.KeyPress += KeyPress_RenameTextBox;
                PatternTextBox.Text = string.Empty;
                DetailOptionPanel.Controls.Add(AutoIncrement);
                DetailOptionPanel.Controls.Add(StartNumberLabel);
                DetailOptionPanel.Controls.Add(StartNumber);
                DetailOptionPanel.Controls.Add(NewNameTextBox);
                DetailOptionPanel.Controls.Add(ExtensionTextBox);
                AutoIncrement.Checked = true;

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }
        #endregion

        private void AdvancedDetailButton_Click(object sender, EventArgs e)
        {
            if (AdvancedOption)
            {
                ClientSize = new Size(400, 250);
                ChangeButton.Location = new Point(215, 215);
                CloseButton.Location = new Point(305, 215);
                AdvancedDetailButton.Text = "고급 설정 ∨";
                AdvancedOption = false;
            }
            else
            {
                ClientSize = new Size(400, 320);
                ChangeButton.Location = new Point(215, 280);
                CloseButton.Location = new Point(305, 280);
                AdvancedDetailButton.Text = "고급 설정 ∧";
                AdvancedOption = true;
            }
        }
        private void KeyDown_RenameForm(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
        private void KeyPress_RenameTextBox(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape) e.Handled = true;

            // 파일명 유효성 검사 KeyCode 
            // /,?, |, \, <, >, ", *, :
            if (e.KeyChar == '/' || e.KeyChar == '?' || e.KeyChar == '|' || e.KeyChar == '\\' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '"' || e.KeyChar == '*' || e.KeyChar == ':' || e.KeyChar == '<')
            {
                Console.WriteLine("Invalid KeyCode");
                e.KeyChar = Convert.ToChar(0);
            }
        }
        private void PatternTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PatternTextBox.Text.Length > 15)
            {
                // 파일명 유효성 검사 KeyCode
                // /,?, |, \, <, >, ", *, :

                // 유효성 금지 문자 무차별 대입시 "의 처음과 끝을 구분하여 "내부 문자열"을 추출하여 정규식 유효성 검사 실행해야 함
                // 처음 "와 끝 "의 구분해야 함
            }
        }
    }
}
