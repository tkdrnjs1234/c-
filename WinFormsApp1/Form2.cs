using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private Form1 parentForm;
        private string mode;
        private DataGridViewRow? row;

        // UPDATE 할 때 원래 값 (WHERE 조건용)
        private string oldDept = "";
        private string oldName = "";

        public Form2(Form1 parent, string mode, DataGridViewRow? row = null)
        {
            InitializeComponent();
            this.parentForm = parent;
            this.mode = mode;
            this.row = row;

            // 콤보박스 항목 (디자이너에서 이미 넣었다면 건너뜀)
            if (cmbDept.Items.Count == 0)
                cmbDept.Items.AddRange(new object[] { "부산지점", "진주지점" });
            if (cmbType.Items.Count == 0)
                cmbType.Items.AddRange(new object[] { "공체", "특별" });

            // 이벤트 연결 (디자이너에서 따로 연결하지 않아도 됨)
            btnSave.Click += btnSave_Click;
            btnReset.Click += btnReset_Click;
            btnClose.Click += btnClose_Click;

            if (mode == "UPDATE" && row != null)
            {
                btnSave.Text = "변경";
                oldDept = row.Cells["부서"].Value?.ToString() ?? "";
                oldName = row.Cells["성명"].Value?.ToString() ?? "";
            }

            ResetFields();
        }

        // 입력칸 초기화 (UPDATE는 원래 값으로 복원, INSERT는 비우기)
        private void ResetFields()
        {
            if (mode == "UPDATE" && row != null)
            {
                cmbDept.Text = row.Cells["부서"].Value?.ToString() ?? "";
                txtName.Text = row.Cells["성명"].Value?.ToString() ?? "";
                txtEdu.Text = row.Cells["학력"].Value?.ToString() ?? "";
                txtEng.Text = row.Cells["영어"].Value?.ToString() ?? "";
                txtMath.Text = row.Cells["수학"].Value?.ToString() ?? "";
                cmbType.Text = row.Cells["전형구분"].Value?.ToString() ?? "";
            }
            else
            {
                cmbDept.SelectedIndex = -1;
                txtName.Clear();
                txtEdu.Clear();
                txtEng.Clear();
                txtMath.Clear();
                cmbType.SelectedIndex = -1;
            }
        }

        // 저장(삽입) / 변경(수정) 버튼
        private async void btnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbDept.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("부서와 성명을 입력하세요.");
                return;
            }
            if (!int.TryParse(txtEng.Text, out int eng) || !int.TryParse(txtMath.Text, out int math))
            {
                MessageBox.Show("영어/수학 점수는 숫자로 입력하세요.");
                return;
            }

            try
            {
                int affectedRows = 0;

                using (SqlConnection conn = new SqlConnection(parentForm.connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    await conn.OpenAsync();
                    cmd.Connection = conn;

                    if (mode == "INSERT")
                    {
                        cmd.CommandText = @"INSERT INTO StudentScore (부서, 성명, 학력, 영어, 수학, 전형구분)
                                            VALUES (@Dept, @Name, @Edu, @Eng, @Math, @Type)";
                    }
                    else
                    {
                        cmd.CommandText = @"UPDATE StudentScore
                                            SET 부서 = @Dept, 성명 = @Name, 학력 = @Edu,
                                                영어 = @Eng, 수학 = @Math, 전형구분 = @Type
                                            WHERE 부서 = @OldDept AND 성명 = @OldName";
                        cmd.Parameters.AddWithValue("@OldDept", oldDept);
                        cmd.Parameters.AddWithValue("@OldName", oldName);
                    }

                    cmd.Parameters.AddWithValue("@Dept", cmbDept.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Edu", txtEdu.Text);
                    cmd.Parameters.AddWithValue("@Eng", eng);
                    cmd.Parameters.AddWithValue("@Math", math);
                    cmd.Parameters.AddWithValue("@Type", cmbType.Text);

                    affectedRows = await cmd.ExecuteNonQueryAsync(); // 비동기 CUD 실행
                }

                if (affectedRows == 0)
                {
                    MessageBox.Show("처리된 데이터가 없습니다.");
                    return;
                }

                await parentForm.LoadDataAsync(); // Form1 표 갱신
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 오류: {ex.Message}");
            }
        }

        // 초기화 버튼
        private void btnReset_Click(object? sender, EventArgs e)
        {
            ResetFields();
        }

        // 종료 버튼
        private void btnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        // 디자이너에 연결돼 있어서 삭제하면 오류가 나므로 그대로 둠 (사용 안 함)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}