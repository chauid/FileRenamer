using MaterialSkin.Controls;

namespace FileRenamer
{
    /// <summary>
    /// 이름 바꾸기 이벤트 핸들러 
    /// </summary>
    /// <param name="NameExpression">이름 변경 표현식</param>
    public delegate void RenameEventArgs(string? NameExpression);

    public partial class Rename : Form
    {
        private bool isRenameAvailable = true;
        private readonly string errorMsg = "";
        private bool advancedOption = false;
        public event RenameEventArgs? ExcuteRename;

        #region Rename
        public Rename()
        {
            InitializeComponent();
            DetailOptionPanel.Select();
            AppendRadioButton.Checked = true;
            PatternTextBox.KeyPress += KeyPress_RenameTextBox;

            // 종료 이벤트 
            foreach (Control controls in Controls)
            {
                controls.KeyDown += KeyDown_RenameForm;
            }
        }
        #endregion

        #region OKCancel
        private void ChangeButton_Click(object sender, EventArgs e) // 변경 
        {
            if (!isRenameAvailable)
            {
                ExcuteRename?.Invoke(PatternTextBox.Text);
                Close();
            }
            else MessageBox.Show(errorMsg, "변경 오류", MessageBoxButtons.OK);
        }

        private void CloseButton_Click(object sender, EventArgs e) { Close(); } // 취소 
        #endregion

        #region Append
        private void AppendRadioButton_CheckedChanged(object sender, EventArgs e) // Append 
        {
            if (!AppendRadioButton.Checked) return;
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
            AppendTextBox.Leave += Leave_RenameTextBox;
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
        #endregion

        #region Delete
        private void DeleteRadioButton_CheckedChanged(object sender, EventArgs e) // Delete 
        {
            if (!DeleteRadioButton.Checked) return;
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
        #endregion

        #region Replace
        private void ReplaceRadioButton_CheckedChanged(object sender, EventArgs e) // Replace 
        {
            if (!ReplaceRadioButton.Checked) return;
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
        #endregion

        #region NewPattern
        private void NewPatternRadioButton_CheckedChanged(object sender, EventArgs e) // NewPattern 
        {
            if (!NewPatternRadioButton.Checked) return;
            DetailOptionPanel.Controls.Clear();
            MaterialCheckbox Increment = new()
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
                Width = 100,
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
            Increment.CheckedChanged += delegate (object? sender, EventArgs e)
            {
                if (Increment.Checked)
                {
                    StartNumberLabel.Show();
                    StartNumber.Show();
                    if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:{1}, {2}}}", NewNameTextBox.Text, StartNumber.Value, 1);
                }
                else
                {
                    StartNumberLabel.Hide();
                    StartNumber.Hide();
                    if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:1, 1}}", NewNameTextBox.Text);
                }
            };
            StartNumber.ValueChanged += delegate (object? sender, EventArgs e)
            {
                if (NewNameTextBox.Text != string.Empty)
                {
                    if (Increment.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:{1}, {2}}}", NewNameTextBox.Text, StartNumber.Value, 1);
                    else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:1, 1}}", NewNameTextBox.Text);
                }
                if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
            };
            NewNameTextBox.TextChanged += delegate (object? sender, EventArgs e)
            {
                if (NewNameTextBox.Text != string.Empty)
                {
                    if (ExtensionTextBox.Text != string.Empty)
                    {
                        if (Increment.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:{1}, 1}}{{NewNameSet:\".{2}\"}}", NewNameTextBox.Text, StartNumber.Value, ExtensionTextBox.Text);
                        else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:1, 1}}{{NewNameSet:\"{1}\"}}", NewNameTextBox.Text, ExtensionTextBox.Text);
                    }
                    else
                    {
                        if (Increment.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:{1}, 1}}", NewNameTextBox.Text, StartNumber.Value);
                        else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:1, 1}}", NewNameTextBox.Text);
                    }
                }
                if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
            };
            ExtensionTextBox.TextChanged += delegate (object? sender, EventArgs e)
            {
                if (NewNameTextBox.Text != string.Empty)
                {
                    if (ExtensionTextBox.Text != string.Empty)
                    {
                        if (Increment.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:{1}, 1}}{{NewNameSet:\".{2}\"}}", NewNameTextBox.Text, StartNumber.Value, ExtensionTextBox.Text);
                        else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:1, 1}}{{NewNameSet:\"{1}\"}}", NewNameTextBox.Text, ExtensionTextBox.Text);
                    }
                    else
                    {
                        if (Increment.Checked) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:{1}, 1}}", NewNameTextBox.Text, StartNumber.Value);
                        else PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{Increment:1, 1}}", NewNameTextBox.Text);
                    }
                }
                if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
            };
            NewNameTextBox.Leave += Leave_RenameTextBox;
            NewNameTextBox.KeyPress += KeyPress_RenameTextBox;
            ExtensionTextBox.Leave += Leave_RenameTextBox;
            ExtensionTextBox.KeyPress += KeyPress_RenameTextBox;
            PatternTextBox.Text = string.Empty;
            DetailOptionPanel.Controls.Add(Increment);
            DetailOptionPanel.Controls.Add(StartNumberLabel);
            DetailOptionPanel.Controls.Add(StartNumber);
            DetailOptionPanel.Controls.Add(NewNameTextBox);
            DetailOptionPanel.Controls.Add(ExtensionTextBox);
            Increment.Checked = true;

            // 종료 이벤트 
            foreach (Control controls in DetailOptionPanel.Controls)
            {
                controls.KeyDown += KeyDown_RenameForm;
            }
        }
        #endregion

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
        private void Leave_RenameTextBox(object? sender, EventArgs e)
        {
            if (sender is TextBox RenameBox)
            {
                if (string.IsNullOrWhiteSpace(RenameBox.Text)) { RenameBox.Text = string.Empty; return; } // 모두 공백이거나 빈 문자열
                while (RenameBox.Text.Last() == '.') RenameBox.Text = RenameBox.Text.Remove(RenameBox.Text.Length - 1); // 마지막 '.' 삭제
                while (RenameBox.Text.First() == ' ') RenameBox.Text = RenameBox.Text.Remove(0, 1); // 처음 ' ' 삭제
                while (RenameBox.Text.Last() == ' ') RenameBox.Text = RenameBox.Text.Remove(RenameBox.Text.Length - 1); // 마직막 ' ' 삭제
            }
        }

        private void PatternTextBox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("text changed");
            RenamePattern renamePattern = new(PatternTextBox.Text);
            // 유효성 검사 1 : 이름 변경 표현식 검사
            string isRegular = string.Empty;




            char[] invalidChar = ['/', '?', '|', '\\', '<', '>', '\"', '*', ':'];
            Stack<char> bracketStack = new();
            if (PatternTextBox.Text[0] != '{')
            {
                ErrorTextBox.Text = "구문 오류!";
                return;
            }
            else ErrorTextBox.Text = "";
            foreach (char c in PatternTextBox.Text)
            {
                if (c == '{') bracketStack.Push(c);
                bracketStack.Peek();
            }
            if (invalidChar.Any(data => PatternTextBox.Text.Contains(data)))
            {
                ErrorTextBox.Text = "구문 오류!";
            }
            else
            {
                ErrorTextBox.Text = "정상";
            }
            // 파일명 유효성 검사 KeyCode
            // /,?, |, \, <, >, ", *, :

        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {

        }
    }
}
