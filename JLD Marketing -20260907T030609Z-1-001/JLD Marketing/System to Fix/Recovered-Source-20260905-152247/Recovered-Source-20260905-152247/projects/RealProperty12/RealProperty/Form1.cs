using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RealProperty;

public class Form1 : Form
{
	private IContainer components = null;

	public Form1()
	{
		InitializeComponent();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(282, 253);
		base.Name = "Form1";
		this.Text = "Form1";
		base.Load += new System.EventHandler(Form1_Load);
		base.ResumeLayout(false);
	}
}
