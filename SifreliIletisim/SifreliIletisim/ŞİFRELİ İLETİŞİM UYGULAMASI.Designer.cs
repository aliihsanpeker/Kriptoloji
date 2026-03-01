namespace SifreliIletisim
{
    partial class ŞİFRELİ_İLETİŞİM_UYGULAMASI
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbYontemSecimi = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAnahtarGiris = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbOrijinalMetin = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.alıcımetin = new System.Windows.Forms.RichTextBox();
            this.btnSifrele = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.rtbGonderilecekMetin = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAliciEmail = new System.Windows.Forms.TextBox();
            this.btnEmailGonder = new System.Windows.Forms.Button();
            this.btnEmailIndir = new System.Windows.Forms.Button();
            this.btnSifreCoz = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Şifreleme Yöntemi:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbYontemSecimi
            // 
            this.cmbYontemSecimi.FormattingEnabled = true;
            this.cmbYontemSecimi.Location = new System.Drawing.Point(131, 50);
            this.cmbYontemSecimi.Name = "cmbYontemSecimi";
            this.cmbYontemSecimi.Size = new System.Drawing.Size(121, 24);
            this.cmbYontemSecimi.TabIndex = 2;
            this.cmbYontemSecimi.SelectedIndexChanged += new System.EventHandler(this.sifrebelirleme_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Anahtar / Parametre:";
            // 
            // txtAnahtarGiris
            // 
            this.txtAnahtarGiris.Location = new System.Drawing.Point(481, 50);
            this.txtAnahtarGiris.Name = "txtAnahtarGiris";
            this.txtAnahtarGiris.Size = new System.Drawing.Size(100, 22);
            this.txtAnahtarGiris.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAnahtarGiris);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbYontemSecimi);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(906, 141);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "YÖNTEM VE ANAHTAR AYARLARI";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupBox2.Controls.Add(this.btnEmailGonder);
            this.groupBox2.Controls.Add(this.txtAliciEmail);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.rtbGonderilecekMetin);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnSifrele);
            this.groupBox2.Controls.Add(this.rtbOrijinalMetin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 526);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GÖNDERİCİ PANELİ";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnSifreCoz);
            this.groupBox3.Controls.Add(this.btnEmailIndir);
            this.groupBox3.Controls.Add(this.alıcımetin);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(479, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 526);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ALICI PANELİ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Orjinal Metni Girin ";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // rtbOrijinalMetin
            // 
            this.rtbOrijinalMetin.Location = new System.Drawing.Point(12, 42);
            this.rtbOrijinalMetin.Name = "rtbOrijinalMetin";
            this.rtbOrijinalMetin.Size = new System.Drawing.Size(443, 122);
            this.rtbOrijinalMetin.TabIndex = 1;
            this.rtbOrijinalMetin.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Şİfreli Metin ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // alıcımetin
            // 
            this.alıcımetin.Location = new System.Drawing.Point(9, 42);
            this.alıcımetin.Name = "alıcımetin";
            this.alıcımetin.Size = new System.Drawing.Size(424, 122);
            this.alıcımetin.TabIndex = 2;
            this.alıcımetin.Text = "";
            // 
            // btnSifrele
            // 
            this.btnSifrele.Location = new System.Drawing.Point(12, 171);
            this.btnSifrele.Name = "btnSifrele";
            this.btnSifrele.Size = new System.Drawing.Size(443, 23);
            this.btnSifrele.TabIndex = 2;
            this.btnSifrele.Text = "Metni Düzenle ve Şifrele";
            this.btnSifrele.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Şifrelenmiş Metin Çıktısı:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // rtbGonderilecekMetin
            // 
            this.rtbGonderilecekMetin.Location = new System.Drawing.Point(9, 255);
            this.rtbGonderilecekMetin.Name = "rtbGonderilecekMetin";
            this.rtbGonderilecekMetin.ReadOnly = true;
            this.rtbGonderilecekMetin.Size = new System.Drawing.Size(446, 124);
            this.rtbGonderilecekMetin.TabIndex = 5;
            this.rtbGonderilecekMetin.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 414);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "Alıcının E-Posta Adresi";
            // 
            // txtAliciEmail
            // 
            this.txtAliciEmail.Location = new System.Drawing.Point(9, 434);
            this.txtAliciEmail.Name = "txtAliciEmail";
            this.txtAliciEmail.Size = new System.Drawing.Size(446, 22);
            this.txtAliciEmail.TabIndex = 7;
            // 
            // btnEmailGonder
            // 
            this.btnEmailGonder.Location = new System.Drawing.Point(7, 463);
            this.btnEmailGonder.Name = "btnEmailGonder";
            this.btnEmailGonder.Size = new System.Drawing.Size(448, 23);
            this.btnEmailGonder.TabIndex = 8;
            this.btnEmailGonder.Text = "E-POSTA GÖNDER";
            this.btnEmailGonder.UseVisualStyleBackColor = true;
            // 
            // btnEmailIndir
            // 
            this.btnEmailIndir.Location = new System.Drawing.Point(9, 171);
            this.btnEmailIndir.Name = "btnEmailIndir";
            this.btnEmailIndir.Size = new System.Drawing.Size(424, 23);
            this.btnEmailIndir.TabIndex = 3;
            this.btnEmailIndir.Text = "E-POSTALARI İNDİR";
            this.btnEmailIndir.UseVisualStyleBackColor = true;
            // 
            // btnSifreCoz
            // 
            this.btnSifreCoz.Location = new System.Drawing.Point(6, 200);
            this.btnSifreCoz.Name = "btnSifreCoz";
            this.btnSifreCoz.Size = new System.Drawing.Size(424, 23);
            this.btnSifreCoz.TabIndex = 4;
            this.btnSifreCoz.Text = "ŞİFREYİ ÇÖZ";
            this.btnSifreCoz.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Çözülmüş Metin Çıktısı";
            // 
            // ŞİFRELİ_İLETİŞİM_UYGULAMASI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 697);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ŞİFRELİ_İLETİŞİM_UYGULAMASI";
            this.Text = "ŞİFRELİ_İLETİŞİM_UYGULAMASI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbYontemSecimi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAnahtarGiris;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSifrele;
        private System.Windows.Forms.RichTextBox rtbOrijinalMetin;
        private System.Windows.Forms.RichTextBox alıcımetin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rtbGonderilecekMetin;
        private System.Windows.Forms.Button btnEmailGonder;
        private System.Windows.Forms.TextBox txtAliciEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEmailIndir;
        private System.Windows.Forms.Button btnSifreCoz;
        private System.Windows.Forms.Label label6;
    }
}