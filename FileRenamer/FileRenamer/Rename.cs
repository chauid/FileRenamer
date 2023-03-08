using MaterialSkin.Controls;

namespace FileManager
{
    /// <summary>
    /// 이름 바꾸기 이벤트 핸들러 
    /// </summary>
    /// <param name="Regular">이름 변경 표현식</param>
    public delegate void RenameEventArgs(string? Regular);

    public partial class Rename : Form
    {
        private bool AdvancedOption = false;
        private string? Regular;
        public event RenameEventArgs? ExcuteRename;

        /// <summary>
        /// 이름 정규식 정의 : {추가, 삭제, 대체}.
        /// 정규식 구분자 : "{}"
        /// </summary>
        public enum RegularType
        {
            /// <summary>
            /// 문자열 추가. 정규식 활용:{Append:"추가할 문자열", 추가할 위치(정수값)}
            /// </summary>
            Append = 0,

            /// <summary>
            /// 문자열 삭제. 정규식 활용:{Delete:[FileName] or string, 삭제할 범위(정수값), 삭제할 위치(정수값)}
            /// </summary>
            Delete = 1,

            /// <summary>
            /// 문자열 대체. 정규식 활용:{Replace:[FileName] or string, "찾을 문자열", "대체할 문자열"}
            /// </summary>
            Replace = 2,
        }
        #region Rename
        public Rename()
        {
            InitializeComponent();
            DetailOptionPanel.Select();
            AppendRadioButton.Checked = true;
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
                        if (AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{{0}:\"{1}\"}}{{FileName}}", RegularType.Append, AppendTextBox.Text);
                    }
                };
                AppendAfterRadioButton.CheckedChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendAfterRadioButton.Checked)
                    {
                        AppendTextBox.Hint = "파일명 뒤에 추가할 문자열 입력";
                        if (AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName}}{{{0}:\"{1}\"}}", RegularType.Append, AppendTextBox.Text);
                    }
                };
                AppendTextBox.TextChanged += delegate (object? sender, EventArgs e)
                {
                    if (AppendBeforeRadioButton.Checked && AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{{0}:\"{1}\"}}{{FileName}}", RegularType.Append, AppendTextBox.Text);
                    if (AppendAfterRadioButton.Checked && AppendTextBox.Text != string.Empty) PatternTextBox.Text = string.Format("{{FileName}}{{{0}:\"{1}\"}}", RegularType.Append, AppendTextBox.Text);
                    if (AppendTextBox.Text == string.Empty) PatternTextBox.Text = "{FileName}";
                };
                DetailOptionPanel.Controls.Add(AppendBeforeRadioButton);
                DetailOptionPanel.Controls.Add(AppendAfterRadioButton);
                DetailOptionPanel.Controls.Add(AppendTextBox);
                AppendBeforeRadioButton.Checked = true;
                AppendTextBox.Focus();
                AppendTextBox.Select();
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
                    PatternTextBox.Text = string.Format("{{Delete:File}}");
                };
                DetailOptionPanel.Controls.Add(DeleteBeforeRadioButton);
                DetailOptionPanel.Controls.Add(DeleteAfterRadioButton);
                DetailOptionPanel.Controls.Add(DeleteNumeric);
                DeleteBeforeRadioButton.Checked = true;
            }
        }
        private void ReplaceRadioButton_CheckedChanged(object sender, EventArgs e) // Replace 
        {
            if (ReplaceRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
            }
        }
        private void NewPatternRadioButton_CheckedChanged(object sender, EventArgs e) // NewPattern 
        {
            if (NewPatternRadioButton.Checked)
            {
                DetailOptionPanel.Controls.Clear();
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
    }
}
