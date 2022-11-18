using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    //EXPERIMENTAL
    public class OrçamentoController : DefaultController
    {
        public ActionResult Orçamentos()
        {
            return View();
        }

        public ActionResult CriarOrçamento(Oficina oficina)
        {
            List<Peça> peças = Model1.GetPeças(oficina.Id);
            List<Serviço> serviços = Model1.GetServiços(oficina.Id);
            List<Carro> carros = Model1.GetCarros(UserID);

            ViewBag.Peças = peças;
            ViewBag.Serviços = serviços;
            ViewBag.Carros = carros;
            

            return View();
        }

        [HttpPost]
        public void CriarOrçamento(Carro carro, Oficina oficina, List<Peça> peças, List<Serviço> serviços)
        {
            //E o itemOrçamento????
            try
            {
                DateTime dateTime = DateTime.Now;
                decimal valorCalculado = 0;
                //Considerar função valor calculado ou usar **javascript** pra calcular na view automáticamente.
                foreach (var item in peças)
                {
                    valorCalculado =+ item.Preço;
                }
                foreach (var item in serviços)
                {
                    valorCalculado =+ item.Preço;
                }
                    
                Model1.CreateOrçamento(UserID, carro.Placa, oficina.Id, dateTime, valorCalculado);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
           return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete()
        {
           return View();
            
        }
    }
}
