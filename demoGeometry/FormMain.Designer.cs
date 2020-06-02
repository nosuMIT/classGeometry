namespace demoGeometry
{
    partial class FormMain
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
            this.buttonBuildHull = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBuildHull
            // 
            this.buttonBuildHull.Location = new System.Drawing.Point(73, 31);
            this.buttonBuildHull.Name = "buttonBuildHull";
            this.buttonBuildHull.Size = new System.Drawing.Size(188, 23);
            this.buttonBuildHull.TabIndex = 0;
            this.buttonBuildHull.Text = "Построить оболочку";
            this.buttonBuildHull.UseVisualStyleBackColor = true;
            this.buttonBuildHull.Click += new System.EventHandler(this.buttonBuildHull_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonBuildHull);
            this.Name = "FormMain";
            this.Text = "Построение выпуклой оболочки";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBuildHull;
    }
}

