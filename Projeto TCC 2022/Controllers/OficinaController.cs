using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OficinaController : DefaultController
    {
        public ActionResult Page(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            Page page = new Page();
            if (oficina == null)
            {
                return RedirectToAction("Index", "Home");

            }
            page.Oficina = oficina;
            page.Imagem = Model1.GetImagem(Id);
            page.Cel = Model1.GetCelularTelefones(Id);
            page.Email = Model1.GetEmail(Id);
            page.Serviços = Model1.GetServiços(Id);
            List<AvItemMédia> Itens = new List<AvItemMédia>();
            double MédiaGeral = 0;

            if (page.Serviços != null) {

                List<Avaliação> Av = Model1.GetAvaliaçãoByOficinaId(Id);

                if (Av != null)
                {
                    foreach (var item in Av)
                    {
                        MédiaGeral += item.Estrelas; 
                    }

                    MédiaGeral /= Av.Count();
                }

                foreach (var item in page.Serviços)
                {
                    List<ItemAvaliação> AvItem = Model1.GetItemAvaliaçãoByServiçoId(item.Id);

                    if (AvItem != null)
                    {
                        double média = 0;
                        foreach (var item2 in AvItem)
                        { 
                            média += item2.Estrelas;
                        }

                        média /= AvItem.Count();

                        AvItemMédia avItemMédia = new AvItemMédia
                        {
                            Média = média,
                            Fk_Serviço_Id = item.Id
                        };

                        Itens.Add(avItemMédia);
                    }
                }
            }

            page.Média_Geral = MédiaGeral;
            page.Av = Itens;

            return View(page);
        }

        public ActionResult EditarOficina(int Id)
        {
            if (UserID == Id)
            {
                Oficina oficina = Model1.GetOficinaById(Id);
                Imagem imagem = Model1.GetImagem(Id);
                ViewBag.Imagem = imagem;
                return View(oficina);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOficina([Bind(Include = "Id,Email,CNPJ,Nome,CEP,Estado,Cidade,Rua,Número,Complemento,Bairro,Descrição,Aprovada,AceitaImportado")] Oficina oficina, string inicio, string fim)
        {
            if (UserID == oficina.Id && Oficina == true)
            {
                if (ModelState.IsValid)
                {
                    string HorarioFuncionamento = inicio + "/" + fim;
                    oficina.HorarioFuncionamento = HorarioFuncionamento;
                    Model1.UpdateOficina(oficina);
                    return RedirectToAction("Page/" + oficina.Id);
                }
            }
            return View(oficina);
        }

        public ActionResult Sugerir()
        {
            if (Oficina == true && Aprovada == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(string Texto)
        {
            if (Oficina == true && Aprovada == true)
            {
                Model1.SendMessage(UserID, Texto, false);
                return RedirectToAction("Sugerir");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}