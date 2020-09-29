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


        public IActionResult Contato(){
            return View();
        }

        [HttpPost]
        public IActionResult Contato(Contato contato)
        {
            ContatoDatabase cd = new ContatoDatabase();
            cd.Inserir(contato);
            
            return View();
        }

        public IActionResult Login(){
            ViewBag.check = false;
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(Login login){
            LoginDatabase ld = new LoginDatabase();
            if(ld.Login(login) != null){
                Login logado = ld.Login(login);
                HttpContext.Session.SetString("Email", logado.Email);
                HttpContext.Session.SetString("Senha", logado.Senha);
                HttpContext.Session.SetInt32("Id", logado.Id);
                return RedirectToAction("Index");
            } else {
                ViewBag.mensagemLogin = "Login não encontrado";
                return View();
            }
        }

        public IActionResult Signin()
        {
            ViewBag.checks = false;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(Login login)
        {
            LoginDatabase ld = new LoginDatabase();
            if (ld.Login(login) == null)
            {
                ld.Inserir(login);
                ViewBag.checks = true;
                ViewBag.mensagemSign = $"Usuário cadastrado com sucesso! Id = {ld.Login(login).Id}";
                return View();
            }
            else
            {
                ViewBag.checks = true;
                ViewBag.mensagemSign = "Usuário já existe";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        public IActionResult Precos(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
