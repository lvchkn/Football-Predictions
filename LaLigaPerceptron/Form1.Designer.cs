﻿
namespace LaLigaPerceptron
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.rtb2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtb1
            // 
            this.rtb1.Location = new System.Drawing.Point(202, 12);
            this.rtb1.Name = "rtb1";
            this.rtb1.Size = new System.Drawing.Size(701, 426);
            this.rtb1.TabIndex = 0;
            this.rtb1.Text = "";
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(12, 12);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(140, 52);
            this.runBtn.TabIndex = 1;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // rtb2
            // 
            this.rtb2.Location = new System.Drawing.Point(12, 70);
            this.rtb2.Name = "rtb2";
            this.rtb2.Size = new System.Drawing.Size(184, 368);
            this.rtb2.TabIndex = 2;
            this.rtb2.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 450);
            this.Controls.Add(this.rtb2);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.rtb1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb1;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.RichTextBox rtb2;
    }
}

