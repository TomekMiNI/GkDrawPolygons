using System;
using System.Drawing;
using System.Windows.Forms;

namespace GK1
{
    partial class Form1
    {
        private PictureBox pic_box;
        private TableLayoutPanel lay_panel;
        private Button _deleteBtn;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._deleteBtn = new System.Windows.Forms.Button();
            this.pic_box = new System.Windows.Forms.PictureBox();
            this.lay_panel = new System.Windows.Forms.TableLayoutPanel();
            this._menu = new System.Windows.Forms.MenuStrip();
            this._newDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this._intrBtn = new System.Windows.Forms.Button();
            this._angleText = new System.Windows.Forms.TextBox();
            this._verticalBtn = new System.Windows.Forms.Button();
            this._horizontalBtn = new System.Windows.Forms.Button();
            this._restrictions = new System.Windows.Forms.Label();
            this._resetBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_box)).BeginInit();
            this.lay_panel.SuspendLayout();
            this._menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _deleteBtn
            // 
            this._deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._deleteBtn.Location = new System.Drawing.Point(10, 34);
            this._deleteBtn.Margin = new System.Windows.Forms.Padding(10);
            this._deleteBtn.Name = "_deleteBtn";
            this._deleteBtn.Size = new System.Drawing.Size(142, 28);
            this._deleteBtn.TabIndex = 0;
            this._deleteBtn.Text = "Delete verticle";
            this._deleteBtn.Click += new System.EventHandler(this.deleteVerticle);
            // 
            // pic_box
            // 
            this.pic_box.BackColor = System.Drawing.Color.White;
            this.pic_box.Image = ((System.Drawing.Image)(resources.GetObject("pic_box.Image")));
            this.pic_box.Location = new System.Drawing.Point(165, 27);
            this.pic_box.Name = "pic_box";
            this.lay_panel.SetRowSpan(this.pic_box, 7);
            this.pic_box.Size = new System.Drawing.Size(518, 337);
            this.pic_box.TabIndex = 0;
            this.pic_box.TabStop = false;
            this.pic_box.Paint += new System.Windows.Forms.PaintEventHandler(this.refreshPolygon);
            this.pic_box.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.addVerticle);
            this.pic_box.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingOnClick);
            this.pic_box.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawing);
            this.pic_box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stopSwaping);
            // 
            // lay_panel
            // 
            this.lay_panel.ColumnCount = 2;
            this.lay_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.lay_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 524F));
            this.lay_panel.Controls.Add(this._menu, 0, 0);
            this.lay_panel.Controls.Add(this._deleteBtn, 0, 1);
            this.lay_panel.Controls.Add(this._intrBtn, 0, 6);
            this.lay_panel.Controls.Add(this._angleText, 0, 5);
            this.lay_panel.Controls.Add(this._verticalBtn, 0, 4);
            this.lay_panel.Controls.Add(this._horizontalBtn, 0, 3);
            this.lay_panel.Controls.Add(this._restrictions, 0, 2);
            this.lay_panel.Controls.Add(this.pic_box, 1, 1);
            this.lay_panel.Controls.Add(this._resetBtn, 0, 7);
            this.lay_panel.Location = new System.Drawing.Point(0, -3);
            this.lay_panel.Name = "lay_panel";
            this.lay_panel.RowCount = 8;
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.lay_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.lay_panel.Size = new System.Drawing.Size(686, 364);
            this.lay_panel.TabIndex = 0;
            // 
            // _menu
            // 
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newDrawing});
            this._menu.Location = new System.Drawing.Point(0, 0);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(162, 24);
            this._menu.TabIndex = 3;
            this._menu.Text = "menuStrip1";
            // 
            // _newDrawing
            // 
            this._newDrawing.Name = "_newDrawing";
            this._newDrawing.Size = new System.Drawing.Size(90, 20);
            this._newDrawing.Text = "New Drawing";
            this._newDrawing.Click += new System.EventHandler(this.newDrawing);
            // 
            // _intrBtn
            // 
            this._intrBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._intrBtn.Location = new System.Drawing.Point(10, 265);
            this._intrBtn.Margin = new System.Windows.Forms.Padding(10);
            this._intrBtn.Name = "_intrBtn";
            this._intrBtn.Size = new System.Drawing.Size(142, 32);
            this._intrBtn.TabIndex = 4;
            this._intrBtn.Text = "Introduce angle";
            this._intrBtn.UseVisualStyleBackColor = true;
            this._intrBtn.Click += new System.EventHandler(this.introduceAngle);
            // 
            // _angleText
            // 
            this._angleText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._angleText.Location = new System.Drawing.Point(10, 221);
            this._angleText.Margin = new System.Windows.Forms.Padding(10);
            this._angleText.Name = "_angleText";
            this._angleText.Size = new System.Drawing.Size(142, 20);
            this._angleText.TabIndex = 5;
            // 
            // _verticalBtn
            // 
            this._verticalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._verticalBtn.Location = new System.Drawing.Point(10, 172);
            this._verticalBtn.Margin = new System.Windows.Forms.Padding(10);
            this._verticalBtn.Name = "_verticalBtn";
            this._verticalBtn.Size = new System.Drawing.Size(142, 29);
            this._verticalBtn.TabIndex = 2;
            this._verticalBtn.Text = "Vertical edge";
            this._verticalBtn.UseVisualStyleBackColor = true;
            this._verticalBtn.Click += new System.EventHandler(this.setVertical);
            // 
            // _horizontalBtn
            // 
            this._horizontalBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._horizontalBtn.Location = new System.Drawing.Point(10, 123);
            this._horizontalBtn.Margin = new System.Windows.Forms.Padding(10);
            this._horizontalBtn.Name = "_horizontalBtn";
            this._horizontalBtn.Size = new System.Drawing.Size(142, 29);
            this._horizontalBtn.TabIndex = 1;
            this._horizontalBtn.Text = "Horizontal edge";
            this._horizontalBtn.UseVisualStyleBackColor = true;
            this._horizontalBtn.Click += new System.EventHandler(this.setHorizontal);
            // 
            // _restrictions
            // 
            this._restrictions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._restrictions.AutoSize = true;
            this._restrictions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this._restrictions.Location = new System.Drawing.Point(10, 82);
            this._restrictions.Margin = new System.Windows.Forms.Padding(10);
            this._restrictions.Name = "_restrictions";
            this._restrictions.Size = new System.Drawing.Size(142, 21);
            this._restrictions.TabIndex = 6;
            this._restrictions.Text = "Restrictions";
            // 
            // _resetBtn
            // 
            this._resetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._resetBtn.Location = new System.Drawing.Point(10, 317);
            this._resetBtn.Margin = new System.Windows.Forms.Padding(10);
            this._resetBtn.Name = "_resetBtn";
            this._resetBtn.Size = new System.Drawing.Size(142, 28);
            this._resetBtn.TabIndex = 7;
            this._resetBtn.Text = "Reset angle";
            this._resetBtn.UseVisualStyleBackColor = true;
            this._resetBtn.Click += new System.EventHandler(this.resetClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.lay_panel);
            this.MainMenuStrip = this._menu;
            this.MaximumSize = new System.Drawing.Size(700, 400);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "Form1";
            this.Text = "Polygons";
            ((System.ComponentModel.ISupportInitialize)(this.pic_box)).EndInit();
            this.lay_panel.ResumeLayout(false);
            this.lay_panel.PerformLayout();
            this._menu.ResumeLayout(false);
            this._menu.PerformLayout();
            this.ResumeLayout(false);

        }






        #endregion

        private Button _horizontalBtn;
        private Button _verticalBtn;
        private MenuStrip _menu;
        private ToolStripMenuItem _newDrawing;
        private Button _intrBtn;
        private TextBox _angleText;
        private Label _restrictions;
        private Button _resetBtn;
    }
}

