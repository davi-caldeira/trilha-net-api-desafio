using Microsoft.AspNetCore.Mvc;
using trilha_net_api_desafio.Context;
using trilha_net_api_desafio.Models;
using System.Linq;

namespace trilha_net_api_desafio.Controllers
{

    public class TarefaController : Controller
    {
        private readonly OrganizadorContext _context;
        
        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tarefas = _context.Tarefas.ToList();
            return View(tarefas);
        }
        public IActionResult ObterPorId(int? id)
        {
            if (id.HasValue)
            {
                var tarefa = _context.Tarefas.Find(id.Value);
                if (tarefa != null)
                {
                    return View(tarefa);
                }
                else
                {
                    TempData["Erro"] = "Tarefa não encontrada.";
                    return View(new Tarefa());
                }
            }

            return View(new Tarefa());
        }

        public IActionResult ObterPorTitulo(string titulo)
        {
            // Se o título fornecido não for nulo ou vazio, realiza a busca
            if (!string.IsNullOrEmpty(titulo))
            {
                var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();

                // Verifica se a lista de tarefas encontradas não está vazia
                if (tarefas.Any())
                {
                    return View(tarefas);
                }
                else
                {
                    TempData["Erro"] = "Nenhuma tarefa encontrada com o título fornecido.";
                    return View(new List<Tarefa>());
                }
            }

            // Se nenhum título for fornecido, retorna uma lista vazia
            TempData["Erro"] = "Título não fornecido.";
            return View(new List<Tarefa>());
        }


        public IActionResult ObterPorData(DateTime ?data)
        {
            // Se o título fornecido não for nulo ou vazio, realiza a busca
            if (data != null)
            {
                var tarefas = _context.Tarefas.Where(x => x.Data.Date == data).ToList();

                // Verifica se a lista de tarefas encontradas não está vazia
                if (tarefas.Any())
                {
                    return View(tarefas);
                }
                else
                {
                    TempData["Erro"] = "Nenhuma tarefa encontrada com o título fornecido.";
                    return View(new List<Tarefa>());
                }
            }

            // Se nenhum título for fornecido, retorna uma lista vazia
            TempData["Erro"] = "Título não fornecido.";
            return View(new List<Tarefa>());
        }

        public IActionResult ObterPorStatus(bool status)
        {
            var statusIn = true;
            Console.WriteLine(statusIn);
            statusIn = (status ? true : false);
            Console.WriteLine(statusIn);
            
                var tarefas = _context.Tarefas.Where(x => x.Status == (statusIn)).ToList();

                // Verifica se a lista de tarefas encontradas não está vazia
                if (tarefas.Any())
                {
                    return View(tarefas);
                }
                else
                {
                    TempData["Erro"] = "Nenhuma tarefa encontrada com o status fornecido.";
                    return View(new List<Tarefa>());
                }

        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public IActionResult Editar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            
            if (tarefa == null)
            {
                return NotFound();
            }
            
            return View(tarefa);   
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _context.Tarefas.Update(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public IActionResult Detalhes(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        
        
        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            
            if (tarefa == null)
            {
                return NotFound();
            }
            
            return View(tarefa);   
        }

        [HttpPost]
        public IActionResult Deletar(Tarefa tarefa)
        {

        if (tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }
        
        _context.Tarefas.Remove(tarefa);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
        }
    }
}
