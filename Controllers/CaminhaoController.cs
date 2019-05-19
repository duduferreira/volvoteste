using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Volvo.Data;
using Volvo.Models;

namespace Volvo.Controllers
{
    public class CaminhaoController : Controller
    {
        private readonly VolvoContext _context;

        public CaminhaoController(VolvoContext context)
        {
            _context = context;
        }

        // GET: Caminhao
        public async Task<IActionResult> Index()
        {
            var volvoContext = _context.Caminhao.Include(c => c.Modelo);
            return View(await volvoContext.ToListAsync());
        }

        // GET: Caminhao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _context.Caminhao
                .Include(c => c.Modelo)
                .Where(c => c.Modelo.Ativo)
                .FirstOrDefaultAsync(m => m.CaminhaoID == id);
            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // GET: Caminhao/Create
        public IActionResult Create()
        {
            ViewData["ModeloID"] = new SelectList(_context.Modelo, "ID", "Nome");
            return View();
        }

        // POST: Caminhao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaminhaoID,Descricao,Ano,AnoModelo,ModeloID")] Caminhao caminhao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caminhao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloID"] = new SelectList(_context.Modelo, "ID", "Nome", caminhao.ModeloID);
            return View(caminhao);
        }

        // GET: Caminhao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _context.Caminhao
                            .Include(c => c.Modelo)
                .Where(c => c.Modelo.Ativo)
                .Where(c => c.CaminhaoID == id)
                .FirstOrDefaultAsync();
            if (caminhao == null)
            {
                return NotFound();
            }
            ViewData["ModeloID"] = new SelectList(_context.Modelo, "ID", "Nome", caminhao.ModeloID);
            return View(caminhao);
        }

        // POST: Caminhao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaminhaoID,Descricao,Ano,AnoModelo,ModeloID")] Caminhao caminhao)
        {
            if (id != caminhao.CaminhaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caminhao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminhaoExists(caminhao.CaminhaoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloID"] = new SelectList(_context.Modelo, "ID", "Nome", caminhao.ModeloID);
            return View(caminhao);
        }

        // GET: Caminhao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caminhao = await _context.Caminhao
                .Include(c => c.Modelo)
                .FirstOrDefaultAsync(m => m.CaminhaoID == id);
            if (caminhao == null)
            {
                return NotFound();
            }

            return View(caminhao);
        }

        // POST: Caminhao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caminhao = await _context.Caminhao.FindAsync(id);
            _context.Caminhao.Remove(caminhao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaminhaoExists(int id)
        {
            return _context.Caminhao.Any(e => e.CaminhaoID == id);
        }
    }
}
