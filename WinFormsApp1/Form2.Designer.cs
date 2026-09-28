namespace WinFormsApp1
{
    partial class Form2
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            cmbDept = new ComboBox();
            cmbType = new ComboBox();
            txtName = new TextBox();
            txtEdu = new TextBox();
            txtEng = new TextBox();
            txtMath = new TextBox();
            btnSave = new Button();
            btnReset = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(13, 33);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "부     서 :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Location = new Point(12, 94);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 1;
            label2.Text = "성     명 :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(12, 157);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 2;
            label3.Text = "학     력 :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label4.Location = new Point(12, 217);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 3;
            label4.Text = "영    어 :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label5.Location = new Point(12, 271);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 4;
            label5.Text = "수    학 :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.Location = new Point(9, 324);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 5;
            label6.Text = "전형구분 :";
            // 
            // cmbDept
            // 
            cmbDept.FormattingEnabled = true;
            cmbDept.Location = new Point(82, 30);
            cmbDept.Name = "cmbDept";
            cmbDept.Size = new Size(263, 23);
            cmbDept.TabIndex = 6;
            // 
            // cmbType
            // 
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(83, 320);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(264, 23);
            cmbType.TabIndex = 7;
            // 
            // txtName
            // 
            txtName.Location = new Point(82, 90);
            txtName.Name = "txtName";
            txtName.Size = new Size(263, 23);
            txtName.TabIndex = 8;
            txtName.TextChanged += textBox1_TextChanged;
            // 
            // txtEdu
            // 
            txtEdu.Location = new Point(83, 154);
            txtEdu.Name = "txtEdu";
            txtEdu.Size = new Size(263, 23);
            txtEdu.TabIndex = 9;
            // 
            // txtEng
            // 
            txtEng.Location = new Point(83, 213);
            txtEng.Name = "txtEng";
            txtEng.Size = new Size(263, 23);
            txtEng.TabIndex = 10;
            // 
            // txtMath
            // 
            txtMath.Location = new Point(83, 266);
            txtMath.Name = "txtMath";
            txtMath.Size = new Size(263, 23);
            txtMath.TabIndex = 11;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(24, 383);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(82, 29);
            btnSave.TabIndex = 12;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(144, 383);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(82, 29);
            btnReset.TabIndex = 13;
            btnReset.Text = "초기화";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(263, 383);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(82, 29);
            btnClose.TabIndex = 14;
            btnClose.Text = "종료";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(368, 450);
            Controls.Add(btnClose);
            Controls.Add(btnReset);
            Controls.Add(btnSave);
            Controls.Add(txtMath);
            Controls.Add(txtEng);
            Controls.Add(txtEdu);
            Controls.Add(txtName);
            Controls.Add(cmbType);
            Controls.Add(cmbDept);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox cmbDept;
        private ComboBox cmbType;
        private TextBox txtName;
        private TextBox txtEdu;
        private TextBox txtEng;
        private TextBox txtMath;
        private Button btnSave;
        private Button btnReset;
        private Button btnClose;
    }
}