using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Volvo.Data
{
    public class DbInitializer
    {
        public static void Initialize(VolvoContext context)
        {
            context.Database.EnsureCreated();

            //verifica se existem registros na tabela "Modelo"
            if (!context.Modelo.Any())
                PopulaModelos(context);
            //verifica se existem registros na tabela "Caminhao"
            if (!context.Caminhao.Any())
                PopulaCaminhoes(context);
        }
        public static void PopulaModelos(VolvoContext ctx)
        {
            string rootPath = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(rootPath, "Resources", "modelo.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                List<Models.Modelo> modelos = JsonConvert.DeserializeObject<List<Models.Modelo>>(json);
                foreach (var m in modelos)
                {
                    ctx.Modelo.Add(m);                    
                }
                ctx.SaveChanges();
            }
        }
        public static void PopulaCaminhoes(VolvoContext ctx)
        {
            string rootPath = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(rootPath, "Resources", "caminhao.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                List<Models.Caminhao> caminhoes = JsonConvert.DeserializeObject<List<Models.Caminhao>>(json);
                foreach (var c in caminhoes)
                {
                    ctx.Caminhao.Add(c);
                }
                ctx.SaveChanges();
            }
        }
    }
}