namespace test
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
            this.controlsOnScreenKeyboard1 = new CardonerSistemas.ControlsOnScreenKeyboard();
            this.SuspendLayout();
            // 
            // controlsOnScreenKeyboard1
            // 
            this.controlsOnScreenKeyboard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.controlsOnScreenKeyboard1.DestinationTextBox = null;
            this.controlsOnScreenKeyboard1.KeyboardLayout = CardonerSistemas.ControlsOnScreenKeyboard.KeyboardLayoutEnums.NumericPhone;
            this.controlsOnScreenKeyboard1.Location = new System.Drawing.Point(22, 12);
            this.controlsOnScreenKeyboard1.Name = "controlsOnScreenKeyboard1";
            this.controlsOnScreenKeyboard1.Size = new System.Drawing.Size(766, 294);
            this.controlsOnScreenKeyboard1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.controlsOnScreenKeyboard1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CardonerSistemas.ControlsOnScreenKeyboard controlsOnScreenKeyboard1;
    }
}

