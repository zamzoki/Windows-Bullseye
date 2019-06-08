namespace BullseyeCursors
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.targetPictureBox = new System.Windows.Forms.PictureBox();
            this.attemptsLabel = new System.Windows.Forms.Label();
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.xCursorPictureBox = new System.Windows.Forms.PictureBox();
            this.xCursorTimer = new System.Windows.Forms.Timer(this.components);
            this.yCursorTimer = new System.Windows.Forms.Timer(this.components);
            this.yCursorPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.targetPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCursorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCursorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // targetPictureBox
            // 
            this.targetPictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.targetPictureBox.Location = new System.Drawing.Point(75, 65);
            this.targetPictureBox.Name = "targetPictureBox";
            this.targetPictureBox.Size = new System.Drawing.Size(400, 400);
            this.targetPictureBox.TabIndex = 0;
            this.targetPictureBox.TabStop = false;
            // 
            // attemptsLabel
            // 
            this.attemptsLabel.AutoSize = true;
            this.attemptsLabel.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attemptsLabel.Location = new System.Drawing.Point(94, 18);
            this.attemptsLabel.Name = "attemptsLabel";
            this.attemptsLabel.Size = new System.Drawing.Size(113, 31);
            this.attemptsLabel.TabIndex = 1;
            this.attemptsLabel.Text = "Attempts";
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.AutoSize = true;
            this.InstructionsLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionsLabel.Location = new System.Drawing.Point(12, 532);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(40, 17);
            this.InstructionsLabel.TabIndex = 2;
            this.InstructionsLabel.Text = "EscR";
            // 
            // pointsLabel
            // 
            this.pointsLabel.AutoSize = true;
            this.pointsLabel.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointsLabel.Location = new System.Drawing.Point(309, 18);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(82, 31);
            this.pointsLabel.TabIndex = 3;
            this.pointsLabel.Text = "Points";
            // 
            // timer1
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // xCursorPictureBox
            // 
            this.xCursorPictureBox.Location = new System.Drawing.Point(75, 488);
            this.xCursorPictureBox.Name = "xCursorPictureBox";
            this.xCursorPictureBox.Size = new System.Drawing.Size(400, 10);
            this.xCursorPictureBox.TabIndex = 4;
            this.xCursorPictureBox.TabStop = false;
            // 
            // timerx
            // 
            this.xCursorTimer.Interval = 2;
            this.xCursorTimer.Tick += new System.EventHandler(this.xCursorTimer_Tick);
            // 
            // timery
            // 
            this.yCursorTimer.Interval = 2;
            this.yCursorTimer.Tick += new System.EventHandler(this.yCursorTimer_Tick);
            // 
            // yCursorPictureBox
            // 
            this.yCursorPictureBox.Location = new System.Drawing.Point(505, 65);
            this.yCursorPictureBox.Name = "yCursorPictureBox";
            this.yCursorPictureBox.Size = new System.Drawing.Size(10, 400);
            this.yCursorPictureBox.TabIndex = 5;
            this.yCursorPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 591);
            this.Controls.Add(this.yCursorPictureBox);
            this.Controls.Add(this.xCursorPictureBox);
            this.Controls.Add(this.pointsLabel);
            this.Controls.Add(this.InstructionsLabel);
            this.Controls.Add(this.attemptsLabel);
            this.Controls.Add(this.targetPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Bullseye";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.targetPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCursorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCursorPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox targetPictureBox;
        private System.Windows.Forms.Label attemptsLabel;
        private System.Windows.Forms.Label InstructionsLabel;
        private System.Windows.Forms.Label pointsLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox xCursorPictureBox;
        private System.Windows.Forms.Timer xCursorTimer;
        private System.Windows.Forms.Timer yCursorTimer;
        private System.Windows.Forms.PictureBox yCursorPictureBox;
    }
}

