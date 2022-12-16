using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PripravljalecPrognozLib;
using System.Configuration;

namespace PripravljalecPrognozClient
{
    public partial class Form1 : Form
    {
        PripravljalecPrognozServiceClient _svc;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            try
            {
                _svc = new PripravljalecPrognozServiceClient();
                _svc.Init();
                label2.Text = _svc.GetConfigDescription();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri inicializaciji");
            }
        }

        public void DispatchMethod(string method, string args)
        {
            this.label3.Text = "Dispatch: " + method;
            Cursor = Cursors.WaitCursor;
            switch (method)
            { 
                case Methods.AddOfftakePoints:
                    _svc.AddOfftakePoints();
                    _svc.ModifyOfftakePoints();
                    _svc.GetOfftakePoints();
                    _svc.ChangeOfftakePointsSupplier();
                    break;
                case Methods.GetOfftakePoints:
                    _svc.GetOfftakePoints();
                    break;
                case Methods.GetOfftakePointsMeasurements:
                    _svc.GetOfftakePointsMeasurements();
                    break;
                case Methods.GetOfftakePointsReadings:
                    _svc.GetOfftakePointsReadings();
                    break;
                case Methods.AddOfftakePointsReadings:
                    _svc.AnnulOfftakePointsReadings();
                    _svc.AddOfftakePointsReadings();
                    _svc.GetOfftakePointsReadings();
                    break;
                case Methods.AddOfftakePointsMeasurments:
                    _svc.AddOfftakePointsMeasurements();
                    _svc.GetOfftakePointsMeasurements();
                    break;
                case Methods.GetOfftakePointsAllocations:
                    _svc.GetOfftakePointsAllocations();
                    break;
                case Methods.TestMethod:
                    MessageBox.Show("TEST OK");
                    break;
                case Methods.AddOfftakePointsEIS:
                    _svc.AddOfftakePointsEIS();
                    break;
                default:
                    this.label3.Text = "Ni implementirano v Dispatch " + method;
                    break;
            }
            this.label3.Text = "Dispatch: OK - " + method;
            Cursor = Cursors.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.AddOfftakePoints, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }

        private void buttonGetAloc_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.GetOfftakePointsAllocations, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }
        private void b_annull_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _svc.AnnulOfftakePointsReadings();
                _svc.AddOfftakePointsReadings();
                _svc.GetOfftakePointsReadings();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.GetOfftakePoints, "");
                //              _svc.GetOfftakePoints();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }
            MessageBox.Show("Uspeh", "Obvestilo");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.AddOfftakePointsReadings, "");
//                _svc.AddOfftakePointsReadings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.AddOfftakePointsMeasurments, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }
        private void buttonGM_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.GetOfftakePointsReadings, "");
                DispatchMethod(Methods.GetOfftakePointsMeasurements, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchMethod(Methods.AddOfftakePointsEIS, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka pri klicu servisa");
                return;
            }

            MessageBox.Show("Uspeh", "Obvestilo");
        }
    }
}
