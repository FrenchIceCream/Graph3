namespace WinFormsApp1
{
    partial class Form1
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
            Canvas = new PictureBox();
            Button1 = new Button();
            Button2 = new Button();
            Button_SetPoint = new Button();
            Button_Turn = new Button();
            Button_Scale = new Button();
            dx = new TextBox();
            dy = new TextBox();
            dx_label = new Label();
            dy_label = new Label();
            Button_Move = new Button();
            degree = new TextBox();
            kx = new TextBox();
            ky = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.BackColor = SystemColors.Window;
            Canvas.Location = new Point(0, 0);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(827, 580);
            Canvas.TabIndex = 0;
            Canvas.TabStop = false;
            Canvas.Click += Canvas_Click;
            // 
            // Button1
            // 
            Button1.Location = new Point(881, 27);
            Button1.Name = "Button1";
            Button1.Size = new Size(135, 36);
            Button1.TabIndex = 1;
            Button1.Text = "Рисовать";
            Button1.UseVisualStyleBackColor = true;
            Button1.Click += Button1_Click;
            // 
            // Button2
            // 
            Button2.Location = new Point(881, 69);
            Button2.Name = "Button2";
            Button2.Size = new Size(135, 36);
            Button2.TabIndex = 2;
            Button2.Text = "Очистить";
            Button2.UseVisualStyleBackColor = true;
            Button2.Click += Button2_Click;
            // 
            // Button_SetPoint
            // 
            Button_SetPoint.Location = new Point(881, 213);
            Button_SetPoint.Name = "Button_SetPoint";
            Button_SetPoint.Size = new Size(135, 35);
            Button_SetPoint.TabIndex = 3;
            Button_SetPoint.Text = "Задать точку";
            Button_SetPoint.UseVisualStyleBackColor = true;
            Button_SetPoint.Click += Button_SetPoint_Click;
            // 
            // Button_Turn
            // 
            Button_Turn.Location = new Point(881, 295);
            Button_Turn.Name = "Button_Turn";
            Button_Turn.Size = new Size(135, 38);
            Button_Turn.TabIndex = 4;
            Button_Turn.Text = "Поворот";
            Button_Turn.UseVisualStyleBackColor = true;
            Button_Turn.Click += Button_Turn_Click;
            // 
            // Button_Scale
            // 
            Button_Scale.Location = new Point(881, 339);
            Button_Scale.Name = "Button_Scale";
            Button_Scale.Size = new Size(135, 38);
            Button_Scale.TabIndex = 5;
            Button_Scale.Text = "Масштаб";
            Button_Scale.UseVisualStyleBackColor = true;
            Button_Scale.Click += Button_Scale_Click;
            // 
            // dx
            // 
            dx.Location = new Point(881, 403);
            dx.Name = "dx";
            dx.Size = new Size(59, 27);
            dx.TabIndex = 6;
            // 
            // dy
            // 
            dy.Location = new Point(957, 403);
            dy.Name = "dy";
            dy.Size = new Size(59, 27);
            dy.TabIndex = 7;
            // 
            // dx_label
            // 
            dx_label.AutoSize = true;
            dx_label.Location = new Point(896, 380);
            dx_label.Name = "dx_label";
            dx_label.Size = new Size(25, 20);
            dx_label.TabIndex = 8;
            dx_label.Text = "dx";
            // 
            // dy_label
            // 
            dy_label.AutoSize = true;
            dy_label.Location = new Point(973, 380);
            dy_label.Name = "dy_label";
            dy_label.Size = new Size(25, 20);
            dy_label.TabIndex = 9;
            dy_label.Text = "dy";
            // 
            // Button_Move
            // 
            Button_Move.Location = new Point(881, 254);
            Button_Move.Name = "Button_Move";
            Button_Move.Size = new Size(135, 35);
            Button_Move.TabIndex = 10;
            Button_Move.Text = "Смещение";
            Button_Move.UseVisualStyleBackColor = true;
            Button_Move.Click += Button_Move_Click;
            // 
            // degree
            // 
            degree.Location = new Point(957, 455);
            degree.Name = "degree";
            degree.Size = new Size(59, 27);
            degree.TabIndex = 11;
            // 
            // kx
            // 
            kx.Location = new Point(881, 507);
            kx.Name = "kx";
            kx.Size = new Size(59, 27);
            kx.TabIndex = 12;
            // 
            // ky
            // 
            ky.Location = new Point(957, 507);
            ky.Name = "ky";
            ky.Size = new Size(59, 27);
            ky.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(973, 484);
            label1.Name = "label1";
            label1.Size = new Size(23, 20);
            label1.TabIndex = 15;
            label1.Text = "ky";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(896, 484);
            label2.Name = "label2";
            label2.Size = new Size(23, 20);
            label2.TabIndex = 14;
            label2.Text = "kx";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(888, 457);
            label3.Name = "label3";
            label3.Size = new Size(44, 20);
            label3.TabIndex = 16;
            label3.Text = "Угол:";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // button3
            // 
            button3.Location = new Point(881, 111);
            button3.Name = "button3";
            button3.Size = new Size(135, 38);
            button3.TabIndex = 17;
            button3.Text = "Поворот на 90";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(881, 155);
            button4.Name = "button4";
            button4.Size = new Size(137, 55);
            button4.TabIndex = 18;
            button4.Text = "Точка пересечения ";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1056, 577);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(ky);
            Controls.Add(kx);
            Controls.Add(degree);
            Controls.Add(Button_Move);
            Controls.Add(dy_label);
            Controls.Add(dx_label);
            Controls.Add(dy);
            Controls.Add(dx);
            Controls.Add(Button_Scale);
            Controls.Add(Button_Turn);
            Controls.Add(Button_SetPoint);
            Controls.Add(Button2);
            Controls.Add(Button1);
            Controls.Add(Canvas);
            Name = "Form1";
            Text = "Графика. Лабораторная 3";
            ((System.ComponentModel.ISupportInitialize)Canvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private PictureBox Canvas;
        private Button Button1;
        private Button Button2;
        private Button Button_SetPoint;
        private Button Button_Turn;
        private Button Button_Scale;
        private TextBox dx;
        private TextBox dy;
        private Label dx_label;
        private Label dy_label;
        private Button Button_Move;
        private TextBox degree;
        private TextBox kx;
        private TextBox ky;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button3;
        private Button button4;
    }
}