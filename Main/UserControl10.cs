﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;


namespace Main
{
    public partial class UserControl10 : UserControl
    {
        public UserControl10()
        {
            InitializeComponent();
        }


        private void UserControl10_Load(object sender, EventArgs e)
        {
            string path = @"qcom/huawei";
            DataTable table = new DataTable();
            table.Columns.Add("File Name");
            table.Columns.Add("File Path");

            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = new FileInfo(files[i]);
                table.Rows.Add(file.Name, path + "\\" + file.Name);
            }
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "File Name";
            comboBox1.ValueMember = "File Path";
            comboBox1.SelectedItem = files[0];
        }
        void romprovider(object sender, DataReceivedEventArgs e)
        {
            string receivedMessage = e.Data + Environment.NewLine;
            if (!string.IsNullOrEmpty(receivedMessage))
            {
                TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                //Output the data into the TextBox
                SetTextToTextBox(myform1textbox1, receivedMessage);
                myform1textbox1.SelectionStart = myform1textbox1.Text.Length;
                myform1textbox1.ScrollToCaret();
                string str = null;
                str = e.Data;
                str = e.Data + null;
                if (str.Contains("The handle is invalid.") == true)
                {
                    myform1textbox1.Text = ("===========================Error device in sahara Mod or Somthing is wrong=================================" + Environment.NewLine);
                }
                if (str.Contains("0 partition images created") == true)
                {
                    myform1textbox1.AppendText("========================Error device in sahara Mod or Somthing is wrong==================================" + Environment.NewLine);
                }
                if (str.Contains("<?xml") == true)
                {
                    myform1textbox1.AppendText("==============================Processing============================================" + Environment.NewLine);
                }
                if (str.Contains("Successfully found GPT partition") == true)
                {
                    myform1textbox1.Text = ("================================Processing============================================" + Environment.NewLine);
                }
                if (str.Contains("ERROR: Option") == true)
                {
                    myform1textbox1.Text = ("==============================Error device in sahara Mod or Somthing is wrong===================================" + Environment.NewLine);
                }
                if (str.Contains("fh_loader.exe") == true)
                {
                    myform1textbox1.Text = ("==============================Flashing Process Started.. don't disconnect device==================================" + Environment.NewLine);
                }
                if (str.Contains("pathFile") == true)
                {
                    myform1textbox1.Text = ("===============================Please Wait.. don't disconnect device==================================" + Environment.NewLine);
                }
                if (str.Contains("emmcdl.exe") == true)
                {
                    myform1textbox1.Text = ("===============================Please Wait.. don't disconnect device==================================" + Environment.NewLine);
                }
                if (str.Contains("fh.attrs.MaxPayloadSizeToTargetInBytes") == true)
                {
                    progressBar1.Value = 10;
                }
                if (str.Contains("partition images created") == true)
                {
                    progressBar1.Value = 100;
                    myform1textbox1.AppendText("==================================Success- You can disconnect device==================================" + Environment.NewLine);
                }
                if (str.Contains("echo Firehose file") == true)
                {
                    progressBar1.Value = 100;
                    myform1textbox1.Text = ("================================Please Select Firehose File==================================" + Environment.NewLine);
                }
                if (str.Contains("23.") == true)
                {
                    progressBar1.Value = 25;
                }
                if (str.Contains("29.") == true)
                {
                    progressBar1.Value = 30;
                }
                if (str.Contains("39.") == true)
                {
                    progressBar1.Value = 39;
                }
                if (str.Contains("63.") == true)
                {
                    progressBar1.Value = 60;
                }
                if (str.Contains("81.") == true)
                {
                    progressBar1.Value = 80;
                }
                if (str.Contains("91.") == true)
                {
                    progressBar1.Value = 90;
                }
                if (str.Contains("All Finished Successfully") == true)
                {
                    progressBar1.Value = 100;
                }
            }
            else
            {

            }
        }
        //Delegate representing the method in which you set text to a RichTextBox
        delegate void CallBackTextBox(TextBox rtf, string text);
        //The method in which you set text to a RichTextBox
        private void SetTextToTextBox(TextBox rtf, string text)
        {
            try
            {
                if (rtf.InvokeRequired)
                {
                    CallBackTextBox d = new CallBackTextBox(SetTextToTextBox);
                    this.Invoke(d, new object[] { rtf, text });
                }
                else
                {
                    rtf.Text = rtf.Text + text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
                port7.PORTFIND7();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) if (checkBox5.Checked == true)
                {
                TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                myform1textbox1.Clear();
                myform1textbox1.AppendText("Please Wait..." + Environment.NewLine);
                myform1textbox1.AppendText("Sending EMMC command..." + Environment.NewLine);
                myform1textbox1.AppendText("Loading Programmer to device......." + Environment.NewLine);
                myform1textbox1.AppendText("Waiting for Result's......." + Environment.NewLine);
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "./c emmcdl.exe -p \"" + PORTER7.Text + "\" -MemoryName emmc -f qcom/huawei/\"" + comboBox1.Text + "\" -e userdata";
                p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                p.Start();
                p.BeginOutputReadLine();
            }
            if (checkBox1.Checked == true) if (checkBox6.Checked == true)
                {
                    TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                    myform1textbox1.Clear();
                    myform1textbox1.AppendText("Please Wait..." + Environment.NewLine);
                    myform1textbox1.AppendText("Sending EMMC command..." + Environment.NewLine);
                    myform1textbox1.AppendText("Loading Programmer to device......." + Environment.NewLine);
                    myform1textbox1.AppendText("Waiting for Result's......." + Environment.NewLine);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "./c emmcdl.exe -p \"" + PORTER7.Text + "\" -MemoryName ufs -f qcom/huawei/\"" + comboBox1.Text + "\" -e userdata";
                    p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                    p.Start();
                    p.BeginOutputReadLine();
                }
            if (checkBox2.Checked == true) if (checkBox5.Checked == true)
                {
                String line;
                try
                {
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter("qcom\\params.cmd");
                    //Write a line of text
                    sw.Write("set SaharaComPort=");
                    sw.Write(PORTER7.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set FirehoseFile=qcom/huawei/");
                    sw.Write(comboBox1.Text);
                    sw.WriteLine();
                    sw.WriteLine("set SelPartsFile=%~dp0parttable.txt");
                    sw.WriteLine("set PartTableFile=%~dp0parttable.txt");
                    sw.WriteLine("set PartTableFileX=%~dp0parttable2.txt");
                    sw.WriteLine("set RawProgFile=%~dp0rawprogram0.xml");
                    sw.WriteLine("set emmcdl_dir=%~dp0");
                    sw.WriteLine("set memory=emmc");
                    sw.WriteLine("set xmlFile=%~dp0rawprogram0.xml");

                        //Close the file
                        sw.Close();
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Exception: " + a.Message);
                    }
                    finally
                    {
                        Console.WriteLine("Executing finally block.");
                    }
                    TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                    myform1textbox1.Clear();
                    myform1textbox1.AppendText("===================================Process Started..........================================================" + Environment.NewLine);
                    myform1textbox1.AppendText("===================================Waiting for Result's.....================================================" + Environment.NewLine);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "./c qcom\\start.cmd";
                    p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                    p.Start();
                    p.BeginOutputReadLine();
                }
            if (checkBox2.Checked == true) if (checkBox6.Checked == true)
                {
                    String line;
                    try
                    {
                        //Pass the filepath and filename to the StreamWriter Constructor
                        StreamWriter sw = new StreamWriter("qcom\\params.cmd");
                        //Write a line of text
                        sw.Write("set SaharaComPort=");
                        sw.Write(PORTER7.Text);
                        sw.WriteLine();
                        //Write a second line of text
                        sw.Write("set FirehoseFile=qcom/huawei/");
                        sw.Write(comboBox1.Text);
                        sw.WriteLine();
                        sw.WriteLine("set SelPartsFile=%~dp0parttable.txt");
                        sw.WriteLine("set PartTableFile=%~dp0parttable.txt");
                        sw.WriteLine("set PartTableFileX=%~dp0parttable2.txt");
                        sw.WriteLine("set RawProgFile=%~dp0rawprogram0.xml");
                        sw.WriteLine("set emmcdl_dir=%~dp0");
                        sw.WriteLine("set memory=ufs");
                        sw.WriteLine("set xmlFile=%~dp0rawprogram0.xml");

                        //Close the file
                        sw.Close();
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Exception: " + a.Message);
                    }
                    finally
                    {
                        Console.WriteLine("Executing finally block.");
                    }
                    TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                    myform1textbox1.Clear();
                    myform1textbox1.AppendText("===================================Process Started..........================================================" + Environment.NewLine);
                    myform1textbox1.AppendText("===================================Waiting for Result's.....================================================" + Environment.NewLine);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "./c qcom\\start.cmd";
                    p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                    p.Start();
                    p.BeginOutputReadLine();
                }
            if (checkBox3.Checked == true) if (checkBox5.Checked == true)
                {
                TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                myform1textbox1.Clear();
                myform1textbox1.AppendText("Please Wait..." + Environment.NewLine);
                myform1textbox1.AppendText("Sending command..." + Environment.NewLine);
                myform1textbox1.AppendText("Loading Programmer to device......." + Environment.NewLine);
                myform1textbox1.AppendText("Waiting for Result's......." + Environment.NewLine);
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "./c emmcdl.exe -p \"" + PORTER7.Text + "\" -MemoryName emmc -f qcom/huawei/\"" + comboBox1.Text + "\" -e oeminfo";
                p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                p.Start();
                p.BeginOutputReadLine();
            }
            if (checkBox3.Checked == true) if (checkBox6.Checked == true)
                {
                    TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                    myform1textbox1.Clear();
                    myform1textbox1.AppendText("Please Wait..." + Environment.NewLine);
                    myform1textbox1.AppendText("Sending command..." + Environment.NewLine);
                    myform1textbox1.AppendText("Loading Programmer to device......." + Environment.NewLine);
                    myform1textbox1.AppendText("Waiting for Result's......." + Environment.NewLine);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "./c emmcdl.exe -p \"" + PORTER7.Text + "\" -MemoryName ufs -f qcom/huawei/\"" + comboBox1.Text + "\" -e oeminfo";
                    p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                    p.Start();
                    p.BeginOutputReadLine();
                }
            if (checkBox4.Checked == true) if (checkBox5.Checked == true)
                {
                TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                myform1textbox1.Clear();
                myform1textbox1.AppendText("Please Wait..." + Environment.NewLine);
                myform1textbox1.AppendText("Sending command..." + Environment.NewLine);
                myform1textbox1.AppendText("Loading Programmer to device......." + Environment.NewLine);
                myform1textbox1.AppendText("Waiting for Result's......." + Environment.NewLine);
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "./c emmcdl.exe -p \"" + PORTER7.Text + "\" -MemoryName emmc -f qcom/huawei/\"" + comboBox1.Text + "\" -e config";
                p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                p.Start();
                p.BeginOutputReadLine();
            }
            if (checkBox4.Checked == true) if (checkBox6.Checked == true)
                {
                    TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                    myform1textbox1.Clear();
                    myform1textbox1.AppendText("Please Wait..." + Environment.NewLine);
                    myform1textbox1.AppendText("Sending command..." + Environment.NewLine);
                    myform1textbox1.AppendText("Loading Programmer to device......." + Environment.NewLine);
                    myform1textbox1.AppendText("Waiting for Result's......." + Environment.NewLine);
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "./c emmcdl.exe -p \"" + PORTER7.Text + "\" -MemoryName ufs -f qcom/huawei/\"" + comboBox1.Text + "\" -e config";
                    p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                    p.Start();
                    p.BeginOutputReadLine();
                }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                textBox8.Text = dialog.FileName;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "rawprogram0|*.xml;";
            openfile.Title = "Open a file..";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                textBox9.Text = openfile.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                try
                {
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter("qcom\\qf.cmd");
                    //Write a line of text
                    sw.Write("set SaharaComPort=");
                    sw.Write(PORTER7.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set FirehoseFile=qcom\\huawei\\");
                    sw.Write(comboBox1.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set xmlFile=");
                    sw.Write(textBox9.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set pathFile=");
                    sw.Write(textBox8.Text);
                    sw.WriteLine();
                    sw.Write("set patchFile=");
                    sw.Write(textBox2.Text);
                    sw.WriteLine();
                    sw.Write("set memory=emmc");

                    //Close the file
                    sw.Close();
                }
                catch (Exception a)
                {
                    Console.WriteLine("Exception: " + a.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }
                TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                myform1textbox1.Clear();
                myform1textbox1.AppendText("==============================Process Started..........================================================" + Environment.NewLine);
                myform1textbox1.AppendText("==============================Waiting for Result's.....================================================" + Environment.NewLine);
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "./c qcom\\qc.cmd";
                p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                p.Start();
                p.BeginOutputReadLine();
            }
            if (checkBox6.Checked == true)
            {
                try
                {
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter("qcom\\qf.cmd");
                    //Write a line of text
                    sw.Write("set SaharaComPort=");
                    sw.Write(PORTER7.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set FirehoseFile=qcom\\huawei\\");
                    sw.Write(comboBox1.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set xmlFile=");
                    sw.Write(textBox9.Text);
                    sw.WriteLine();
                    //Write a second line of text
                    sw.Write("set pathFile=");
                    sw.Write(textBox8.Text);
                    sw.WriteLine();
                    sw.Write("set patchFile=");
                    sw.Write(textBox2.Text);
                    sw.WriteLine();
                    sw.Write("set memory=ufs");

                    //Close the file
                    sw.Close();
                }
                catch (Exception a)
                {
                    Console.WriteLine("Exception: " + a.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }
                TextBox myform1textbox1 = (ParentForm.Controls["textbox1"] as TextBox);
                myform1textbox1.Clear();
                myform1textbox1.AppendText("=================================Process Started..........================================================" + Environment.NewLine);
                myform1textbox1.AppendText("=================================Waiting for Result's.....================================================" + Environment.NewLine);
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "./c qcom\\qc.cmd";
                p.OutputDataReceived += new DataReceivedEventHandler(romprovider);
                p.Start();
                p.BeginOutputReadLine();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Patch0|*.xml;";
            openfile.Title = "Open a file..";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openfile.FileName;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
