namespace LotteryPrinter
{
    partial class QueryTitck
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryTitck));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SpValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyTitckID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Multiple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxBonus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTitck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TouZhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BiaoShi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TouZhuValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label_SpSum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label_countD = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Guan = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label_Munitlp = new System.Windows.Forms.Label();
            this.label_PlayID = new System.Windows.Forms.Label();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SpValue,
            this.BuyTitckID,
            this.Multiple,
            this.PlayType,
            this.MaxBonus,
            this.IsTitck,
            this.TouZhu,
            this.BiaoShi,
            this.CreateTime,
            this.UpTime,
            this.TouZhuValue});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(878, 385);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.Enter += new System.EventHandler(this.dataGridView1_Enter);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // SpValue
            // 
            this.SpValue.HeaderText = "SP状态升降平";
            this.SpValue.Name = "SpValue";
            this.SpValue.ReadOnly = true;
            // 
            // BuyTitckID
            // 
            this.BuyTitckID.HeaderText = "订单号";
            this.BuyTitckID.Name = "BuyTitckID";
            this.BuyTitckID.ReadOnly = true;
            // 
            // Multiple
            // 
            this.Multiple.HeaderText = "倍数";
            this.Multiple.Name = "Multiple";
            this.Multiple.ReadOnly = true;
            // 
            // PlayType
            // 
            this.PlayType.HeaderText = "玩法";
            this.PlayType.Name = "PlayType";
            this.PlayType.ReadOnly = true;
            // 
            // MaxBonus
            // 
            this.MaxBonus.HeaderText = "奖金预测";
            this.MaxBonus.Name = "MaxBonus";
            this.MaxBonus.ReadOnly = true;
            // 
            // IsTitck
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.IsTitck.DefaultCellStyle = dataGridViewCellStyle1;
            this.IsTitck.HeaderText = "是否已出票";
            this.IsTitck.Name = "IsTitck";
            this.IsTitck.ReadOnly = true;
            // 
            // TouZhu
            // 
            this.TouZhu.HeaderText = "投注格式";
            this.TouZhu.Name = "TouZhu";
            this.TouZhu.ReadOnly = true;
            // 
            // BiaoShi
            // 
            this.BiaoShi.HeaderText = "标识";
            this.BiaoShi.Name = "BiaoShi";
            this.BiaoShi.ReadOnly = true;
            this.BiaoShi.Visible = false;
            // 
            // CreateTime
            // 
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            // 
            // UpTime
            // 
            this.UpTime.HeaderText = "完成时间";
            this.UpTime.Name = "UpTime";
            this.UpTime.ReadOnly = true;
            // 
            // TouZhuValue
            // 
            this.TouZhuValue.HeaderText = "投注赔率";
            this.TouZhuValue.Name = "TouZhuValue";
            this.TouZhuValue.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(651, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "预测奖金:";
            // 
            // label_SpSum
            // 
            this.label_SpSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_SpSum.AutoSize = true;
            this.label_SpSum.BackColor = System.Drawing.Color.White;
            this.label_SpSum.Font = new System.Drawing.Font("华文新魏", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_SpSum.ForeColor = System.Drawing.Color.Red;
            this.label_SpSum.Location = new System.Drawing.Point(713, 21);
            this.label_SpSum.Name = "label_SpSum";
            this.label_SpSum.Size = new System.Drawing.Size(110, 35);
            this.label_SpSum.TabIndex = 2;
            this.label_SpSum.Text = "*****";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label_countD);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label_Guan);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label_Munitlp);
            this.groupBox1.Controls.Add(this.label_PlayID);
            this.groupBox1.Controls.Add(this.label_Type);
            this.groupBox1.Controls.Add(this.label_ID);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label_SpSum);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(877, 169);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本数据";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(859, 117);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "张";
            // 
            // label_countD
            // 
            this.label_countD.AutoSize = true;
            this.label_countD.BackColor = System.Drawing.Color.Transparent;
            this.label_countD.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_countD.ForeColor = System.Drawing.Color.Red;
            this.label_countD.Location = new System.Drawing.Point(808, 112);
            this.label_countD.Name = "label_countD";
            this.label_countD.Size = new System.Drawing.Size(29, 19);
            this.label_countD.TabIndex = 22;
            this.label_countD.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(718, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "当前页面剩余:";
            // 
            // label_Guan
            // 
            this.label_Guan.AutoSize = true;
            this.label_Guan.BackColor = System.Drawing.Color.Transparent;
            this.label_Guan.Location = new System.Drawing.Point(71, 142);
            this.label_Guan.Name = "label_Guan";
            this.label_Guan.Size = new System.Drawing.Size(23, 12);
            this.label_Guan.TabIndex = 20;
            this.label_Guan.Text = "---";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(7, 142);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "串关:";
            // 
            // label_Munitlp
            // 
            this.label_Munitlp.AutoSize = true;
            this.label_Munitlp.BackColor = System.Drawing.Color.Transparent;
            this.label_Munitlp.Location = new System.Drawing.Point(71, 114);
            this.label_Munitlp.Name = "label_Munitlp";
            this.label_Munitlp.Size = new System.Drawing.Size(23, 12);
            this.label_Munitlp.TabIndex = 18;
            this.label_Munitlp.Text = "---";
            // 
            // label_PlayID
            // 
            this.label_PlayID.AutoSize = true;
            this.label_PlayID.BackColor = System.Drawing.Color.Transparent;
            this.label_PlayID.Location = new System.Drawing.Point(71, 86);
            this.label_PlayID.Name = "label_PlayID";
            this.label_PlayID.Size = new System.Drawing.Size(23, 12);
            this.label_PlayID.TabIndex = 17;
            this.label_PlayID.Text = "---";
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.BackColor = System.Drawing.Color.Transparent;
            this.label_Type.Location = new System.Drawing.Point(71, 58);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(23, 12);
            this.label_Type.TabIndex = 16;
            this.label_Type.Text = "---";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.BackColor = System.Drawing.Color.Transparent;
            this.label_ID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ID.Location = new System.Drawing.Point(71, 30);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(31, 14);
            this.label_ID.TabIndex = 15;
            this.label_ID.Text = "---";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(7, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "倍数:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(7, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "玩法:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(7, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "彩票类型:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(641, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "还原默认";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(705, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "过期票:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(757, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "再打一波";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(705, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "已出票:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(7, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "订单号:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(755, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "▄▄▄▄▄▄▄▄▄";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(707, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(164, 43);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(755, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "▄▄▄▄▄▄▄▄▄";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(-1, 175);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(877, 385);
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            // 
            // QueryTitck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(902, 584);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryTitck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查票窗口";
            this.Load += new System.EventHandler(this.QueryTitck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_SpSum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_Guan;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_Munitlp;
        private System.Windows.Forms.Label label_PlayID;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyTitckID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Multiple;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxBonus;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTitck;
        private System.Windows.Forms.DataGridViewTextBoxColumn TouZhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn BiaoShi;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TouZhuValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_countD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}