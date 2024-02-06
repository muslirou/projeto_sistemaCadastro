using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaCadastro
{
    public partial class Form1 : Form
    {
        List<Pessoa> pessoas; // instanciar uma lista

        public Form1()
        {
            InitializeComponent();

            pessoas = new List<Pessoa>(); // inicia-se uma lista e coloca as opções no combobox

            ComboEC.Items.Add("Casado(a)");
            ComboEC.Items.Add("Solteiro(a)");
            ComboEC.Items.Add("Viuvo(a)");
            ComboEC.Items.Add("Separado(a)");

            ComboEC.SelectedIndex = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int index = -1; // para informar novo cadastro

            foreach (Pessoa pessoa in pessoas)
            {
                if(pessoa.Nome == txtNome.Text)
                {
                    index = pessoas.IndexOf(pessoa); //verifica se existe a pessoa cadastrada
                }
            }

            if(txtNome.Text == "") // se o campo não for preenchido
            {
                MessageBox.Show("Preencha o campo nome.");
                txtNome.Focus(); // alerta e mantem o foco no campo
                return; // finalizar o código pois sem o campo ele não pode continuar
            }

            if (txtTelefone.Text == "(  )      -") // se o campo não for preenchido
            {
                MessageBox.Show("Preencha o campo telefone.");
                txtTelefone.Focus(); // alerta e mantem o foco no campo
                return;
            }

            char genero;

            if (radioM.Checked)
            {
                genero = 'M';
            }
            else if(radioF.Checked)
            {
                genero = 'F';
            }
            else
            {
                genero = 'O';
            }

            Pessoa p = new Pessoa();
            p.Nome = txtNome.Text;
            p.DataNascimento = txtData.Text;
            p.EstadoCivil = ComboEC.SelectedItem.ToString();
            p.Telefone = txtTelefone.Text;
            p.CasaPropria = checkCasa.Checked;
            p.Veiculo = checkVeiculo.Checked;
            p.Genero = genero;

            if(index < 0) // if para adcionar nova pessoa ou alterar cadastro
            {
                pessoas.Add(p); // add nova pessoa
            }
            else
            {
                pessoas[index] = p; // alterando um novo valor ao index
            }

            btnLimpar_Click(btnLimpar, EventArgs.Empty); // executa o evento limpar sem acionar o botao

            Listar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int indice = lista.SelectedIndex;
            pessoas.RemoveAt(indice);
            Listar();

            btnLimpar_Click(btnLimpar, EventArgs.Empty);

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtData.Text = "";
            ComboEC.SelectedIndex = 0;
            txtTelefone.Text = "";
            checkCasa.Checked = false;
            checkVeiculo.Checked = false;
            radioM.Checked = true;
            radioF.Checked = false;
            radioO.Checked = false;
            txtNome.Focus();
        }

        private void Listar()
        {
            lista.Items.Clear();

            foreach (Pessoa p in pessoas) // pra cada item da lista é adcionado e atualizado
            {
                lista.Items.Add(p.Nome);
            }
        }

        private void lista_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indice = lista.SelectedIndex;
            Pessoa p = pessoas[indice];

            txtNome.Text = p.Nome;
            txtData.Text = p.DataNascimento;
            ComboEC.SelectedItem = p.EstadoCivil;
            txtTelefone.Text= p.Telefone;
            checkCasa.Checked = p.CasaPropria;
            checkVeiculo.Checked = p.Veiculo;

            switch (p.Genero)
            {
                case 'M':
                    radioM.Checked = true;
                    break;
                case 'F':
                    radioF.Checked = true;
                    break;
                default:
                    radioO.Checked = true;
                    break;
            }

        }
    }
}
