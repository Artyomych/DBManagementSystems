
namespace EntityFrameworkApp
{
    partial class StartForm
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
            this.SignUp_btn = new System.Windows.Forms.Button();
            this.LogIn_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignUp_btn
            // 
            this.SignUp_btn.Location = new System.Drawing.Point(180, 222);
            this.SignUp_btn.Name = "SignUp_btn";
            this.SignUp_btn.Size = new System.Drawing.Size(139, 48);
            this.SignUp_btn.TabIndex = 0;
            this.SignUp_btn.Text = "Регистрация";
            this.SignUp_btn.UseVisualStyleBackColor = true;
            this.SignUp_btn.Click += new System.EventHandler(this.SignUp_btn_Click);
            // 
            // LogIn_btn
            // 
            this.LogIn_btn.Location = new System.Drawing.Point(180, 123);
            this.LogIn_btn.Name = "LogIn_btn";
            this.LogIn_btn.Size = new System.Drawing.Size(139, 48);
            this.LogIn_btn.TabIndex = 1;
            this.LogIn_btn.Text = "Вход";
            this.LogIn_btn.UseVisualStyleBackColor = true;
            this.LogIn_btn.Click += new System.EventHandler(this.LogIn_btn_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 398);
            this.Controls.Add(this.LogIn_btn);
            this.Controls.Add(this.SignUp_btn);
            this.Name = "StartForm";
            this.Text = "Начальная форма";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SignUp_btn;
        private System.Windows.Forms.Button LogIn_btn;
    }
}

