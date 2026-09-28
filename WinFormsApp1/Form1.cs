using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // DB 연결 문자열 (Form2에서도 사용하므로 public)
        public string connectionString = "Server=localhost;Database=TestDB;Integrated Security=true;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();

            // 부서 콤보박스 항목 (디자이너에서 이미 넣었다면 건너뜀)
            if (comboBox1.Items.Count == 0)
                comboBox1.Items.AddRange(new object[] { "부산지점", "진주지점" });

            // 하단 표(StudentScore) 설정
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            // 상단 ListView(StudentInfo) 설정
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.MultiSelect = false;
            if (listView1.Columns.Count == 0)
            {
                listView1.Columns.Add("수험번호", 80);
                listView1.Columns.Add("부서", 80);
                listView1.Columns.Add("성명", 70);
                listView1.Columns.Add("주민등록번호", 100);
                listView1.Columns.Add("우편번호", 70);
                listView1.Columns.Add("주소", 300);
            }

            // 이벤트 연결 (디자이너에서 따로 연결하지 않아도 됨)
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            button6.Click += button6_Click;
        }

        // 데이터 로드 버튼
        private async void button2_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // 쿼리 2개를 한 번에 보내고 NextResultAsync로 두 번째 결과로 이동
        public async Task LoadDataAsync()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    await conn.OpenAsync();

                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        SELECT 수험번호, 부서, 성명, 주민등록번호, 우편번호, 주소 FROM StudentInfo;
                        SELECT 부서, 성명, 학력, 영어, 수학, 전형구분 FROM StudentScore;";

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // 첫 번째 결과: StudentInfo -> 상단 ListView
                        listView1.Items.Clear();
                        while (await reader.ReadAsync())
                        {
                            string[] values = new string[reader.FieldCount];
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                // NULL 체크
                                if (await reader.IsDBNullAsync(i))
                                    values[i] = "";
                                else
                                    values[i] = await reader.GetFieldValueAsync<string>(i);
                            }
                            listView1.Items.Add(new ListViewItem(values));
                        }

                        // 두 번째 결과: StudentScore -> 하단 표
                        if (await reader.NextResultAsync())
                        {
                            DataTable dtScore = new DataTable();
                            dtScore.Load(reader);
                            dataGridView1.DataSource = dtScore;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 오류: {ex.Message}");
            }
        }

        // 검색 버튼
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    await conn.OpenAsync();

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT 부서, 성명, 학력, 영어, 수학, 전형구분 FROM StudentScore WHERE 1=1";

                    if (!string.IsNullOrEmpty(comboBox1.Text))
                    {
                        cmd.CommandText += " AND 부서 = @Dept";
                        cmd.Parameters.AddWithValue("@Dept", comboBox1.Text);
                    }
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        cmd.CommandText += " AND 성명 LIKE @Name";
                        cmd.Parameters.AddWithValue("@Name", "%" + textBox1.Text + "%");
                    }

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검색 오류: {ex.Message}");
            }
        }

        // 삭제 버튼
        private async void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            string targetDept = dataGridView1.CurrentRow.Cells["부서"].Value?.ToString() ?? "";
            string targetName = dataGridView1.CurrentRow.Cells["성명"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(targetName)) return;

            DialogResult result = MessageBox.Show(
                $"{targetName} 님의 성적 데이터를 삭제할까요?", "삭제 확인", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    await conn.OpenAsync();

                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM StudentScore WHERE 부서 = @Dept AND 성명 = @Name";
                    cmd.Parameters.AddWithValue("@Dept", targetDept);
                    cmd.Parameters.AddWithValue("@Name", targetName);

                    int affectedRows = await cmd.ExecuteNonQueryAsync();
                }

                await LoadDataAsync(); // 삭제 후 표 갱신
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 오류: {ex.Message}");
            }
        }

        // 삽입 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 insertForm = new Form2(this, "INSERT");
            insertForm.ShowDialog();
        }

        // 수정 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                Form2 updateForm = new Form2(this, "UPDATE", selectedRow);
                updateForm.ShowDialog();
            }
        }

        // 종료 버튼
        private void button6_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        // 상단 ListView 항목 선택 -> 검색 조건(부서, 성명)에 채우기
        private void listView1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            ListViewItem item = listView1.SelectedItems[0];
            comboBox1.Text = item.SubItems[1].Text; // 부서
            textBox1.Text = item.SubItems[2].Text;  // 성명
        }
    }
}