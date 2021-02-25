using Hzdtf.Autofac.Extensions;
using Hzdtf.CodeGenerator.Contract;
using Hzdtf.CodeGenerator.Model;
using Hzdtf.Utility.Data;
using Hzdtf.Utility.Factory;
using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hzdtf.CodeGenerator
{
    /// <summary>
    /// 主界面
    /// @ 黄振东
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// 数据源类型字典
        /// </summary>
        private IDictionary<string, string> dataSourceTypes;

        /// <summary>
        /// 构造方法
        /// </summary>
        public Main()
        {
            InitializeComponent();

            dgvTable.AutoGenerateColumns = false;
            BindDataSourceType();

            var dataSourceConfigFile = $"{AppContext.BaseDirectory}/Config/defaultDataConfig.json";
            var dataConfig = dataSourceConfigFile.ToJsonObjectFromFile<DataSourceConfigInfo>();
           
            cbxDataSourceType.SelectedItem = dataConfig.DataSource;
            txtHost.Text = dataConfig.DataSource.Host;
            txtPort.Text = dataConfig.DataSource.Port.ToString();
            txtUser.Text = dataConfig.DataSource.User;
            txtPassword.Text = dataConfig.DataSource.Password;
            txtDb.Text = dataConfig.DataSource.Db;

            txtNameSpacePfx.Text = dataConfig.BuilderItem.Namespace;
            cbxPkType.SelectedItem = dataConfig.BuilderItem.PkType;
            cbxTenant.Checked = dataConfig.BuilderItem.IsTenant;
        }

        /// <summary>
        /// 绑定数据源类型
        /// </summary>
        private void BindDataSourceType()
        {
            var reader = AutofacTool.Resolve<IReader<IDictionary<string, string>>>();
            dataSourceTypes = reader.Reader();
            if (dataSourceTypes.IsNullOrCount0())
            {
                return;
            }

            foreach (KeyValuePair<string, string> item in dataSourceTypes)
            {
                cbxDataSourceType.Items.Add(item.Key);
            }

            cbxDataSourceType.SelectedIndex = 0;
        }

        /// <summary>
        /// 点击测试连接
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnTestConn_Click(object sender, EventArgs e)
        {
            try
            {
                var connStr = GetDbConnectionString();
                if (string.IsNullOrWhiteSpace(connStr))
                {
                    return;
                }
                var factory = AutofacTool.Resolve<ISimpleFactory<string, IDbConnection>>();
                var dbConnection = factory.Create(cbxDataSourceType.SelectedItem.ToString());

                dbConnection.ConnectionString = connStr;
                dbConnection.Open();
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();

                    MessageBox.Show("连接成功");
                }
                else
                {
                    MessageBox.Show("连接失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <returns>数据库连接字符串</returns>
        private string GetDbConnectionString(string dataBase = null)
        {
            if (!string.IsNullOrWhiteSpace(txtPort.Text))
            {
                int port;
                if (!int.TryParse(txtPort.Text, out port))
                {
                    MessageBox.Show("端口必须是整数");

                    return null;
                }
            }
            string portStr = null;
            if (!string.IsNullOrWhiteSpace(txtPort.Text))
            {
                portStr = txtPort.Text.Trim();
                if (cbxDataSourceType.SelectedIndex == 1)
                {
                    portStr = "," + portStr;
                }
            }

            return string.Format(dataSourceTypes[cbxDataSourceType.SelectedItem.ToString()],
                txtHost.Text,
                string.IsNullOrWhiteSpace(dataBase) ? txtDb.Text : dataBase,
                txtUser.Text,
                txtPassword.Text,
                portStr);
        }

        /// <summary>
        /// 点击加载
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                var connStr = GetDbConnectionString();
                if (string.IsNullOrWhiteSpace(connStr))
                {
                    return;
                }
                var dbInfoService = AutofacTool.Resolve<IDbInfoService>();
                var returnInfo = dbInfoService.Query(txtDb.Text, connStr, cbxDataSourceType.SelectedItem.ToString());
                if (returnInfo.Success())
                {
                    dgvTable.Tag = cbxDataSourceType.SelectedItem.ToString();
                    dgvTable.DataSource = returnInfo.Data;
                }
                else
                {
                    MessageBox.Show(returnInfo.Msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 点击生成
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void btnBuilder_Click(object sender, EventArgs e)
        {
            var param = new CodeGeneratorParamInfo();
            try
            {
                param.Tables = new List<TableInfo>(dgvTable.Rows.Count);
                foreach (DataGridViewRow row in dgvTable.Rows)
                {
                    DataGridViewCheckBoxCell checkCell = row.Cells[3] as DataGridViewCheckBoxCell;
                    if (checkCell.Value != null && Convert.ToBoolean(checkCell.Value))
                    {
                        var t = row.DataBoundItem as TableInfo;
                        param.Tables.Add(t);


                        DataGridViewCheckBoxCell pageCell = row.Cells[2] as DataGridViewCheckBoxCell;
                        if (pageCell.Value != null)
                        {
                            t.IsPage = Convert.ToBoolean(pageCell.Value);
                        }
                    }
                }

                if (param.Tables.Count == 0)
                {
                    MessageBox.Show("请勾选要生成的表");
                    return;
                }

                param.FunctionTypes = GetFunctionTypes();
                if (param.FunctionTypes.Length == 0)
                {
                    MessageBox.Show("请勾选要生成的功能项");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNameSpacePfx.Text))
                {
                    MessageBox.Show("命名空间前辍不能为空");
                    return;
                }
                param.NamespacePfx = txtNameSpacePfx.Text;

                switch (cbxPkType.SelectedItem.ToString())
                {
                    case "字符串":
                        param.PkType = PrimaryKeyType.STRING;

                        break;

                    case "Guid":
                        param.PkType = PrimaryKeyType.GUID;

                        break;

                    case "雪花算法":
                        param.PkType = PrimaryKeyType.SNOWFLAKE;

                        break;
                }
                param.Type = dgvTable.Tag.ToString();
                param.IsTenant = cbxTenant.Checked;

                Cursor.Current = Cursors.WaitCursor;
                ICodeGeneratorService generatorService = AutofacTool.Resolve<ICodeGeneratorService>();
                var returnInfo = generatorService.Generator(param);
                Cursor.Current = Cursors.Default;
                if (returnInfo.Success())
                {
                    MessageBox.Show("生成成功");
                }
                else
                {
                    MessageBox.Show(returnInfo.Msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取生成功能类型集合
        /// </summary>
        /// <returns>生成功能类型集合</returns>
        private FunctionType[] GetFunctionTypes()
        {
            IList<FunctionType> functionTypes = new List<FunctionType>();
            if (cbxModel.Checked)
            {
                functionTypes.Add(FunctionType.MODEL);
            }
            if (cbxPersistence.Checked)
            {
                functionTypes.Add(FunctionType.PERSISTENCE);
            }
            if (cbxService.Checked)
            {
                functionTypes.Add(FunctionType.SERVICE);
            }
            if (cbxController.Checked)
            {
                functionTypes.Add(FunctionType.CONTROLLER);
            }
            if (cbxRoutePermissionConfig.Checked)
            {
                functionTypes.Add(FunctionType.ROUTE_PERMISSION_CONFIG);
            }

            return functionTypes.ToArray();
        }

        /// <summary>
        /// 改变全选
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件参数</param>
        private void cbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTable.Rows)
            {
                var checkCell = row.Cells[3] as DataGridViewCheckBoxCell;
                checkCell.Value = cbxSelectAll.Checked;
            }
        }
    }
}
