
namespace Hzdtf.CodeGenerator
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxDataSourceType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSelectAll = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBuilder = new System.Windows.Forms.Button();
            this.cbxRoutePermissionConfig = new System.Windows.Forms.CheckBox();
            this.cbxController = new System.Windows.Forms.CheckBox();
            this.cbxService = new System.Windows.Forms.CheckBox();
            this.cbxPersistence = new System.Windows.Forms.CheckBox();
            this.cbxModel = new System.Windows.Forms.CheckBox();
            this.cbxTenant = new System.Windows.Forms.CheckBox();
            this.cbxPkType = new System.Windows.Forms.ComboBox();
            this.txtNameSpacePfx = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Paging = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Selcct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "主机：";
            // 
            // cbxDataSourceType
            // 
            this.cbxDataSourceType.FormattingEnabled = true;
            this.cbxDataSourceType.Location = new System.Drawing.Point(68, 32);
            this.cbxDataSourceType.Name = "cbxDataSourceType";
            this.cbxDataSourceType.Size = new System.Drawing.Size(121, 25);
            this.cbxDataSourceType.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(459, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "端口：";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(270, 32);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(165, 23);
            this.txtHost.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestConn);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtDb);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtHost);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxDataSourceType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据源";
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(749, 70);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(91, 26);
            this.btnTestConn.TabIndex = 6;
            this.btnTestConn.Text = "测试连接";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(509, 72);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(160, 23);
            this.txtPassword.TabIndex = 5;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(509, 32);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(87, 23);
            this.txtPort.TabIndex = 2;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(270, 72);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(165, 23);
            this.txtUser.TabIndex = 4;
            // 
            // txtDb
            // 
            this.txtDb.Location = new System.Drawing.Point(675, 32);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(165, 23);
            this.txtDb.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(459, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "用户：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(613, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "数据库：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "主机：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxSelectAll);
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.dgvTable);
            this.groupBox2.Location = new System.Drawing.Point(12, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(879, 471);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表";
            // 
            // cbxSelectAll
            // 
            this.cbxSelectAll.AutoSize = true;
            this.cbxSelectAll.Location = new System.Drawing.Point(803, 75);
            this.cbxSelectAll.Name = "cbxSelectAll";
            this.cbxSelectAll.Size = new System.Drawing.Size(51, 21);
            this.cbxSelectAll.TabIndex = 2;
            this.cbxSelectAll.Text = "全选";
            this.cbxSelectAll.UseVisualStyleBackColor = true;
            this.cbxSelectAll.CheckedChanged += new System.EventHandler(this.cbxSelectAll_CheckedChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(788, 31);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "加载";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgvTable
            // 
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column,
            this.Desc,
            this.Paging,
            this.Selcct});
            this.dgvTable.Location = new System.Drawing.Point(18, 31);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.RowTemplate.Height = 25;
            this.dgvTable.Size = new System.Drawing.Size(746, 417);
            this.dgvTable.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBuilder);
            this.groupBox3.Controls.Add(this.cbxRoutePermissionConfig);
            this.groupBox3.Controls.Add(this.cbxController);
            this.groupBox3.Controls.Add(this.cbxService);
            this.groupBox3.Controls.Add(this.cbxPersistence);
            this.groupBox3.Controls.Add(this.cbxModel);
            this.groupBox3.Controls.Add(this.cbxTenant);
            this.groupBox3.Controls.Add(this.cbxPkType);
            this.groupBox3.Controls.Add(this.txtNameSpacePfx);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(12, 636);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(879, 146);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成项";
            // 
            // btnBuilder
            // 
            this.btnBuilder.Location = new System.Drawing.Point(424, 66);
            this.btnBuilder.Name = "btnBuilder";
            this.btnBuilder.Size = new System.Drawing.Size(108, 38);
            this.btnBuilder.TabIndex = 5;
            this.btnBuilder.Text = "生成";
            this.btnBuilder.UseVisualStyleBackColor = true;
            this.btnBuilder.Click += new System.EventHandler(this.btnBuilder_Click);
            // 
            // cbxRoutePermissionConfig
            // 
            this.cbxRoutePermissionConfig.AutoSize = true;
            this.cbxRoutePermissionConfig.Checked = true;
            this.cbxRoutePermissionConfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxRoutePermissionConfig.Location = new System.Drawing.Point(288, 76);
            this.cbxRoutePermissionConfig.Name = "cbxRoutePermissionConfig";
            this.cbxRoutePermissionConfig.Size = new System.Drawing.Size(99, 21);
            this.cbxRoutePermissionConfig.TabIndex = 4;
            this.cbxRoutePermissionConfig.Text = "路由权限配置";
            this.cbxRoutePermissionConfig.UseVisualStyleBackColor = true;
            // 
            // cbxController
            // 
            this.cbxController.AutoSize = true;
            this.cbxController.Checked = true;
            this.cbxController.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxController.Location = new System.Drawing.Point(215, 76);
            this.cbxController.Name = "cbxController";
            this.cbxController.Size = new System.Drawing.Size(63, 21);
            this.cbxController.TabIndex = 4;
            this.cbxController.Text = "控制器";
            this.cbxController.UseVisualStyleBackColor = true;
            // 
            // cbxService
            // 
            this.cbxService.AutoSize = true;
            this.cbxService.Checked = true;
            this.cbxService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxService.Location = new System.Drawing.Point(152, 76);
            this.cbxService.Name = "cbxService";
            this.cbxService.Size = new System.Drawing.Size(51, 21);
            this.cbxService.TabIndex = 4;
            this.cbxService.Text = "服务";
            this.cbxService.UseVisualStyleBackColor = true;
            // 
            // cbxPersistence
            // 
            this.cbxPersistence.AutoSize = true;
            this.cbxPersistence.Checked = true;
            this.cbxPersistence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPersistence.Location = new System.Drawing.Point(78, 76);
            this.cbxPersistence.Name = "cbxPersistence";
            this.cbxPersistence.Size = new System.Drawing.Size(63, 21);
            this.cbxPersistence.TabIndex = 4;
            this.cbxPersistence.Text = "持久化";
            this.cbxPersistence.UseVisualStyleBackColor = true;
            // 
            // cbxModel
            // 
            this.cbxModel.AutoSize = true;
            this.cbxModel.Checked = true;
            this.cbxModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxModel.Location = new System.Drawing.Point(21, 76);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.Size = new System.Drawing.Size(51, 21);
            this.cbxModel.TabIndex = 4;
            this.cbxModel.Text = "模型";
            this.cbxModel.UseVisualStyleBackColor = true;
            // 
            // cbxTenant
            // 
            this.cbxTenant.AutoSize = true;
            this.cbxTenant.Location = new System.Drawing.Point(769, 34);
            this.cbxTenant.Name = "cbxTenant";
            this.cbxTenant.Size = new System.Drawing.Size(75, 21);
            this.cbxTenant.TabIndex = 23;
            this.cbxTenant.Text = "是否租户";
            this.cbxTenant.UseVisualStyleBackColor = true;
            // 
            // cbxPkType
            // 
            this.cbxPkType.FormattingEnabled = true;
            this.cbxPkType.Items.AddRange(new object[] {
            "整型",
            "Guid",
            "字符串",
            "雪花算法"});
            this.cbxPkType.Location = new System.Drawing.Point(602, 30);
            this.cbxPkType.Name = "cbxPkType";
            this.cbxPkType.Size = new System.Drawing.Size(121, 25);
            this.cbxPkType.TabIndex = 21;
            // 
            // txtNameSpacePfx
            // 
            this.txtNameSpacePfx.Location = new System.Drawing.Point(116, 32);
            this.txtNameSpacePfx.Name = "txtNameSpacePfx";
            this.txtNameSpacePfx.Size = new System.Drawing.Size(370, 23);
            this.txtNameSpacePfx.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(528, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "主键类型：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "命名空间前辍：";
            // 
            // Column
            // 
            this.Column.DataPropertyName = "Name";
            this.Column.HeaderText = "列名";
            this.Column.Name = "Column";
            this.Column.ReadOnly = true;
            this.Column.Width = 150;
            // 
            // Desc
            // 
            this.Desc.DataPropertyName = "Description";
            this.Desc.HeaderText = "描述";
            this.Desc.Name = "Desc";
            this.Desc.Width = 400;
            // 
            // Paging
            // 
            this.Paging.DataPropertyName = "IsPage";
            this.Paging.HeaderText = "分页";
            this.Paging.Name = "Paging";
            this.Paging.Width = 50;
            // 
            // Selcct
            // 
            this.Selcct.HeaderText = "选择";
            this.Selcct.Name = "Selcct";
            this.Selcct.Width = 50;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 807);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "代码生成器(.Net)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxDataSourceType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxSelectAll;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbxTenant;
        private System.Windows.Forms.ComboBox cbxPkType;
        private System.Windows.Forms.TextBox txtNameSpacePfx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBuilder;
        private System.Windows.Forms.CheckBox cbxRoutePermissionConfig;
        private System.Windows.Forms.CheckBox cbxController;
        private System.Windows.Forms.CheckBox cbxService;
        private System.Windows.Forms.CheckBox cbxPersistence;
        private System.Windows.Forms.CheckBox cbxModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Paging;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selcct;
    }
}

