namespace FileRenamer
{
    partial class ChangeDateTime
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
            MakeDateCheckBox = new MaterialSkin.Controls.MaterialCheckbox();
            ModifyDateCheckBox = new MaterialSkin.Controls.MaterialCheckbox();
            AccessDateCheckBox = new MaterialSkin.Controls.MaterialCheckbox();
            ApplyButton = new Button();
            CloseButton = new Button();
            SuspendLayout();
            // 
            // MakeDateCheckBox
            // 
            MakeDateCheckBox.AutoSize = true;
            MakeDateCheckBox.Depth = 0;
            MakeDateCheckBox.Location = new Point(50, 15);
            MakeDateCheckBox.Margin = new Padding(0);
            MakeDateCheckBox.MouseLocation = new Point(-1, -1);
            MakeDateCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            MakeDateCheckBox.Name = "MakeDateCheckBox";
            MakeDateCheckBox.ReadOnly = false;
            MakeDateCheckBox.Ripple = true;
            MakeDateCheckBox.Size = new Size(87, 37);
            MakeDateCheckBox.TabIndex = 0;
            MakeDateCheckBox.Text = "만든 날짜";
            MakeDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // ModifyDateCheckBox
            // 
            ModifyDateCheckBox.AutoSize = true;
            ModifyDateCheckBox.Depth = 0;
            ModifyDateCheckBox.Location = new Point(50, 55);
            ModifyDateCheckBox.Margin = new Padding(0);
            ModifyDateCheckBox.MouseLocation = new Point(-1, -1);
            ModifyDateCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            ModifyDateCheckBox.Name = "ModifyDateCheckBox";
            ModifyDateCheckBox.ReadOnly = false;
            ModifyDateCheckBox.Ripple = true;
            ModifyDateCheckBox.Size = new Size(99, 37);
            ModifyDateCheckBox.TabIndex = 1;
            ModifyDateCheckBox.Text = "수정한 날짜";
            ModifyDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // AccessDateCheckBox
            // 
            AccessDateCheckBox.AutoSize = true;
            AccessDateCheckBox.Depth = 0;
            AccessDateCheckBox.Location = new Point(50, 95);
            AccessDateCheckBox.Margin = new Padding(0);
            AccessDateCheckBox.MouseLocation = new Point(-1, -1);
            AccessDateCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            AccessDateCheckBox.Name = "AccessDateCheckBox";
            AccessDateCheckBox.ReadOnly = false;
            AccessDateCheckBox.Ripple = true;
            AccessDateCheckBox.Size = new Size(111, 37);
            AccessDateCheckBox.TabIndex = 2;
            AccessDateCheckBox.Text = "액세스한 날짜";
            AccessDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // ApplyButton
            // 
            ApplyButton.BackColor = Color.LightGreen;
            ApplyButton.FlatAppearance.BorderSize = 0;
            ApplyButton.FlatAppearance.MouseDownBackColor = Color.Thistle;
            ApplyButton.FlatAppearance.MouseOverBackColor = Color.LawnGreen;
            ApplyButton.FlatStyle = FlatStyle.Flat;
            ApplyButton.Location = new Point(50, 155);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(80, 25);
            ApplyButton.TabIndex = 3;
            ApplyButton.Text = "변경";
            ApplyButton.UseVisualStyleBackColor = false;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.Goldenrod;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatAppearance.MouseDownBackColor = Color.Thistle;
            CloseButton.FlatAppearance.MouseOverBackColor = Color.Salmon;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Location = new Point(135, 155);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(80, 25);
            CloseButton.TabIndex = 4;
            CloseButton.Text = "취소";
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // ChangeDateTime
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(220, 190);
            Controls.Add(CloseButton);
            Controls.Add(ApplyButton);
            Controls.Add(AccessDateCheckBox);
            Controls.Add(ModifyDateCheckBox);
            Controls.Add(MakeDateCheckBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangeDateTime";
            Text = "날짜 변경 선택";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialCheckbox MakeDateCheckBox;
        private MaterialSkin.Controls.MaterialCheckbox ModifyDateCheckBox;
        private MaterialSkin.Controls.MaterialCheckbox AccessDateCheckBox;
        private Button ApplyButton;
        private Button CloseButton;
    }
}