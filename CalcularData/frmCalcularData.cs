using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CalcularData
{
    public partial class frmCalcularData : Form
    {
        public frmCalcularData()
        {
            InitializeComponent();
        }


        private void btnLimparCampos_Click(object sender, EventArgs e)
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                    (c as TextBox).Text = "";
            }
            string mascara = mskData.Mask;

            mskData.Text = "";

            mskData.Mask = mascara;
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            if (!((txtValor.Text).All(char.IsDigit)) && txtValor.Text != "-")
                MessageBox.Show("valor informado não é válido.", "Valor inválido");

        }

        private void txtOperador_TextChanged(object sender, EventArgs e)
        {
            if (txtOperador.Text != string.Empty)
                if (txtOperador.Text != "+" && txtOperador.Text != "-")
                {
                    MessageBox.Show("Operador informado não é válido.", "Operador inválido");
                    txtOperador.Text = string.Empty;
                }

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                ClsData data = new ClsData();

                if (data.StringEmpty(mskData.Text)) throw new Exception("Data inválida");

                if (data.StringEmpty(txtOperador.Text)) throw new Exception("Operador não foi informado.");

                if (data.StringEmpty(txtValor.Text)) throw new Exception("Valor não foi informado.");


                txtDataFinal.Text = data.CalcularData(mskData.Text, Convert.ToChar(txtOperador.Text), long.Parse(txtValor.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Verifique os campos. {ex.Message}");

            }
        }
    }
}
