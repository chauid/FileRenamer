using MaterialSkin.Controls;

namespace FileManager
{
    /// <summary>
    /// 이름 바꾸기 이벤트 핸들러 
    /// </summary>
    /// <param name="Regular">이름 변경 표현식</param>
    public delegate void RenameEventArgs(string? Regular);

    /// <summary>
    /// 이름 정규식 정의 : {추가, 삭제, 대체}.
    /// 정규식 구분자 : "{}"
    /// </summary>
    public enum RegularType
    {
        /// <summary>
        /// 문자열 추가. 정규식 활용:{[FileName] or string:Append, "추가할 문자열", 추가할 위치(int), 인덱싱순서(Boolean)}
        /// </summary>
        Append = 0,

        /// <summary>
        /// 문자열 삭제. 정규식 활용:{[FileName] or string:Delete, 삭제할 범위(int), 삭제할 위치(int), 인덱싱순서(Boolean)}
        /// </summary>
        Delete = 1,

        /// <summary>
        /// 문자열 대체. 정규식 활용:{[FileName] or string:Replace, "찾을 문자열", "대체할 문자열"}
        /// </summary>
        Replace = 2,

        /// <summary>
        /// 새로운 문자열 추가. 정규식 활용 : {NewNameSet, "새로운 문자열"}
        /// </summary>
        NewNameSet = 3,

        /// <summary>
        /// 숫자 자동 증가. 정규식 활용 : {AutoIncrement:시작값(int), 증분값(int)}
        /// </summary>
        AutoIncrement = 4
    }
    public partial class Rename : Form
    {
        private bool AdvancedOption = false;
        private string? Regular;
        public event RenameEventArgs? ExcuteRename;

        #region Rename
        public Rename()
        {
            InitializeComponent();
            DetailOptionPanel.Select();
            AppendRadioButton.Checked = true;

            // 종료 이벤트 
            foreach (Control controls in this.Controls)
            {
                controls.KeyDown += KeyDown_RenameForm;
            }
        }
        #endregion

        private void ChangeButton_Click(object sender, EventArgs e) // 변경 
        {
            ExcuteRename?.Invoke(Regular);
            Close();
        }
        private void CloseButton_Click(object sender, EventArgs e) { Close(); } // 취소 
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
                        if (AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName:Append, \"{0}\", {1}, {2}}}", AppendTextBox.Text, 0, true);
                    }
                };
                AppendAfterRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendAfterRadioButton.Checked)
                    {
                        AppendTextBox.Hint = "파일명 뒤에 추가할 문자열 입력";
                        if (AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName:Append, \"{0}\", {1}, {2}}}", AppendTextBox.Text, 0, false);
                    }
                };
                AppendTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendBeforeRadioButton.Checked && AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName:Append, \"{0}\", {1}, {2}}}", AppendTextBox.Text, 0, true);
                    if (AppendAfterRadioButton.Checked && AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName:Append, \"{0}\", {1}, {2}}}", AppendTextBox.Text, 0, false);
                    if (AppendTextBox.Text == string.Empty) PatternTextBox.Text = "{FileName}";
                };
                PatternTextBox.Text = "{FileName}";
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
                NumericUpDown DeleteNumeric = new()
                {
                    Location = new Point(120, 60),
                    Maximum = 65535,
                    Minimum = 1,
                    Value = 1,
                    ThousandsSeparator = true,
                    AutoSize = true
                };
                DeleteBeforeRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    PatternTextBox.Text = string.Format("{{FileName:Delete, {0}, {1}, {2}}}", DeleteNumeric.Value, 0, true);
                };
                DeleteAfterRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    PatternTextBox.Text = string.Format("{{FileName:Delete, {0}, {1}, {2}}}", DeleteNumeric.Value, 0, false);
                };
                DeleteNumeric.ValueChanged += delegate (object? sender, EventArgs e)
                {
                    if (DeleteBeforeRadioButton.Checked) PatternTextBox.Text = string.Format("{{FileName:Delete, {0}, {1}, {2}}}", DeleteNumeric.Value, 0, true);
                    if (DeleteAfterRadioButton.Checked) PatternTextBox.Text = string.Format("{{FileName:Delete, {0}, {1}, {2}}}", DeleteNumeric.Value, 0, false);
                };
                DetailOptionPanel.Controls.Add(DeleteBeforeRadioButton);
                DetailOptionPanel.Controls.Add(DeleteAfterRadioButton);
                DetailOptionPanel.Controls.Add(DeleteNumeric);
                DeleteBeforeRadioButton.Checked = true;

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }
        private void ReplaceRadioButton_CheckedChanged(object sender, EventArgs e) // Replace 
        {
            if (ReplaceRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
                MaterialTextBox2 SearchTextBox = new()
                {
                    Location = new Point(55, 10),
                    Hint = "찾은 문자열",
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
                    if (SearchTextBox.Text != string.Empty && ReplaceTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName:Replace, \"{0}\", \"{1}\"}}", SearchTextBox.Text, ReplaceTextBox.Text);
                    if (SearchTextBox.Text == string.Empty || ReplaceTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                PatternTextBox.Text = "{FileName}";
                DetailOptionPanel.Controls.Add(SearchTextBox);
                DetailOptionPanel.Controls.Add(ReplaceTextBox);

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }
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
                    Location = new Point(170, 10),
                    Text = "시작 숫자 :",
                    AutoSize = true
                };
                NumericUpDown StartNumber = new()
                {
                    Location = new Point(240, 8),
                    Maximum = 65535,
                    Minimum = 1,
                    Value = 1,
                    ThousandsSeparator = true,
                    AutoSize = true
                };
                MaterialTextBox2 NewNameTextBox = new()
                {
                    Location = new Point(55, 60),
                    Hint = "새로운 파일명 입력",
                    AutoSize = true
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
                        if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, {2}}}", NewNameTextBox.Text, 1, 1);
                    }
                };
                StartNumber.ValueChanged += delegate (object? sender, EventArgs e)
                {
                    if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, {2}}}", NewNameTextBox.Text, StartNumber.Value, 1);
                    if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                NewNameTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (NewNameTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{NewNameSet:\"{0}\"}}{{AutoIncrement:{1}, {2}}}", NewNameTextBox.Text, StartNumber.Value, 1);
                    if (NewNameTextBox.Text == string.Empty) PatternTextBox.Text = string.Empty;
                };
                PatternTextBox.Text = string.Empty;
                DetailOptionPanel.Controls.Add(AutoIncrement);
                DetailOptionPanel.Controls.Add(StartNumberLabel);
                DetailOptionPanel.Controls.Add(StartNumber);
                DetailOptionPanel.Controls.Add(NewNameTextBox);
                AutoIncrement.Checked = true;

                // 종료 이벤트 
                foreach (Control controls in DetailOptionPanel.Controls)
                {
                    controls.KeyDown += KeyDown_RenameForm;
                }
            }
        }

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

        private void PatternTextBox_TextChanged(object sender, EventArgs e) // Regular Update 
        {
            Regular = PatternTextBox.Text;
        }
        private void KeyDown_RenameForm(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}
