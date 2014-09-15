namespace WindowsFormsApplication1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_nextQuestion = new System.Windows.Forms.Button();
            this.textBox_question = new System.Windows.Forms.TextBox();
            this.btn_zoomPlus = new System.Windows.Forms.Button();
            this.btn_zoomMinus = new System.Windows.Forms.Button();
            this.btn_startWrite = new System.Windows.Forms.Button();
            this.btn_setLbl = new System.Windows.Forms.Button();
            this.textBox_lbl = new System.Windows.Forms.TextBox();
            this.textBoxToFile = new System.Windows.Forms.TextBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 595);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(564, 20);
            this.textBox1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 578);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_mouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_mouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel1_mouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 621);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 51);
            this.button1.TabIndex = 2;
            this.button1.Text = "draw mode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_nextQuestion
            // 
            this.btn_nextQuestion.Location = new System.Drawing.Point(452, 621);
            this.btn_nextQuestion.Name = "btn_nextQuestion";
            this.btn_nextQuestion.Size = new System.Drawing.Size(98, 31);
            this.btn_nextQuestion.TabIndex = 3;
            this.btn_nextQuestion.Text = "Next question";
            this.btn_nextQuestion.UseVisualStyleBackColor = true;
            this.btn_nextQuestion.Click += new System.EventHandler(this.btn_nextQuestion_Click);
            // 
            // textBox_question
            // 
            this.textBox_question.Location = new System.Drawing.Point(452, 652);
            this.textBox_question.Name = "textBox_question";
            this.textBox_question.Size = new System.Drawing.Size(213, 20);
            this.textBox_question.TabIndex = 4;
            // 
            // btn_zoomPlus
            // 
            this.btn_zoomPlus.Location = new System.Drawing.Point(105, 613);
            this.btn_zoomPlus.Name = "btn_zoomPlus";
            this.btn_zoomPlus.Size = new System.Drawing.Size(27, 23);
            this.btn_zoomPlus.TabIndex = 5;
            this.btn_zoomPlus.Text = "+";
            this.btn_zoomPlus.UseVisualStyleBackColor = true;
            this.btn_zoomPlus.Click += new System.EventHandler(this.btn_zoomPlus_Click);
            // 
            // btn_zoomMinus
            // 
            this.btn_zoomMinus.Location = new System.Drawing.Point(105, 641);
            this.btn_zoomMinus.Name = "btn_zoomMinus";
            this.btn_zoomMinus.Size = new System.Drawing.Size(27, 27);
            this.btn_zoomMinus.TabIndex = 6;
            this.btn_zoomMinus.Text = "-";
            this.btn_zoomMinus.UseVisualStyleBackColor = true;
            this.btn_zoomMinus.Click += new System.EventHandler(this.btn_zoomMinus_Click);
            // 
            // btn_startWrite
            // 
            this.btn_startWrite.Location = new System.Drawing.Point(264, 620);
            this.btn_startWrite.Name = "btn_startWrite";
            this.btn_startWrite.Size = new System.Drawing.Size(81, 26);
            this.btn_startWrite.TabIndex = 7;
            this.btn_startWrite.Text = "startWrite";
            this.btn_startWrite.UseVisualStyleBackColor = true;
            this.btn_startWrite.Click += new System.EventHandler(this.btn_startWrite_Click);
            // 
            // btn_setLbl
            // 
            this.btn_setLbl.Location = new System.Drawing.Point(264, 652);
            this.btn_setLbl.Name = "btn_setLbl";
            this.btn_setLbl.Size = new System.Drawing.Size(76, 21);
            this.btn_setLbl.TabIndex = 8;
            this.btn_setLbl.Text = "set label";
            this.btn_setLbl.UseVisualStyleBackColor = true;
            this.btn_setLbl.Click += new System.EventHandler(this.btn_setLbl_Click);
            // 
            // textBox_lbl
            // 
            this.textBox_lbl.Location = new System.Drawing.Point(346, 652);
            this.textBox_lbl.Name = "textBox_lbl";
            this.textBox_lbl.Size = new System.Drawing.Size(100, 20);
            this.textBox_lbl.TabIndex = 9;
            // 
            // textBoxToFile
            // 
            this.textBoxToFile.Location = new System.Drawing.Point(669, 595);
            this.textBoxToFile.Multiline = true;
            this.textBoxToFile.Name = "textBoxToFile";
            this.textBoxToFile.Size = new System.Drawing.Size(319, 70);
            this.textBoxToFile.TabIndex = 10;
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(138, 617);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(82, 35);
            this.btnUndo.TabIndex = 11;
            this.btnUndo.Text = "Undo one coordinate";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 673);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.textBoxToFile);
            this.Controls.Add(this.textBox_lbl);
            this.Controls.Add(this.btn_setLbl);
            this.Controls.Add(this.btn_startWrite);
            this.Controls.Add(this.btn_zoomMinus);
            this.Controls.Add(this.btn_zoomPlus);
            this.Controls.Add(this.textBox_question);
            this.Controls.Add(this.btn_nextQuestion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void SetText(string text)
        {
            textBox1.Text = text;
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_nextQuestion;
        private System.Windows.Forms.TextBox textBox_question;
        private System.Windows.Forms.Button btn_zoomPlus;
        private System.Windows.Forms.Button btn_zoomMinus;
        private System.Windows.Forms.Button btn_startWrite;
        private System.Windows.Forms.Button btn_setLbl;
        private System.Windows.Forms.TextBox textBox_lbl;
        private System.Windows.Forms.TextBox textBoxToFile;
        private System.Windows.Forms.Button btnUndo;

    }
}

