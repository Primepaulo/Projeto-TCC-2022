using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class AvaliaçãoController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.éAprovada = Aprovada;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Pessoa == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }

        public ActionResult Avaliar(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (orçamento.fk_Pessoa_Id == UserID)
            {
                List<Serviço> serviços = new List<Serviço>();
                List<ItemOrçamento> Itens = Model1.GetItems(Id);
                foreach (var item in Itens)
                {
                    Serviço serviço = Model1.GetServiçoByNomeId(orçamento.fk_Oficina_Id, item.Nome);

                    //Resolução que pode ocasionar bugs caso uma oficina tenha um serviço com o mesmo nome e descrição da peça, o que
                    //é possível.

                    if (serviço != null)
                    {
                        if (item.Descrição == serviço.Descrição && item.Nome == serviço.Nome)
                        {
                            if (serviços.Contains(serviço) != true)
                            {
                                serviços.Add(serviço);
                            }
                        }
                    }
                }

                Avaliar avaliar = new Avaliar
                {
                    Serviços = serviços,
                    Itens = Itens,
                    OrçamentoId = orçamento.Id
                };
                return View(avaliar);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Avaliar(Avaliar avaliar)
        {
            Orçamento orçamento = Model1.GetOrçamento(avaliar.OrçamentoId);
            if (orçamento.fk_Pessoa_Id == UserID)
            {
                int estrelas = 0;
                foreach (var item in avaliar.AvItem)
                {
                    estrelas += item.Estrelas;
                }

                float média = estrelas / avaliar.AvItem.Count();

                Avaliação avaliação = new Avaliação
                {
                    Estrelas = média,
                    Texto = avaliar.Texto,
                    fk_Pessoa_Id = UserID,
                    Fk_Oficina_Id = orçamento.fk_Oficina_Id,
                    Fk_Orçamento_Id = orçamento.Id
                };

                Model1.GerarAvaliação(avaliação, avaliar.AvItem);
            }
            return RedirectToAction("HistoricoAvaliações");
        }

        public ActionResult HistoricoAvaliações()
        {
            if (Pessoa == true)
            {
                List<Avaliação> Av = Model1.GetAvaliações(UserID);

                List<ItemAvaliação> Itens = new List<ItemAvaliação>();
                List<Oficina> oficinas = new List<Oficina>();
                List<Imagem> imagens = new List<Imagem>();
                List<Serviço> serviços = new List<Serviço>();
                List<Orçamento> orçamentos = new List<Orçamento>();

                foreach (var item in Av)
                {
                    Orçamento orçamento = Model1.GetOrçamento(item.Fk_Orçamento_Id);
                    Oficina oficina = Model1.GetOficinaById(orçamento.fk_Oficina_Id);

                    oficinas.Add(oficina);
                    orçamentos.Add(orçamento);

                    Imagem img = Model1.GetImagem(oficina.Id);

                    imagens.Add(img);

                    var x = Model1.GetItensAvaliação(item.Id);
                    foreach (var item2 in x)
                    {

                        Itens.Add(item2);
                        serviços.Add(Model1.GetServiço(item2.Fk_Serviço_Id));
                        
                    }
                }



                HistoricoAvaliação Hav = new HistoricoAvaliação
                {
                    Av = Av,
                    Itens = Itens,
                    Oficinas = oficinas,
                    Imagens = imagens,
                    Orçamentos = orçamentos,
                    Serviços = serviços
                };

                return View(Hav);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditarAvaliação(int Id)
        {
            Avaliação avaliação = Model1.GetAvaliação(Id);
            List<ItemAvaliação> Itens = avaliação.ItemAvaliação.ToList();
            List<Serviço> serviços = new List<Serviço>();
            List<ItemOrçamento> Or = Model1.GetItems(avaliação.Fk_Orçamento_Id);

            foreach (var item in Itens)
            {
                serviços.Add(Model1.GetServiço(item.Fk_Serviço_Id));
            }

            EditarAvaliação editarAvaliação = new EditarAvaliação
            {
                Avaliação = avaliação,
                Serviços = serviços,
                AvItem = Itens,
                Itens = Or
            };

            return View(editarAvaliação);
        }

        [HttpPost]
        public ActionResult Editar(EditarAvaliação editarAvaliação)
        {
            Avaliação avaliação = Model1.GetAvaliação(editarAvaliação.Id);
            List<ItemAvaliação> Itens = Model1.GetItensAvaliação(editarAvaliação.Id);
            int estrelas = 0;

            if (avaliação.fk_Pessoa_Id == UserID)
            {

                for (int i = 0; i < editarAvaliação.AvItem.Count(); i++)
                {
                    Itens[i].Estrelas = editarAvaliação.Estrelas[i];

                    estrelas += editarAvaliação.Estrelas[i];

                }

                float média = estrelas / editarAvaliação.AvItem.Count();

                avaliação.Texto = editarAvaliação.Texto;
                avaliação.Estrelas = média;

                Model1.UpdateAvaliação(avaliação, Itens);

                return RedirectToAction("HistoricoAvaliações", "Avaliação");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExcluirAvaliação(int Id)
        {
            Avaliação avaliação = Model1.GetAvaliação(Id);
            List<ItemAvaliação> Itens = avaliação.ItemAvaliação.ToList();
            List<Serviço> serviços = new List<Serviço>();
            Oficina oficina = Model1.GetOficinaById(Model1.GetOrçamento(avaliação.Fk_Orçamento_Id).fk_Oficina_Id);

            foreach (var item in Itens)
            {
                serviços.Add(Model1.GetServiço(item.Fk_Serviço_Id));
            }

            ExcluirAvaliação ex = new ExcluirAvaliação
            {
                Avaliação = avaliação,
                Serviços = serviços,
                Itens = Itens,
                Oficina = oficina
            };

            return View(ex);
        }

        [HttpPost]
        public ActionResult Excluir(int Id)
        {
            Avaliação av = Model1.GetAvaliação(Id);
            if (av.fk_Pessoa_Id == UserID)
            {
                Model1.DeleteAvaliação(av);
            }
            return RedirectToAction("HistoricoAvaliações", "Avaliação");
        }
    }
}