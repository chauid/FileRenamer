namespace FileRenamer
{
    public delegate void ChangeDateTimeEventArgs(bool make, bool modify, bool access);
    public partial class ChangeDateTime : Form
    {
        public event ChangeDateTimeEventArgs? ApplyDateTime;

        public ChangeDateTime()
        {
            InitializeComponent();
            ApplyButton.Select();

            // 종료 이벤트
            foreach (Control controls in this.Controls)
            {
                controls.KeyDown += KeyDown_ChangeDateForm;
            }
        }
        private void ApplyButton_Click(object sender, EventArgs e) // 적용
        {
            ApplyDateTime?.Invoke(MakeDateCheckBox.Checked, ModifyDateCheckBox.Checked, AccessDateCheckBox.Checked);
            Close();
        }
        private void CloseButton_Click(object sender, EventArgs e) { Close(); } // 취소
        private void KeyDown_ChangeDateForm(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}
