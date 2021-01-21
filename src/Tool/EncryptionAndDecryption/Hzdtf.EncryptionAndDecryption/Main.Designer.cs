
namespace Hzdtf.EncryptionAndDecryption
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlaintext = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCiphertext = new System.Windows.Forms.TextBox();
            this.btnParseToPlaintext = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnCopyCiphertext = new System.Windows.Forms.Button();
            this.btnParseToCiphertext = new System.Windows.Forms.Button();
            this.btnDecryption = new System.Windows.Forms.Button();
            this.btnPlaintext = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlaintext);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "明文";
            // 
            // txtPlaintext
            // 
            this.txtPlaintext.Location = new System.Drawing.Point(6, 22);
            this.txtPlaintext.Multiline = true;
            this.txtPlaintext.Name = "txtPlaintext";
            this.txtPlaintext.Size = new System.Drawing.Size(764, 102);
            this.txtPlaintext.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCiphertext);
            this.groupBox2.Location = new System.Drawing.Point(12, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 143);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "密文";
            // 
            // txtCiphertext
            // 
            this.txtCiphertext.Location = new System.Drawing.Point(6, 22);
            this.txtCiphertext.Multiline = true;
            this.txtCiphertext.Name = "txtCiphertext";
            this.txtCiphertext.Size = new System.Drawing.Size(764, 102);
            this.txtCiphertext.TabIndex = 0;
            // 
            // btnParseToPlaintext
            // 
            this.btnParseToPlaintext.Location = new System.Drawing.Point(75, 178);
            this.btnParseToPlaintext.Name = "btnParseToPlaintext";
            this.btnParseToPlaintext.Size = new System.Drawing.Size(95, 38);
            this.btnParseToPlaintext.TabIndex = 2;
            this.btnParseToPlaintext.Text = "粘贴到明文";
            this.btnParseToPlaintext.UseVisualStyleBackColor = true;
            this.btnParseToPlaintext.Click += new System.EventHandler(this.btnParseToPlaintext_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(176, 178);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(95, 38);
            this.btnEncrypt.TabIndex = 3;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnCopyCiphertext
            // 
            this.btnCopyCiphertext.Location = new System.Drawing.Point(277, 178);
            this.btnCopyCiphertext.Name = "btnCopyCiphertext";
            this.btnCopyCiphertext.Size = new System.Drawing.Size(95, 38);
            this.btnCopyCiphertext.TabIndex = 4;
            this.btnCopyCiphertext.Text = "复制密文";
            this.btnCopyCiphertext.UseVisualStyleBackColor = true;
            this.btnCopyCiphertext.Click += new System.EventHandler(this.btnCopyCiphertext_Click);
            // 
            // btnParseToCiphertext
            // 
            this.btnParseToCiphertext.Location = new System.Drawing.Point(428, 178);
            this.btnParseToCiphertext.Name = "btnParseToCiphertext";
            this.btnParseToCiphertext.Size = new System.Drawing.Size(95, 38);
            this.btnParseToCiphertext.TabIndex = 2;
            this.btnParseToCiphertext.Text = "粘贴到密文";
            this.btnParseToCiphertext.UseVisualStyleBackColor = true;
            this.btnParseToCiphertext.Click += new System.EventHandler(this.btnParseToCiphertext_Click);
            // 
            // btnDecryption
            // 
            this.btnDecryption.Location = new System.Drawing.Point(529, 178);
            this.btnDecryption.Name = "btnDecryption";
            this.btnDecryption.Size = new System.Drawing.Size(95, 38);
            this.btnDecryption.TabIndex = 3;
            this.btnDecryption.Text = "解密";
            this.btnDecryption.UseVisualStyleBackColor = true;
            this.btnDecryption.Click += new System.EventHandler(this.btnDecryption_Click);
            // 
            // btnPlaintext
            // 
            this.btnPlaintext.Location = new System.Drawing.Point(630, 178);
            this.btnPlaintext.Name = "btnPlaintext";
            this.btnPlaintext.Size = new System.Drawing.Size(95, 38);
            this.btnPlaintext.TabIndex = 4;
            this.btnPlaintext.Text = "复制明文";
            this.btnPlaintext.UseVisualStyleBackColor = true;
            this.btnPlaintext.Click += new System.EventHandler(this.btnPlaintext_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 383);
            this.Controls.Add(this.btnPlaintext);
            this.Controls.Add(this.btnCopyCiphertext);
            this.Controls.Add(this.btnDecryption);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnParseToCiphertext);
            this.Controls.Add(this.btnParseToPlaintext);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Text = "加解密";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlaintext;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCiphertext;
        private System.Windows.Forms.Button btnParseToPlaintext;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnCopyCiphertext;
        private System.Windows.Forms.Button btnParseToCiphertext;
        private System.Windows.Forms.Button btnDecryption;
        private System.Windows.Forms.Button btnPlaintext;
    }
}

