﻿namespace CardonerSistemas
{
    partial class OnScreenKeyboard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OnScreenKeyboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "OnScreenKeyboard";
            this.Size = new System.Drawing.Size(211, 208);
            this.FontChanged += new System.EventHandler(this.FontChangedEvent);
            this.ForeColorChanged += new System.EventHandler(this.ForeColorChangedEvent);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
