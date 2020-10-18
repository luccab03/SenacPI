using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AirManu.Models;

namespace AirManu.Controllers

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contato(Contato contato)
        {
            // Cria uma conexão com o banco de dados para contatos
            ContatoDatabase cd = new ContatoDatabase();

            // Insere o contato no bd de contatos 
            cd.Inserir(contato);

            return View();
        }

        public IActionResult Login()
        {

            // Zera a página
            ViewBag.check = false;

            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {

            // Cria uma conexão com o banco de dados para login
            LoginDatabase ld = new LoginDatabase();

            // Verifica se o login existe
            if (ld.Login(login) != null)
            {

                // Salva as informações em uma variável e na sessão
                Login logado = ld.Login(login);
                HttpContext.Session.SetString("Email", logado.Email);
                HttpContext.Session.SetString("Senha", logado.Senha);
                HttpContext.Session.SetInt32("Id", logado.Id);

                // Retorna para a página inicial
                return RedirectToAction("Index");
            }
            else
            {

                // Avisa o usuario que o login está errado
                ViewBag.mensagemLogin = "Login não encontrado";
                return View();
            }
        }

        public IActionResult Signin()
        {
            // Zera a página
            ViewBag.checks = false;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(Login login)
        {
            // Cria uma conexão com o banco de dados para signin
            LoginDatabase ld = new LoginDatabase();
            // Verifica se o login já existe
            if (ld.Login(login) == null)
            {
                // Insere o login no banco de dados, 
                // diz que o script foi executado,
                // avisa o usuario do sucesso
                ld.Inserir(login);
                ViewBag.checks = true;
                ViewBag.mensagemSign = $"Usuário cadastrado com sucesso! Id = {ld.Login(login).Id}";
                return View();
            }
            else
            {
                // Diz que o script foi executado existe
                // avisa o usuario do erro
                ViewBag.checks = true;
                ViewBag.mensagemSign = "Usuário já existe";
                return View();
            }
        }

        public IActionResult Logout()
        {
            // Limpa a sessão com os dados do usuário e redireciona pra página inicial
            HttpContext.Session.Clear();
            return View("Index");
        }

        public IActionResult Precos()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
