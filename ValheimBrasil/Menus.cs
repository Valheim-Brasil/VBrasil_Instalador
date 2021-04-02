using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using Microsoft.Win32;

namespace ValheimBrasil
{
    public class Menus
    {
        /*public static void TermosECondicoes()
        {
            Console.WriteLine("TERMOS E CONDIÇÕES GERAIS DE USO!");
            Console.WriteLine("Pra fins exclusivamente de proteção dos dados, serviços e acesso do \"servidor\", se comprometendo a não divulgar qualquer informação dos usuários cadastrados.");
            Console.WriteLine("Localizado nos seguintes links: (https://github.com/Valheim-Brasil/VBrasil_Instalador/blob/main/TERMOSECONDICOES.md) e (https://github.com/Valheim-Brasil/VBrasil/blob/main/TERMOSECONDICOES.md);");
            Console.WriteLine("Ao prosseguir na instalação você aceita os nossos Termos e Condições. Ao manter os arquivos baixados por este instalador em sua máquina você reafirma que aceitou os termos.");
            Console.WriteLine("\nVocê aceita estes termos e condições gerais de uso?");
            Console.Write("[S/N]\n> ");
            bool resposta = Util.ProsseguirOuNao();
            if(!resposta)
                Environment.Exit(1);
            if (resposta)
            {
                Console.WriteLine("Você aceitou os termos e condições gerais de uso.");
                Console.Clear();
            }
        }*/
        
        public static void Menu()
        {
            Console.WriteLine($"========== {Program.name} [{Program.version}] ==========\n");
            Console.WriteLine("AVISO: O JOGO DEVE SER DESLIGADO!");
            Console.WriteLine("Bem vindo ao instalador do Valheim Brasil");
            Console.WriteLine($"Este aplicativo foi projetado para instalar o {Program.name}, basicamente abre um Cliente Web para baixar o arquivo .zip,\nentão o extrai para a pasta selecionada e apaga o arquivo .zip baixado.");
            Console.WriteLine($"Esta aplicação é MIT, o código fonte está no repositório: {Program.repository}\n");
        }

        public static void InstallMenu()
        {
            Menu();
            Console.WriteLine($"Versão Instalada: [{Program.veratual}]\nVersão Mais Atual [{Program.verrepo}]\n");
            Console.WriteLine("O que você quer fazer?");
            Console.WriteLine($"[1] Instalar {Program.installname}");
            Console.WriteLine($"[2] Desinstalar {Program.installname}");
            Console.WriteLine($"[3] Atualizar {Program.installname}");
            Console.Write("> ");
        }
        
        public static void FinishThanks()
        {
            Console.WriteLine($"========== {Program.name} [{Program.version}] ==========\n");
            Console.WriteLine($"{Program.name} foi instalado com sucesso, seja bem vindo!.");
            Console.WriteLine("O instalador fechará em 10 segundos...");
            Console.WriteLine($"GitHub: {Program.creatorgithub}");
            Console.WriteLine($"\nCriado Por: {Program.creator}");
            System.Threading.Thread.Sleep(10000);
        }
        
        public static void Goodbye()
        {
            Console.WriteLine($"========== {Program.name} [{Program.version}] ==========\n");
            Console.WriteLine($"{Program.name} foi desinstalado com sucesso, adeus :(");
            Console.WriteLine("O instalador fechará em 10 segundos...");
            Console.WriteLine($"GitHub: {Program.creatorgithub}");
            Console.WriteLine($"\nCriado Por: {Program.creator}");
            System.Threading.Thread.Sleep(10000);
        }
        
        public static void UpdateMessage()
        {
            Console.WriteLine($"========== {Program.name} [{Program.version}] ==========\n");
            Console.WriteLine($"{Program.name} foi atualizado com sucesso!");
            Console.WriteLine("O instalador fechará em 10 segundos...");
            Console.WriteLine($"GitHub: {Program.creatorgithub}");
            Console.WriteLine($"\nCriado Por: {Program.creator}");
            System.Threading.Thread.Sleep(10000);
        }
    }
}