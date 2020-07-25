using System;
using System.Collections.Generic;

using Windows.UI;
using Windows.UI.Text;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SudGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        static int[,] matriz;

        static int acertos = 0;
        static int acertosAuto = 71;
        static int acertosNecessarios = 81;

        List<string> acertados = new List<string>();

        public MainPage()
        {
            this.InitializeComponent();

            interacao.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            interacao.FontSize = 14;
            interacao.Text = Shared.sharedMessage();

        }
        private void btnGen_Click(object sender, RoutedEventArgs e)
        {
            #region Resetando mensagens
            acertos = 0;
            interacao.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            interacao.FontSize = 24;
            interacao.Text = "Sudoku iniciado!";


            tempbox.Text = "";
            tempbox2.Text = "";
            #endregion
            ////////////////////////////////////////////////////////////////////////////
            matriz = Gen.SudokuGen.startAndGetSudoku();

            ////////////////////////////////////////////////////////////////////////////
            this.cleanGrid();

            ////////////////////////////////////////////////////////////////////////////
            int position, positionCont = 0;
            int[] positions = new int[acertosAuto];
            bool repetido;
            Random rand = new Random();

            while (positionCont < acertosAuto)
            {
                //GERAR ALEATORIO
                position = rand.Next(0, 81);
                //RESETAR 
                repetido = false;
                //VERIFICAR SE O NUMERO É REPETIDO
                for (int i = 0; i < positions.Length; i++)
                {
                    if (position == positions[i])
                    {
                        repetido = true;
                    }
                }
                //SE NÃO FOR REPETIDO, ADICIONE
                if (repetido == false)
                {
                    positions[positionCont] = position;
                    positionCont++;

                }
            }
            //ORDENAR VETOR DE POSICOES
            Array.Sort(positions);

            ////////////////////////////////////////////////////////////////////////////            

            TextBox tBox;
            var preto = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
            var azul = new SolidColorBrush(Color.FromArgb(100, 0, 0, 255));
            var verm = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
            var branco = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            var verde = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            var red = new SolidColorBrush(Color.FromArgb(100, 255, 46, 12));

            int z = 0;
            bool tem;

            for (int row = 0; row < gridSud.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < gridSud.ColumnDefinitions.Count; column++)
                {
                    //PESQUISAR SE VAI SER readOnly
                    tem = false;
                    for (int a = 0; a < positions.Length; a++)
                    {
                        if (z == positions[a])
                        {
                            tem = true;
                        }
                    }
                    tBox = new TextBox();
                    tBox = this.boxPadrao(tBox, column, row, tem);
                    z++;
                }
            }
        }

        private TextBox boxPadrao(TextBox temp, int column, int row, bool tem)
        {
            var preto = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
            var azul = new SolidColorBrush(Color.FromArgb(100, 70, 23, 180));
            var verm = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
            var branco = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            var verde = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            var red = new SolidColorBrush(Color.FromArgb(100, 255, 46, 12));

            temp = new TextBox();
            temp.FontSize = 36;
            temp.SetValue(TextBox.BorderBrushProperty, preto);
            //temp.Width = 45;
            //temp.Height = 50;
            temp.SetValue(TextBox.ForegroundProperty, preto);
            temp.Padding = new Thickness(18, 0, 0, 0);
            temp.SetValue(TextBox.FontWeightProperty, FontWeights.Bold);
            temp.SetValue(Grid.ColumnProperty, column);
            temp.SetValue(Grid.RowProperty, row);
            temp.MaxLength = 1;

            if (tem)
            {
                temp.Text = matriz[row, column].ToString();
                temp.SetValue(TextBox.IsReadOnlyProperty, true);
                gridSud.Children.Add(temp);
            }
            else
            {
                temp.Text = "";
                temp.SetValue(TextBox.IsReadOnlyProperty, false);
                temp.SetValue(TextBox.BackgroundProperty, azul);
                //EVENTOS
                temp.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(teste_KeyDown), true);
                temp.GotFocus += tbox_GotFocus;
                temp.LostFocus += tbox_LostFocus;
                gridSud.Children.Add(temp);
            }
            return temp;
        }

        //LIMPAR SUDOKU
        private void cleanGrid()
        {
            TextBox tBox;
            for (int i = 0; i < 3; i++)
            {
                for (int row = 0; row < gridSud.RowDefinitions.Count; row++)
                {
                    for (int column = 0; column < gridSud.ColumnDefinitions.Count; column++)
                    {
                        tBox = new TextBox();
                        tBox.SetValue(Grid.ColumnProperty, column);
                        tBox.SetValue(Grid.RowProperty, row);
                        tBox.ClearValue(TextBox.TextProperty);
                        gridSud.Children.Add(tBox);
                    }
                }
            }
        }

        //ALTERAR
        private void avaliar(object sender)
        {
            TextBox objTextBox = (TextBox)sender;

            var preto = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
            var azul = new SolidColorBrush(Color.FromArgb(100, 0, 0, 255));
            var verm = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
            var branco = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            var verde = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            var red = new SolidColorBrush(Color.FromArgb(100, 255, 46, 12));

            int y, x;

            y = Grid.GetColumn(objTextBox);
            x = Grid.GetRow(objTextBox);

            //tempbox.Text = matriz[x, y].ToString();
            tempbox.Text = "Acertos:";

            //SE O TEXTBOX NÃO ESTÁ VAZIO
            if (!objTextBox.Text.Equals(""))
            {
                //SE O TEXTBOX É IGUAL AO NÚMERO CERTO
                if (objTextBox.Text.Equals(matriz[x, y].ToString()))
                {
                    //DEIXE O TEXTO VERDE
                    objTextBox.SetValue(TextBox.ForegroundProperty, preto);
                    //objTextBox.SetValue(TextBox.BorderBrushProperty, verde);
                    objTextBox.SetValue(TextBox.BackgroundProperty, verde);

                    //RESETAR VARIÁVEIS PARA FAZER O TESTE
                    bool tem = false;
                    string temStr = "";


                    //PESQUISAR SE ESSE ACERTO JÁ FOI ADICIONADO ALGUMA VEZ
                    temStr += x.ToString();
                    temStr += y.ToString();
                    foreach (string tempAcertos in acertados)
                    {
                        if (tempAcertos.Equals(temStr))
                        {
                            tem = true;
                        }
                    }
                    //SE NÃO FOI ADICIONADO, ENTÃO ADICIONE
                    if (tem == false)
                    {
                        acertos++;
                        acertados.Add(temStr);
                    }
                }
                //SE NÃO É IGUAL AO NÚMERO CERTO
                else
                {
                    //DEIXE O TEXTO VERMELHO
                    objTextBox.SetValue(TextBox.ForegroundProperty, preto);
                    //objTextBox.SetValue(TextBox.BorderBrushProperty, red);
                    objTextBox.SetValue(TextBox.BackgroundProperty, red);

                    //RESETAR VARIÁVEIS PARA FAZER O TESTE
                    bool tem = false;
                    string temStr = "";
                    //PESQUISAR SE ESSE ACERTO JÁ FOI ADICIONADO ALGUMA VEZ
                    temStr += x.ToString();
                    temStr += y.ToString();
                    foreach (string tempAcertos in acertados)
                    {
                        if (tempAcertos.Equals(temStr))
                        {
                            tem = true;
                        }
                    }
                    //SE FOI ADICIONADO, ENTÃO REMOVA
                    if (tem == true)
                    {
                        acertos--;
                        acertados.Remove(temStr);
                    }
                }
            }
            tempbox2.Text = (acertos + acertosAuto).ToString();
            if ((acertos + acertosAuto) == acertosNecessarios)
            {
                interacao.Text = "Parabéns!";
            }

        }

        //QUANDO UMA TECLA É PRESSIONADA
        private void teste_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            this.avaliar(sender);
        }

        //QUANDO UMA CAIXA É SELECIONADA
        private void tbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            objTextBox.SelectAll();

            this.avaliar(sender);
        }

        //QUANDO UMA CAIXA PERDE A SELEÇÃO
        private void tbox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.avaliar(sender);
        }
    }

}
