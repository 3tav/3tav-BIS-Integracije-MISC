namespace InfoteamClient
{
    partial class Form1
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
            this.btnPostMeterReadData = new System.Windows.Forms.Button();
            this.inputObdobjeOd = new System.Windows.Forms.TextBox();
            this.inputObdobjeDo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetMeterReadData = new System.Windows.Forms.Button();
            this.inputPeriod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.inputAll = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputDateFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.inputDateTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnUvozRocniPopis = new System.Windows.Forms.Button();
            this.inputDatoteka = new System.Windows.Forms.TextBox();
            this.btnUvozRocniMenjave = new System.Windows.Forms.Button();
            this.btnWarehouse = new System.Windows.Forms.Button();
            this.btnPrenosRemote = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPostMeterReadData
            // 
            this.btnPostMeterReadData.Location = new System.Drawing.Point(286, 34);
            this.btnPostMeterReadData.Name = "btnPostMeterReadData";
            this.btnPostMeterReadData.Size = new System.Drawing.Size(135, 23);
            this.btnPostMeterReadData.TabIndex = 0;
            this.btnPostMeterReadData.Text = "Prenesi na Infotim";
            this.btnPostMeterReadData.UseVisualStyleBackColor = true;
            this.btnPostMeterReadData.Click += new System.EventHandler(this.btnPostMeterReadData_Click);
            // 
            // inputObdobjeOd
            // 
            this.inputObdobjeOd.Location = new System.Drawing.Point(83, 36);
            this.inputObdobjeOd.Name = "inputObdobjeOd";
            this.inputObdobjeOd.Size = new System.Drawing.Size(48, 20);
            this.inputObdobjeOd.TabIndex = 1;
            // 
            // inputObdobjeDo
            // 
            this.inputObdobjeDo.Location = new System.Drawing.Point(165, 36);
            this.inputObdobjeDo.Name = "inputObdobjeDo";
            this.inputObdobjeDo.Size = new System.Drawing.Size(48, 20);
            this.inputObdobjeDo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Obdobje od:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "do:";
            // 
            // btnGetMeterReadData
            // 
            this.btnGetMeterReadData.Location = new System.Drawing.Point(286, 115);
            this.btnGetMeterReadData.Name = "btnGetMeterReadData";
            this.btnGetMeterReadData.Size = new System.Drawing.Size(135, 23);
            this.btnGetMeterReadData.TabIndex = 5;
            this.btnGetMeterReadData.Text = "Prenos Nazaj";
            this.btnGetMeterReadData.UseVisualStyleBackColor = true;
            this.btnGetMeterReadData.Click += new System.EventHandler(this.btnGetMeterReadData_Click);
            // 
            // inputPeriod
            // 
            this.inputPeriod.Location = new System.Drawing.Point(83, 96);
            this.inputPeriod.Name = "inputPeriod";
            this.inputPeriod.Size = new System.Drawing.Size(48, 20);
            this.inputPeriod.TabIndex = 6;
            this.inputPeriod.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Period:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "All:";
            // 
            // inputAll
            // 
            this.inputAll.Location = new System.Drawing.Point(193, 96);
            this.inputAll.Name = "inputAll";
            this.inputAll.Size = new System.Drawing.Size(48, 20);
            this.inputAll.TabIndex = 8;
            this.inputAll.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "DateFrom:";
            // 
            // inputDateFrom
            // 
            this.inputDateFrom.Location = new System.Drawing.Point(83, 122);
            this.inputDateFrom.Name = "inputDateFrom";
            this.inputDateFrom.Size = new System.Drawing.Size(52, 20);
            this.inputDateFrom.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "DateTo:";
            // 
            // inputDateTo
            // 
            this.inputDateTo.Location = new System.Drawing.Point(193, 122);
            this.inputDateTo.Name = "inputDateTo";
            this.inputDateTo.Size = new System.Drawing.Size(48, 20);
            this.inputDateTo.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Terminal ID:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 20);
            this.textBox1.TabIndex = 14;
            // 
            // btnUvozRocniPopis
            // 
            this.btnUvozRocniPopis.Location = new System.Drawing.Point(15, 197);
            this.btnUvozRocniPopis.Name = "btnUvozRocniPopis";
            this.btnUvozRocniPopis.Size = new System.Drawing.Size(127, 23);
            this.btnUvozRocniPopis.TabIndex = 16;
            this.btnUvozRocniPopis.Text = "Ročni uvoz - Popis";
            this.btnUvozRocniPopis.UseVisualStyleBackColor = true;
            this.btnUvozRocniPopis.Click += new System.EventHandler(this.btnUvozRocni_Click);
            // 
            // inputDatoteka
            // 
            this.inputDatoteka.Location = new System.Drawing.Point(15, 171);
            this.inputDatoteka.Name = "inputDatoteka";
            this.inputDatoteka.Size = new System.Drawing.Size(456, 20);
            this.inputDatoteka.TabIndex = 17;
            // 
            // btnUvozRocniMenjave
            // 
            this.btnUvozRocniMenjave.Location = new System.Drawing.Point(330, 197);
            this.btnUvozRocniMenjave.Name = "btnUvozRocniMenjave";
            this.btnUvozRocniMenjave.Size = new System.Drawing.Size(141, 23);
            this.btnUvozRocniMenjave.TabIndex = 18;
            this.btnUvozRocniMenjave.Text = "Ročni uvoz - Menjave";
            this.btnUvozRocniMenjave.UseVisualStyleBackColor = true;
            this.btnUvozRocniMenjave.Click += new System.EventHandler(this.btnUvozRocniMenjave_Click);
            // 
            // btnWarehouse
            // 
            this.btnWarehouse.Location = new System.Drawing.Point(178, 197);
            this.btnWarehouse.Name = "btnWarehouse";
            this.btnWarehouse.Size = new System.Drawing.Size(120, 23);
            this.btnWarehouse.TabIndex = 19;
            this.btnWarehouse.Text = "Prenos - Skladišče";
            this.btnWarehouse.UseVisualStyleBackColor = true;
            this.btnWarehouse.Click += new System.EventHandler(this.btnWarehouse_Click);
            // 
            // btnPrenosRemote
            // 
            this.btnPrenosRemote.Location = new System.Drawing.Point(286, 64);
            this.btnPrenosRemote.Name = "btnPrenosRemote";
            this.btnPrenosRemote.Size = new System.Drawing.Size(135, 23);
            this.btnPrenosRemote.TabIndex = 20;
            this.btnPrenosRemote.Text = "Prenos REMOTE";
            this.btnPrenosRemote.UseVisualStyleBackColor = true;
            this.btnPrenosRemote.Click += new System.EventHandler(this.btnPrenosRemote_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 249);
            this.Controls.Add(this.btnPrenosRemote);
            this.Controls.Add(this.btnWarehouse);
            this.Controls.Add(this.btnUvozRocniMenjave);
            this.Controls.Add(this.inputDatoteka);
            this.Controls.Add(this.btnUvozRocniPopis);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.inputDateTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inputDateFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inputPeriod);
            this.Controls.Add(this.btnGetMeterReadData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputObdobjeDo);
            this.Controls.Add(this.inputObdobjeOd);
            this.Controls.Add(this.btnPostMeterReadData);
            this.Name = "Form1";
            this.Text = "Prenos na Infotim";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPostMeterReadData;
        private System.Windows.Forms.TextBox inputObdobjeOd;
        private System.Windows.Forms.TextBox inputObdobjeDo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetMeterReadData;
        private System.Windows.Forms.TextBox inputPeriod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inputAll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox inputDateFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox inputDateTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnUvozRocniPopis;
        private System.Windows.Forms.TextBox inputDatoteka;
        private System.Windows.Forms.Button btnUvozRocniMenjave;
        private System.Windows.Forms.Button btnWarehouse;
        private System.Windows.Forms.Button btnPrenosRemote;
    }
}

