using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsultaCep.Servico.Modelo;
using ConsultaCep.Servico;

namespace ConsultaCep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object send, EventArgs args)
        {
            string cep = CEP.Text.Trim(); //Trim remove todos espaços vazios
            
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCep(cep);

                    if (end == null)
                    {
                        DisplayAlert("ERRO", "O CEP " + cep + " não existe!", "OK");
                    }
                    else
                    {
                        RESULTADO.Text = string.Format("Endereço: {0}", end.logradouro);
                        RESULTADO.Text += string.Format("\nBairro: {0}", end.bairro);
                        RESULTADO.Text += string.Format("\nCidade: {0}", end.localidade);
                        RESULTADO.Text += string.Format("\nUF: {0}", end.uf);
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            int novoCEP = 0;
            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
